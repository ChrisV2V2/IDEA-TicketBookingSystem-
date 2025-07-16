using IDEATicketSystem.Data.Seeders;
using IDEATicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace IDEATicketSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<EmailAttachment> EmailAttachments { get; set; }
        public DbSet<EmailReceived> ReceivedEmails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new EmailConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new EmailAttachmentConfiguration());

        }

    }
}
