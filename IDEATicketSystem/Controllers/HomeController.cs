using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IDEATicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using IDEATicketSystem.Data;

namespace IDEATicketSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> SaveEmailsToDb()
    {
        var gmailService = new GmailEmailService();
        var unreadMessages = gmailService.GetUnreadMessages();

        foreach (var msg in unreadMessages)
        {
            var email = gmailService.ConvertToEmailReceived(msg);
            _context.ReceivedEmails.Add(email);
        }

        await _context.SaveChangesAsync();

        TempData["Message"] = "Emails saved as tickets successfully!";
        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        // Load recent 10 emails ordered by timestamp descending
        var emails = _context.ReceivedEmails
                             .OrderByDescending(e => e.EmailReceivedTimeStamped)
                             .Take(10)
                             .ToList();

        return View(emails);  // Pass the emails as the Model to the view
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
