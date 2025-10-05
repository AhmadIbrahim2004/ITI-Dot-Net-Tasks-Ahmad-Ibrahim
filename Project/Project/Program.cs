using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Middleware;
using Project.Repositories.Implementations;
using Project.Repositories.Interfaces;
// Add using statements for your middleware and filters here later

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -- Registering Repositories for Dependency Injection --
// We use AddScoped so that a new repository is created for each web request.
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseStudentRepository, CourseStudentRepository>();


// -- Enable Session for State Management --
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// -- Add Middleware and Session --
app.UseMiddleware<RequestLoggingMiddleware>(); // We will add this later
app.UseSession(); // Important: UseSession must be called before UseEndpoints

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
