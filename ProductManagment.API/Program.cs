using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using ProductManagment.API.Helpers;
using ProductManagment.API.Repository;
using ProductManagment.API.Services;
using ProductManagment.API.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton(typeof(IJsonFileHelper<>),typeof(JsonFileHelper<>));
builder.Services.Configure<AppConfigSettings>(builder.Configuration.GetSection("AppConfigSettings"));
builder.Services.AddCors();
builder.Services.AddControllers();
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
app.UseFileServer(enableDirectoryBrowsing: true);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(x => x.AllowAnyMethod()
.AllowAnyHeader()
.AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
