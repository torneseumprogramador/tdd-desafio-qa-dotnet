using Moq;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios;
using test.Mocks;
using tdd_desafio_qa_dotnet.Contexto;
using Microsoft.EntityFrameworkCore;

namespace test.Funcionais.Repositorios;

[TestClass]
public class AdministradorRepoMockEntityTests
{
    private Mock<DbContexto> dbContexto;
    private Mock<DbSet<Administrador>> mockDbSet;
    private AdministradorRepo administradorRepo;

    // Este executa uma todas as vezes antes de cada cenário de teste
    [TestInitialize]
    public void SetUp()
    {
        mockDbSet = new Mock<DbSet<Administrador>>();
        dbContexto = new Mock<DbContexto>();
        administradorRepo = new AdministradorRepo(dbContexto.Object);
    }

    [TestMethod]
    public void TestandoSalvarOsDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        
        var dados = new List<Administrador> { adm }.AsQueryable();
        mockIQueryable(dados);

        // Act
        administradorRepo.Salvar(adm);
        var admDb = administradorRepo.BuscaPorEmail(email);

        // Assert
        Assert.IsNotNull(admDb);
    }

    [TestMethod]
    public void TestandoAtualizacaoDeDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        var nomeAtualizado = "adm atualizado";

        var objComId = new Administrador(){
            Id = 1,
            Nome = nomeAtualizado,
            Email = adm.Email,
            Senha = adm.Senha
        };

        var dados = new List<Administrador> { objComId }.AsQueryable();
        mockIQueryable(dados);

        administradorRepo.Salvar(adm);

        // Atualiza o nome do Administrador
        adm.Nome = nomeAtualizado;
        
        // Act
        administradorRepo.Salvar(objComId);
        var admDb = administradorRepo.BuscaPorEmail(email);

        // Assert
        Assert.AreEqual("adm atualizado", admDb.Nome);
    }

    [TestMethod]
    public void TestandoExclusaoDeDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        var dados = new List<Administrador> { }.AsQueryable();
        mockIQueryable(dados);

        administradorRepo.Salvar(adm);

        // Act
        administradorRepo.Excluir(adm);
        var admDb = administradorRepo.BuscaPorEmail(email);

        // Assert
        Assert.IsNull(admDb);
    }

    private Administrador CriarAdministrador(string email)
    {
        return new Administrador()
        {
            Email = email,
            Nome = "adm",
            Senha = "teste"
        };
    }

    private Administrador CriaMockId(Administrador adm)
    {
        adm.Id = 1;
        return adm;
    }

    private void mockIQueryable(IQueryable<Administrador> dados)
    {
        mockDbSet.As<IQueryable<Administrador>>().Setup(m => m.Provider).Returns(dados.Provider);
        mockDbSet.As<IQueryable<Administrador>>().Setup(m => m.Expression).Returns(dados.Expression);
        mockDbSet.As<IQueryable<Administrador>>().Setup(m => m.ElementType).Returns(dados.ElementType);
        mockDbSet.As<IQueryable<Administrador>>().Setup(m => m.GetEnumerator()).Returns(dados.GetEnumerator());

        dbContexto.Setup(x => x.Administradores).Returns(mockDbSet.Object);
    }
}