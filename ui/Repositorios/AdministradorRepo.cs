using tdd_desafio_qa_dotnet.Contexto;
using tdd_desafio_qa_dotnet.Models;

namespace tdd_desafio_qa_dotnet.Repositorios;

public class AdministradorRepo
{
    private DbContexto _dbContexto;

    public AdministradorRepo(DbContexto dbContexto)
    {
        this._dbContexto = dbContexto;
    }

    public void Salvar(Administrador adm)
    {
        _dbContexto.Administradores.Add(adm);
        _dbContexto.SaveChanges();
    }

    public Administrador BuscaPorEmail(string email)
    {
        return _dbContexto.Administradores.Where(c => c.Email.ToLower() == email.ToLower().Trim()).FirstOrDefault();
    }
}