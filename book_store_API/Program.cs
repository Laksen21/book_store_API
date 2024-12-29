using Microsoft.EntityFrameworkCore;
using book_store_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiContext>
    (opt => opt.UseInMemoryDatabase("BooksDb"));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Allow only this origin
               .AllowAnyMethod()                  // Allow any HTTP methods (GET, POST, etc.)
               .AllowAnyHeader();                 // Allow any headers
    });
});

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

app.UseCors("AllowAngularDevApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
