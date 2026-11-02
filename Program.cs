using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Swagger no Container de Serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "ExoApi - Gerenciamento de Projetos", 
        Version = "v1",
        Description = "API desenvolvida para as atividades práticas do SENAI."
    });
});

var app = builder.Build();

// Ativação do Swagger no Pipeline do Sistema
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExoApi v1");
    c.RoutePrefix = string.Empty; 
    // Define o Swagger como página inicial
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
