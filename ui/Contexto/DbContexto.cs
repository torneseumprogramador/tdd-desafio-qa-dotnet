using Microsoft.EntityFrameworkCore;
using tdd_desafio_qa_dotnet.Models;

namespace tdd_desafio_qa_dotnet.Contexto;

public class DbContexto : DbContext
{
    public DbContexto() { }

    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

    public virtual DbSet<Administrador> Administradores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var conexao = Startup.StrConexao();
            optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
        }
    }
}