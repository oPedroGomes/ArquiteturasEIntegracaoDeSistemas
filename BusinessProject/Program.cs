using BusinessProject;
using BusinessProject.BL;
using Elastic.CommonSchema.Serilog;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();




Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Information()
       .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Error)
       .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
       .WriteTo.Console(new EcsTextFormatter())
       .WriteTo.Elasticsearch(new[] { new Uri("http://elastic:changeme@127.0.0.1:9200") }, opts =>
       {
           opts.TextFormatting = new EcsTextFormatterConfiguration<Elastic.CommonSchema.EcsDocument>()
           {
               IncludeHost = false,
               IncludeProcess = false,
               IncludeUser = false,

           };
           opts.DataStream = new DataStreamName("logs", "console-example", "demo");
           opts.BootstrapMethod = BootstrapMethod.Failure;

       }, (trans) =>
       {
           trans.DisablePing();
       })
       .CreateLogger();
builder.Host.UseSerilog(Log.Logger, true);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/{0}");

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
