using EmployeeData;
using EmployeeDemo.Middelware;
using EmployeeRepository;
using EmployeeServices.Implementation;
using EmployeeServices.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Get The Connection String Value
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Registering The DbContext
builder.Services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
 string allowSpecificOrigins = "CorsPolicy";
builder.Services.AddCors(options => options.AddPolicy(allowSpecificOrigins, builder =>
{
    builder
       .AllowAnyMethod()
       .AllowAnyHeader()
       .AllowAnyOrigin();
}));
builder.Services.AddControllers();

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime=true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5276",
            ValidAudience = "http://localhost:5276",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(allowSpecificOrigins);

app.UseExceptionHandling();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
