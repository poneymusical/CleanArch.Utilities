using CleanArch.Domain;
using CleanArch.Repository;
using CleanArch.Utilities.DependencyInjection;
using CleanArch.Utilities.GenericCrud.DependencyInjection;
using CleanArch.Utilities.GenericCrud.Services.Create;
using CleanArch.Utilities.Repository.EntityFrameworkCore.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CleanArch.Api
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
            services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddDbContext<CleanArchContext>(options => options.UseInMemoryDatabase("CleanArchDb"));
            services.AddEFCoreGenericRepository<CleanArchContext>();

            var serviceAssembly = typeof(MockServiceRequest).Assembly;
            services.AddServices(serviceAssembly);
            services.AddGenericCrud(serviceAssembly);
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
