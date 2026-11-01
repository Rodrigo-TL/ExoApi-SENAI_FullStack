using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. ADICIONAR OS SERVIÇOS DO SWAGGER AO CONTAINER
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "ExoApi - Gerenciamento de Projetos", 
        Version = "v1",
        Description = "API desenvolvida para fins educacionais no curso do SENAI para controle de projetos internos."
    });
});

var app = builder.Build();

// 2. CONFIGURAR O PIPELINE DE REQUISIÇÃO HTTP PARA EXIBIR O SWAGGER
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExoApi v1");
        // Define o Swagger como a página inicial da aplicação
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

