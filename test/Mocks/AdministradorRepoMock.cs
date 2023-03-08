
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios.Interfaces;

namespace test.Mocks;

public class AdministradorRepoMock : IAdministradorRepo
{
    public static List<Administrador> baseEmMemoriaAdm = new List<Administrador>();
    public Administrador BuscaPorEmail(string email)
    {
        return baseEmMemoriaAdm.Find(a => a.Email == email);
    }

    public Administrador BuscaPorId(int id)
    {
        return baseEmMemoriaAdm.Find(a => a.Id == id);
    }

    public List<Administrador> BuscarTodos()
    {
        return baseEmMemoriaAdm;
    }

    public void Excluir(Administrador adm)
    {
        var admMemoria = baseEmMemoriaAdm.Find(a => a.Id == adm.Id);
        baseEmMemoriaAdm.Remove(admMemoria);
    }

    public void Salvar(Administrador adm)
    {
        if(adm.Id > 0)
        {
            var admMemoria = baseEmMemoriaAdm.Find(a => a.Id == adm.Id);
            admMemoria.Nome = adm.Nome;
            admMemoria.Email = adm.Email;
            admMemoria.Senha = adm.Senha;
        }
        else
        {
            adm.Id = baseEmMemoriaAdm.Count + 1;
            baseEmMemoriaAdm.Add(adm);
        }
    }

    public void Truncate()
    {
        baseEmMemoriaAdm = new List<Administrador>();
    }
}