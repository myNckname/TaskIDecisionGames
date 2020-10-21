using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;


using TestIDecisionGames.DataBase;
using TestIDecisionGames.Exceptions;
using TestIDecisionGames.Interfaces;
using TestIDecisionGames.Repositories;
using TestIDecisionGames.Services;

namespace TestIDecisionGames
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
            ConfigureMongoDatabase(services);
            ConfigureRepositories(services);
            ConfigureCarsServices(services);
            ConfigureSwagger(services);

            services.AddControllers(options=>options.Filters.Add(new AsyncExceptionFilter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test Task");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public virtual void ConfigureMongoDatabase(IServiceCollection services)
        {
            services.Configure<CarsDbSettings>(Configuration.GetSection(nameof(CarsDbSettings)));
            services.AddTransient<ICarsDbSettings>(sp => sp.GetRequiredService<IOptions<CarsDbSettings>>().Value);
        }
        public virtual void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ICarsRepository, MongoCarsRepository>();
        }
        public virtual void ConfigureCarsServices(IServiceCollection services)
        {
            services.AddScoped<ICarsService, CarsService>();
        }
        public virtual void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "IDecisionGamesTest" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
