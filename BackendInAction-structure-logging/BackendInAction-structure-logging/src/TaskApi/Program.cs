using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.ConfigureLogging(logging =>
    {
        logging.AddSerilog();
        logging.SetMinimumLevel(LogLevel.Information);
    })
    .UseSerilog();

//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();