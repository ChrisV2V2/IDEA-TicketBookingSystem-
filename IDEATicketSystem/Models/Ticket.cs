using System.ComponentModel;
using IDEATicketSystem.Data.Enums;

namespace IDEATicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public SupportEngineer supportEngineer { get; set; }
        public Category category { get; set; }
        public Status status { get; set; }
        public string Owner { get; set; }//The person who's ticket is being dealt with (email address)
        public DateTime AssignedTimeStamp { get; set; }
    }
}
