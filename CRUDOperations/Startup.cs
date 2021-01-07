using System.IO;
using AutoMapper;
using CRUDOperations.Api.Helpers;
using CRUDOperations.Api.Validators;
using CRUDOperations.Core;
using CRUDOperations.Data;
using CRUDOperations.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using InventoryContext = CRUDOperations.Data.InventoryContext;
using Swashbuckle.AspNetCore.Swagger;
namespace CRUDOperations.Api
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductService, ProductService>();
            services.AddDbContext<InventoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InventoryDatabase"), x => x.MigrationsAssembly("CRUDOperations.Data")));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD Operations", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "CRUDOperations.Api.xml");
                options.IncludeXmlComments(filePath);
            });
            services.AddScoped<SaveProductValidator>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger(); app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD Operations V1");
            });

            app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = new JsonExceptionMiddleware().Invoke
                }
            );
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
