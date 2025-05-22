using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nexus.API.DataService;
using Nexus.API.DataService.DataService;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;
using Nexus.API.Manager.Manager;
using Nexus.API.WebClient.ConfigureSwagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<UserManager>();

builder.Services.AddTransient<IUserDetailService, UserDetailService>();
builder.Services.AddTransient<UserDetailManager>();

builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<StatusManager>();

builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<ProjectManager>();

builder.Services.AddTransient<IIssueService, IssueService>();
builder.Services.AddTransient<IssueManager>();

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<TaskManager>();

builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<LoginManager>();

builder.Services.AddTransient<IRelationService, RelationService>();
builder.Services.AddTransient<RelationManager>();

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<EmailManager>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<NexusContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
