using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Filters;
using Project.Middleware;
using Project.Models;
using Project.Repositories.Implementations;
using Project.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.

// --- Database Context Setup ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// This service is required for the UseMigrationsEndPoint() helper.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// --- Identity Services Setup ---
// This adds the user and role management system.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// --- Repository Dependency Injection ---
// This tells the application how to create our custom repositories.
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseStudentRepository, CourseStudentRepository>();

// --- MVC and Session Services ---
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register the custom filter
builder.Services.AddScoped<AuthorizeStudentFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

// --- Authentication & Authorization Middleware ---
// These must be in this order and after UseRouting but before Map... calls.
app.UseAuthentication();
app.UseAuthorization();

// --- Custom Middleware and Session ---
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // Required for Identity UI pages

// --- Role and User Seeding ---
// This will automatically create roles and the first admin user on startup.
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "HR", "Instructor", "Student" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminUser = await userManager.FindByEmailAsync("admin@university.com");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin@university.com",
            Email = "admin@university.com",
            EmailConfirmed = true // Confirm email immediately for the admin
        };
        // Create the user with a password
        var result = await userManager.CreateAsync(adminUser, "Admin@123");
        if (result.Succeeded)
        {
            // Assign the user to the "Admin" role
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

app.Run();

