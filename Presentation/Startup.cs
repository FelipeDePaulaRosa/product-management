using MediatR;
using Application;
using FluentValidation;
using Presentation.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Domain.Utils.FluentValidations;
using Microsoft.Extensions.Configuration;
using Infrastructure.DependencyInjections;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterApiServices();
            services.RegisterProductDependencyInjections(Configuration);
            
            services.AddMediatR(ApplicationAssemblyRef.Assembly);
            services.AddValidatorsFromAssembly(ApplicationAssemblyRef.Assembly, includeInternalTypes: true);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
           
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(NotificationFilter));
            });
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Management", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}