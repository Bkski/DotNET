using Lab7.Areas.Identity.Data;
using Lab7.Data;
using Lab7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lab7.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ChinookDbContext _chinook;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(

        ILogger<HomeController> logger, 
        ChinookDbContext chinook,
        UserManager<ApplicationUser> userManager
        )
    {
        _logger = logger;
        _chinook = chinook;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var customers = _chinook.Customers.ToList();
        return View(customers);
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

    [Authorize]
    public async Task<IActionResult> MyOrders()
    {
        var user = await _userManager.GetUserAsync(User);
        var customerId = user.CustomerId;
        return View(await _chinook.Invoices.Where(x =>
        x.CustomerId == customerId).ToListAsync());
    }

    [Authorize]
    public async Task<IActionResult> OrderDetails(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Forbid();

        var invoice = await _chinook.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == id);

        if (invoice == null || invoice.CustomerId != user.CustomerId)
            return NotFound();

        await _chinook.Entry(invoice)
            .Collection(i => i.InvoiceLines)
            .Query()
            .Include(il => il.Track)
            .LoadAsync();

        return View(invoice);
    }


}
