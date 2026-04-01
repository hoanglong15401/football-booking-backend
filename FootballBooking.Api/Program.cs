using FootballBooking.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (!string.IsNullOrEmpty(databaseUrl))
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        var connectionString =
            $"Server={uri.Host};" +
            $"Port={uri.Port};" +
            $"Database={uri.AbsolutePath.Trim('/')};" +
            $"User={userInfo[0]};" +
            $"Password={userInfo[1]};" +
            $"SslMode=Required;";

        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    else
    {
        options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(
                builder.Configuration.GetConnectionString("DefaultConnection"))
        );
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)
        ),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();