using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using tdd_desafio_qa_dotnet.Contexto;
using tdd_desafio_qa_dotnet.Repositorios;
using tdd_desafio_qa_dotnet.Repositorios.Interfaces;

namespace tdd_desafio_qa_dotnet;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get;set; }
    public static string StrConexao(IConfiguration Configuration = null)
    {
        string conexao = Environment.GetEnvironmentVariable("DATABASE_URL");
        if(string.IsNullOrEmpty(conexao))
        {
            conexao = Configuration?.GetConnectionString("Conexao");

            if(string.IsNullOrEmpty(conexao))
                conexao = "Server=localhost;Database=desafioqa;Uid=root;Pwd=root";
        }
        
        return conexao;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        var conexao = StrConexao(Configuration);
        services.AddDbContext<DbContexto>(options =>
        {
            options.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
        });

        services.AddScoped<IAdministradorRepo, AdministradorRepo>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });
    }
}