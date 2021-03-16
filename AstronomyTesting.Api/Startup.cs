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
            services.AddTransient<IRolesRepository, EFRolesRepository>();
            services.AddTransient<ITestsRepository, EFTestsRepository>();
            services.AddTransient<IGroupsRepository, EFGroupsRepository>();
            services.AddTransient<IAnswersRepository, EFAnswersRepository>();
            services.AddTransient<IStudentsRepository, EFStudentsRepository>();
            services.AddTransient<IQuestionsRepository, EFQuestionsRepository>();
            services.AddTransient<IStudentAnswersRepository, EFStudentAnswersRepository>();
            services.AddTransient<IDetailsOfTheTestRepository, EFDetailsOfTheTestRepository>();
            services.AddTransient<DataManager>();

            services.AddDbContext<AstronomyTestingContext>((options) =>
            {
                options.UseSqlServer(ConfigDb.ConnectionStringDb);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => 
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
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
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
