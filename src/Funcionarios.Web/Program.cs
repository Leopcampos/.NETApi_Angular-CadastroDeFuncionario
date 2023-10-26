using Funcionarios.Infra.Data.Contracts;
using Funcionarios.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

#region Configuração de Injeção de Dependência

//obtendo a string de conexão mapeada no arquivo /appsettings.json
var connectionstring = builder.Configuration.GetConnectionString("ProjetoAPI01");

//configurando o repositorio..
builder.Services.AddTransient<IFuncionarioRepository>
    (map => new FuncionarioRepository(connectionstring));

builder.Services.AddTransient<IDependenteRepository>
    (map => new DependenteRepository(connectionstring));

#endregion

//#region Configuração do Swagger

//builder.Services.AddSwaggerGen(
//    s =>
//    {
//        s.SwaggerDoc("v1", new OpenApiInfo
//        {
//            Title = "API para controle de funcionários",
//            Version = "v1",
//            Description = "Projeto desenvolvido em NET CORE 3 API com DAPPER",
//            Contact = new OpenApiContact
//            {
//                Name = "COTI Informática - Treinamento C# WevDeveloper",
//                Url = new Uri("http://www.cotiinformatica.com.br"),
//                Email = "contato@cotiinformatica.com.br"
//            }
//        });
//    });

//#endregion

#region CORS - Cross Origin Resource Sharing

builder.Services.AddCors(
s => s.AddPolicy("DefaultPolicy",
builder =>
{
    builder.AllowAnyOrigin()//clientes de qualquer origem
    .AllowAnyMethod()  //qualquer método (POST, PUT, DELETE, GET, etc)
    .AllowAnyHeader(); //qualquer cabeçalho
})
);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region CORS - Cross Origin Resource Sharing

app.UseCors("DefaultPolicy");

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();