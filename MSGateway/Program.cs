using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration).AddPolly();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();



app.UseOcelot().Wait();
app.UseAuthorization();

app.MapControllers();

app.Run();
