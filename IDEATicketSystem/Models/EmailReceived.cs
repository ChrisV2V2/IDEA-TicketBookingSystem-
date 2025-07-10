using System.ComponentModel.DataAnnotations;

namespace IDEATicketSystem.Models
{
    public class EmailReceived
    {
        [Key]
        public int Id { get; set; }
        public string Sender { get; set; } //The email addresss for the user that logged the ticket
        public string EmailContents { get; set; } //Email body
        public EmailAttachment emailAttachment { get; set; }
    }
}
