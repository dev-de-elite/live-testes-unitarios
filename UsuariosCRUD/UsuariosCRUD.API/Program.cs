using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UsuariosCRUD.API.Profiles;
using UsuariosCRUD.AuthenticationService.DependencyInjection;
using UsuariosCRUD.DatabaseService.DependecyInjection;
using UsuariosCRUD.DomainService.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Usuários API",
        Description = "Uma API para CRUD de usuários e geração de tokens de autenticação"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddDomainService();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddDatabaseService(builder.Configuration);

builder.Services.AddAutoMapper(typeof(ApiProfile).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Run();
