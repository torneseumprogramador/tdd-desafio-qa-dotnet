using tdd_desafio_qa_dotnet.Contexto;
using tdd_desafio_qa_dotnet.Models;
using Microsoft.EntityFrameworkCore;

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
        if(adm.Id > 0)
            _dbContexto.Administradores.Update(adm);
        else
            _dbContexto.Administradores.Add(adm);
        
        _dbContexto.SaveChanges();
    }

    public Administrador BuscaPorEmail(string email)
    {
        return _dbContexto.Administradores.Where(c => c.Email.ToLower() == email.ToLower().Trim()).FirstOrDefault();
    }

    public void Truncate()
    {
        _dbContexto.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");
    }

    public void Excluir(Administrador adm)
    {
        _dbContexto.Administradores.Remove(adm);
        _dbContexto.SaveChanges();
    }

    public List<Administrador> BuscarTodos()
    {
        return _dbContexto.Administradores.ToList();
    }

    public Administrador BuscaPorId(int id)
    {
        return _dbContexto.Administradores.Where(c => c.Id == id).FirstOrDefault();
    }
}