using DinkToPdf.Contracts;
using DinkToPdf;
using ReportPOC.Services;
using ReportPOC.ReportFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


builder.Services.AddScoped<PdfGenerator>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<TestHtmlPdf>();

// Register DinkToPdf services
var context = new CustomAssemblyLoadContext();

// Use the absolute path to load the DLL
string dllPath = Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll");
context.LoadUnmanagedLibrary(dllPath);

builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReportService, ReportService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
