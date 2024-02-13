using crud_webapi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });

    option.AddPolicy("OnlySite", builder =>
    {
        builder.WithOrigins("http://localhost:4200/")
            .AllowAnyMethod().AllowAnyHeader();
    });
});

//builder.Services.AddDbContext<UserApiDbContext>(options => options.UseInMemoryDatabase("UsersDb"));
builder.Services.AddDbContext<UserApiDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsersApiConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
