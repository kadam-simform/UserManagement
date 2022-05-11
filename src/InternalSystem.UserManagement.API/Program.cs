using AutoMapper;
using InternalSystem.UserManagement.API.Extensions;
using InternalSystem.UserManagement.API.Helpers;
using InternalSystem.UserManagement.Database;
using InternalSystem.UserManagement.Database.DatabaseEntity;
using InternalSystem.UserManagement.Service.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExternalServices(builder.Configuration);
builder.Services.AddInternalServices(builder.Configuration, builder.Environment);
builder.Services.AddControllers();
builder.Services.AddDbContext<UserManagementDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionString:UserManagementConnectionString"]));
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<UserManagementDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.SaveToken = true;
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            LifetimeValidator = (before, expires, token, param) =>
            {
                return expires > DateTime.UtcNow;
            },
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(1);
builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.CustomSchemaIds(x => x.FullName);
    swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management", Version = "v1" });
});

//added support for API versioning
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
});
RegisterAutoMapper(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(app =>
{
    app.MapControllers();
});


app.Run();

 void RegisterAutoMapper(IServiceCollection services)
{
    // Auto Mapper Configurations
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new MappingProfile());
    });

    IMapper mapper = mappingConfig.CreateMapper();
    services.AddSingleton(mapper);
}
