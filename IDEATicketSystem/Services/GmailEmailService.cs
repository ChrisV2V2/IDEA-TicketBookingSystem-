using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using IDEATicketSystem.Data;
using IDEATicketSystem.Models;
using Microsoft.Data.SqlClient;
using System.Text;

public class GmailEmailService
{
    private readonly GmailService _service;
    private readonly ApplicationDbContext _context;

    public GmailEmailService(ApplicationDbContext context)
    {
        _service = TicketService.GetGmailService();
        _context = context;
    }

    public void SavUnreadEmailsToDatabase()
    {
        IList<Message> unreadMessages = GetUnreadMessages();
        List<EmailReceived> emailList = new();

        foreach(var gmailMessage in unreadMessages)
        {
            var email = ConvertToEmailReceived(gmailMessage);
            emailList.Add(email);
        }

        if (emailList.Any())
        {
            _context.ReceivedEmails.AddRange(emailList);
            _context.SaveChanges();
        }
    }

    public IList<Message> GetUnreadMessages()
    {
        var request = _service.Users.Messages.List("me");
        request.LabelIds = new[] { "INBOX", "UNREAD" };
        request.Q = "is:unread";
        var response = request.Execute();

        return response.Messages ?? new List<Message>();
    }

    public EmailReceived ConvertToEmailReceived(Message gmailMessage)
    {
        var fullMessage = _service.Users.Messages.Get("me", gmailMessage.Id).Execute();
        var headers = fullMessage.Payload.Headers;

        string from = headers.FirstOrDefault(h => h.Name == "From")?.Value ?? "(unknown)";
        string subject = headers.FirstOrDefault(h => h.Name == "Subject")?.Value ?? "(no subject)";
        string body = GetMessageBody(fullMessage);

        return new EmailReceived
        {
            Sender = from,
            Subject = subject,
            EmailContents = body,
            EmailReceivedTimeStamped = DateTime.UtcNow
            // We'll add attachments later
        };
    }

    private string GetMessageBody(Message message)
    {
        if (message.Payload?.Parts != null)
        {
            foreach (var part in message.Payload.Parts)
            {
                if (part.MimeType == "text/plain" && part.Body?.Data != null)
                {
                    return DecodeBase64(part.Body.Data);
                }
            }
        }

        if (message.Payload?.Body?.Data != null)
        {
            return DecodeBase64(message.Payload.Body.Data);
        }

        return "(no body found)";
    }

    private string DecodeBase64(string input)
    {
        string fixedInput = input.Replace("-", "+").Replace("_", "/");
        var bytes = Convert.FromBase64String(fixedInput);
        return Encoding.UTF8.GetString(bytes);
    }
}
