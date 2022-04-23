using ASPNETCoreMVC6.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContosouniversityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<MyService>();
//builder.Services.AddScoped<MyService>();
//builder.Services.AddSingleton<MyService>();

ConfigureServices(builder);

var app = builder.Build();

Configure(app);

app.Run();





































static void ConfigureServices(WebApplicationBuilder builder)
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddHealthChecks();
}

static void Configure(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapHealthChecks("/healthz");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}