using tdd_desafio_qa_dotnet.Contexto;
using tdd_desafio_qa_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace tdd_desafio_qa_dotnet.Repositorios.Interfaces;

public interface IAdministradorRepo
{
    void Salvar(Administrador adm);
    Administrador BuscaPorEmail(string email);
    void Truncate();
    void Excluir(Administrador adm);
    List<Administrador> BuscarTodos();
    Administrador BuscaPorId(int id);
}