using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class TicketService
{
    public static GmailService GetGmailService() 
    {
        UserCredential credential;

        using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json"; // this is where user's OAuth tokens are stored
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                new[] { GmailService.Scope.GmailReadonly }, // or GmailModify if you want to mark as read
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

            Console.WriteLine("Credential file saved to: " + credPath);
        }

        // Create Gmail API service
        return new GmailService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Gmail Ticket Reader",
        });
    }
}
