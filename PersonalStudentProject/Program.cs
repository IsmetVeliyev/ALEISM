using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Services;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Repositories;
using PersonalStudentProject.DataAccess.Context;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Lütfen geçerli bir token girin",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "your-secret-key-here")
        )
    };
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IUserSubRoomService, UserSubRoomService>();
builder.Services.AddScoped<IUserSubRoomRepository, UserSubRoomRepository>();
builder.Services.AddScoped<ISubRoomService, SubRoomService>();
builder.Services.AddScoped<ISubRoomRepository, SubRoomRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageSubService, MessageSubService>();
builder.Services.AddScoped<IMessageSubRepository, MessageSubRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IDirectMessageService, DirectMessageService>();
builder.Services.AddScoped<IDirectMessageRepository, DirectMessageRepository>();

builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var hash = scope.ServiceProvider.GetRequiredService<IHashService>();

    if (!db.Users.Any(u => u.Role == "Admin"))
    {
        db.Users.Add(new PersonalStudentProject.DataAccess.Models.User
        {
            Name = "Admin",
            Email = "admin@admin.com",
            Password = hash.HashPassword("Admin123"),
            Role = "Admin",
            Age = 0,
            Location = "System"
        });
        db.SaveChanges();
        Console.WriteLine("Default admin created: admin@admin.com / Admin123");
    }
}

app.UseCors("AllowAll");

if (app.Environment.EnvironmentName == "Development")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
