using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GopherToolboxRefresh.Data;
using GopherToolboxRefresh.Models;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>() 
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Index}/{id?}",
    defaults: new { controller = "Admin" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "adminOrders",
    pattern: "{controller=Admin}/{action=Orders}/{id?}");

app.MapControllerRoute(
    name: "adminApproveCancellation",
    pattern: "{controller=Admin}/{action=ApproveCancellation}/{id?}");

app.MapControllerRoute(
    name: "adminRejectCancellation",
    pattern: "{controller=Admin}/{action=RejectCancellation}/{id?}");

app.MapRazorPages();

// Seed the roles and admin user
await SeedRolesAndAdminUser(app);

app.Run();

async Task SeedRolesAndAdminUser(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        // Ensure the Admin role exists
        var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
        if (!adminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Ensure the Admin user exists and is assigned the Admin role
        var adminUser = await userManager.FindByEmailAsync("admin@tgies.pl");
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = "admin@tgies.pl",
                Email = "admin@tgies.pl",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, "H4$l0d0K0nt4A4m1n4");
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine($"Error adding admin user to role: {error.Description}");
                    }
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error creating admin user: {error.Description}");
                }
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine($"Error adding existing admin user to role: {error.Description}");
                    }
                }
            }
        }
    }
}
