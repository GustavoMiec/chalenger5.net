using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; // Adicione isso para usar ILogger

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        // Configura o Firebase
        var path = Path.Combine(Directory.GetCurrentDirectory(), "config", "serviceAccountKey.json");
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(path)
        });

        // Adiciona o HttpClient para injeção de dependência
        services.AddHttpClient<PaymentService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        // Mensagem de log
        logger.LogInformation("Iniciando a aplicação...");

        // Configurações de ambiente
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Para desenvolvimento
        }
        else
        {
            app.UseExceptionHandler("/Home/Error"); // Para produção
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication(); // Remova isso se não estiver usando autenticação
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
