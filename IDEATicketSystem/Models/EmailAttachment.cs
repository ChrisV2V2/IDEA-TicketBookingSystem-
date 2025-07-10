using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDEATicketSystem.Models
{
    public class EmailAttachment
    {
        [Key]
        public int Id { get; set; }
        public int EmailId { get; set; }//Foreign key linking emailattachment to received email
        [ForeignKey("EmailReceived")]
        public byte[] Attachment { get; set; }
    }
}
