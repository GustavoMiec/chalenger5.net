using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sprint3.Persistence;
using System.Configuration;
using System.Reflection;

namespace Sprint3
{
    public class Program
    {
        private const string SwaggerDocVersion = "v1";
        private const string SwaggerDocTitle = "Sprint3 API";
        private const string DefaultController = "Home";
        private const string DefaultAction = "Index";

        
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OracleDbContext>(options =>
            {
                options.UseOracle(configuration.GetConnectionString("OracleFiap")); // Usando a configuração passada
            });

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerDocVersion, new OpenApiInfo { Title = SwaggerDocTitle, Version = SwaggerDocVersion });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private static void Configure(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=" + DefaultController + "}/{action=" + DefaultAction + "}/{id?}");
        }
    }
}
