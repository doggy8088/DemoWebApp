using ASPNETCoreWebApi6.Filters;
using Serilog;
using Serilog.Events;
using System.Text.Json;

#nullable disable

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    //builder.Logging.ClearProviders();
    //builder.Logging.AddJsonConsole(options =>
    //{
    //    options.JsonWriterOptions = new JsonWriterOptions
    //    {
    //        Indented = true
    //    };
    //    options.IncludeScopes = true;
    //});
    //builder.Logging.AddDebug();

    //builder.Services.AddCors(options =>
    //{
    //    options.AddDefaultPolicy(builder =>
    //    {
    //        builder.WithOrigins(new string[] { "https://github.dev", "https://docs.microsoft.com" })
    //            .AllowAnyHeader()
    //            .AllowAnyMethod();
    //    });
    //});

    builder.Services.AddDbContext<ContosouniversityContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    // Add services to the container.

    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new HttpResponseExceptionFilter());
    });

    builder.Host.UseSerilog();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
