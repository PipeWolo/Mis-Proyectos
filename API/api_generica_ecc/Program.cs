using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.WithOrigins("https://eccnetserver.entelcallcenter.cl",
                                              "http://eccnetserver.entelcallcenter.cl",
                                              "https://proyectos.entelcc.cl",
                                              "http://proyectos.entelcc.cl",
                                              "http://192.168.239.79",
                                              "http://192.168.239.32",
                                              "http://192.168.239.140",
                                              "http://192.168.239.150",
                                              "http://192.168.239.145",
                                              "http://192.168.239.146",
                                              "http://192.168.239.149",
                                              "http://192.168.239.147",
                                              "http://192.168.239.148",
                                              "http://192.168.239.126",
                                              "http://192.168.239.125",
                                              "http://192.168.239.127",
                                              "http://192.168.223.214",
                                              "http://192.168.223.58",
                                              "http://192.168.223.152",
                                              "http://192.168.223.158")
                                                  .AllowAnyMethod()
                                                  .AllowAnyHeader();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").GetSection("key").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
