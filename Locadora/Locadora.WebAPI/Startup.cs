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
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Locadora.Comuns;

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

            var chave = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
