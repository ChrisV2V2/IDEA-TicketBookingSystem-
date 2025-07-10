using System.ComponentModel.DataAnnotations;
using IDEATicketSystem.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace IDEATicketSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
