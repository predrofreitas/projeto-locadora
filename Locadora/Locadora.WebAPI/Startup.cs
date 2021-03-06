using Locadora.Dados;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Locadora.Dados.Repositorios;
using Locadora.Dominio.Interfaces;
using System;
using RabbitMQ.Client;
using MediatR;
using Locadora.WebAPI.Handlers;

namespace Locadora.WebAPI
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
            services.AddTransient<IRepositorioCliente, RepositorioCliente>();
            services.AddTransient<IRepositorioAluguel, RepositorioAluguel>();
            services.AddTransient<IRepositorioItem, RepositorioItem>();
            services.AddTransient<IRepositorioEstoque, RepositorioEstoque>();
            services.AddTransient<IRepositorioAluguelItem, RepositorioAluguelItem>();

            services.AddMediatR((AppDomain.CurrentDomain.GetAssemblies()));

            services.AddScoped<UnitOfWork>();
            services.AddDbContext<LocadoraContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocadoraContext"));
            });
            services.AddSingleton<IConnection>(x =>
            {
                var fabrica = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };
                return fabrica.CreateConnection();
            });
            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Locadora.WebAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
