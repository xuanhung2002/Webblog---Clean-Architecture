using Microsoft.AspNetCore.Identity;
using WebBlog.API.Middlewares;
using WebBlog.Infrastructure.DependencyInjection.Extensions;
using WebBlog.Infrastructure.Identity;
using WebBlog.Infrastructure.Persistance.DataSeeding;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 
builder.Services.AddSwagger();

builder.Services.ConfigureSqlServerRetryOptionsPersistence(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlServerPersistence();
builder.Services.AddBackgroupTaskQueue();
builder.Services.AddRepositoryPersistence();
builder.Services.AddAuthenticationWithJwt(builder.Configuration);
builder.Services.AddCaching(builder.Configuration);
builder.Services.AddAuthorization();


var app = builder.Build();

// Seed roles
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
    await AdminUserSeeder.SeedAdmin(userManager, services);
}
catch
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError("Seed failed");
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
