using Loja.Web.Application.Applications.Registration.Address;
using Loja.Web.Application.Applications.Registration.Contact;
using Loja.Web.Application.Applications.Registration.Finance;
using Loja.Web.Application.Applications.Registration.Image;
using Loja.Web.Application.Applications.Registration.Manufacturer;
using Loja.Web.Application.Applications.Registration.Product;
using Loja.Web.Application.Applications.Security;
using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Application.Interfaces.Registration.Contact;
using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Application.Interfaces.Registration.Image;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Application.Interfaces.Security;
using Loja.Web.Infra.CrossCutting.Config;
using Loja.Web.Presentation.MVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

#region Global config variable
Settings.Configuration = builder.Configuration;
#endregion

#region Session configuration
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion

#region Dependency injection
builder.Services.AddSingleton<ISecurityApplication, SecurityApplication>();
builder.Services.AddSingleton<IAddressApplication, AddressApplication>();
builder.Services.AddSingleton<IContactApplication, ContactApplication>();
builder.Services.AddSingleton<IManufacturerApplication, ManufacturerApplication>();
builder.Services.AddSingleton<ICategoryApplication, CategoryApplication>();
builder.Services.AddSingleton<ISubcategoryApplication, SubcategoryApplication>();
builder.Services.AddSingleton<IProductApplication, ProductApplication>();
builder.Services.AddSingleton<ICurrencyApplication, CurrencyApplication>();
builder.Services.AddSingleton<IMeasurementApplication, MeasurementApplication>();
builder.Services.AddSingleton<IImageApplication, ImageApplication>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

#region Session configuration:
app.UseSession();
#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
