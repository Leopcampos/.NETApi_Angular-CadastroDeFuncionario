using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using ProjetoAPI01.Repository.Contracts;
using ProjetoAPI01.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configuração de Injeção de Dependência

//obtendo a string de conexão mapeada no arquivo /appsettings.json
var connectionstring = builder.Configuration.GetConnectionString("ProjetoAPI01");

//configurando o repositorio..
builder.Services.AddTransient<IFuncionarioRepository>
    (map => new FuncionarioRepository(connectionstring));

builder.Services.AddTransient<IDependenteRepository>
    (map => new DependenteRepository(connectionstring));

#endregion

#region Configuração do Swagger

builder.Services.AddSwaggerGen(
    s =>
    {
        s.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API para controle de funcionários",
            Version = "v1",
            Description = "Projeto desenvolvido em NET CORE 3 API com DAPPER",
            Contact = new OpenApiContact
            {
                Name = "COTI Informática - Treinamento C# WevDeveloper",
                Url = new Uri("http://www.cotiinformatica.com.br"),
                Email = "contato@cotiinformatica.com.br"
            }
        });
    });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto"); });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
