using InternalSystem.UserManagement.Database;
using InternalSystem.UserManagement.Database.DatabaseEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<UserManagementDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionString:UserManagementConnectionString"]));
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<UserManagementDbContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//added support for API versioning
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
});

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
