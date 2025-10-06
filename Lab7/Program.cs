using Lab7.Areas.Identity.Data;
using Lab7.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<ChinookDbContext>();


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("en-US") };
app.UseRequestLocalization(
    new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("en-US"),
        SupportedCultures = supportedCultures,
        FallBackToParentCultures = false
    }
);
CultureInfo.DefaultThreadCurrentCulture =
    CultureInfo.CreateSpecificCulture("en-US");
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "orderslist",
    pattern: "orders",
    defaults: new { controller = "Home", action = "MyOrders" }
);

app.MapControllerRoute(
    name: "order",
    pattern: "orders/{id}",
    defaults: new { controller = "Home", action = "OrderDetails" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var chinookContext = serviceProvider.GetRequiredService<ChinookDbContext>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var firstCustomerEmail = chinookContext.Customers.OrderBy(c => c.CustomerId).First().Email;
    if (await userManager.FindByEmailAsync(firstCustomerEmail) == null)
    {
        foreach (var customer in chinookContext.Customers)
        {
            var user = new ApplicationUser
            {
                UserName = customer.Email,
                NormalizedUserName = customer.Email.ToUpper(),
                Email = customer.Email,
                NormalizedEmail = customer.Email.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                CustomerId = customer.CustomerId
            };

            await userManager.CreateAsync(user, "P@ssw0rd");
        }
    }
}


app.Run();
