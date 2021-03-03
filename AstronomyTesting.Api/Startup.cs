using AstronomyTesting.Api.Service;
using AstronomyTesting.Model;
using AstronomyTesting.Model.Repositories.Abstract;
using AstronomyTesting.Model.Repositories.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AstronomyTesting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("ConfigDb", new ConfigDb());

            services.AddTransient<IUsersRepository, EFUsersRepository>();
            services.AddTransient<DataManager>();

            services.AddDbContext<AstronomyTestingContext>((options) =>
            {
                options.UseSqlServer(ConfigDb.ConnectionStringDb);
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
    }
}
