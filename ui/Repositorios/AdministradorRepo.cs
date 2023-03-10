using tdd_desafio_qa_dotnet.Contexto;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace tdd_desafio_qa_dotnet.Repositorios;

public class AdministradorRepo : IAdministradorRepo
{
    private DbContexto _dbContexto;

    public AdministradorRepo(){ }

    public AdministradorRepo(DbContexto dbContexto)
    {
        this._dbContexto = dbContexto;
    }

    public virtual void Salvar(Administrador adm)
    {
        if(adm.Id > 0){
            var admDb = BuscaPorId(adm.Id);
            admDb.Nome = adm.Nome;
            admDb.Email = adm.Email;
            admDb.Senha = adm.Senha;
            
            _dbContexto.Administradores.Update(admDb);
        }
        else{
            _dbContexto.Administradores.Add(adm);
        }
        _dbContexto.SaveChanges();
    }

    public virtual Administrador BuscaPorEmail(string email)
    {
        return _dbContexto.Administradores.Where(c => c.Email.ToLower() == email.ToLower().Trim()).FirstOrDefault();
    }

    public void Truncate()
    {
        _dbContexto.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");
    }

    public virtual void Excluir(Administrador adm)
    {
        _dbContexto.Administradores.Remove(adm);
        _dbContexto.SaveChanges();
    }

    public virtual List<Administrador> BuscarTodos()
    {
        return _dbContexto.Administradores.ToList();
    }

    public virtual Administrador BuscaPorId(int id)
    {
        return _dbContexto.Administradores.Where(c => c.Id == id).FirstOrDefault();
    }
}