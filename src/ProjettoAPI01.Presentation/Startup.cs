using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjetoAPI01.Repository.Contracts;
using ProjetoAPI01.Repository.Repositories;
using System;

namespace ProjettoAPI01.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Configuração de Injeção de Dependência

            //obtendo a string de conexão mapeada no arquivo /appsettings.json
            var connectionstring = Configuration.GetConnectionString("ProjetoAPI01");

            //configurando o repositorio..
            services.AddTransient<IFuncionarioRepository>(map => new FuncionarioRepository(connectionstring));

            #endregion

            #region Configuração do Swagger

            services.AddSwaggerGen(
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region Configuração do Swagger

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto");
            });

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
