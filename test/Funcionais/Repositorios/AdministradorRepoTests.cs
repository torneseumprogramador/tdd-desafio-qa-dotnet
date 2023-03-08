using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios.Interfaces;
using test.Mocks;

namespace test.Funcionais.Repositorios;

[TestClass]
public class AdministradorRepoTests
{
    private static IAdministradorRepo administradorRepo;

    // Este executa uma todas as vezes antes de cada cenário de teste
    [TestInitialize]
    public void SetUp()
    {
        administradorRepo = new AdministradorRepoMock();
        administradorRepo.Truncate();
    }

    [TestMethod]
    public void TestandoSalvarOsDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);

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
        administradorRepo.Salvar(adm);

        // Atualiza o nome do Administrador
        adm.Nome = "adm atualizado";

        // Act
        administradorRepo.Salvar(adm);
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
        administradorRepo.Salvar(adm);

        // Act
        administradorRepo.Excluir(adm);
        var admDb = administradorRepo.BuscaPorEmail(email);

        // Assert
        Assert.IsNull(admDb);
    }


    [TestMethod]
    public void TestandoBuscaPorObjetoInexistenteNoBanco()
    {
        // Arrange
        var email = "emailinexistente@teste.com";

        // Act
        var admDb = administradorRepo.BuscaPorEmail(email);

        // Assert
        Assert.IsNull(admDb);
    }

    [TestMethod]
    public void TestandoBuscaPorObjetoExistenteNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        administradorRepo.Salvar(adm);

        // Act
        var admDb = administradorRepo.BuscaPorEmail(email);

        // Assert
        Assert.AreEqual(email, admDb.Email);
        Assert.AreEqual("adm", admDb.Nome);
        Assert.AreEqual("teste", admDb.Senha);
    }


    [TestMethod]
    public void TestandoBuscaPorIdExistenteNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        administradorRepo.Salvar(adm);

        // Act
        var admDb = administradorRepo.BuscaPorId(adm.Id);

        // Assert
        Assert.AreEqual(email, admDb.Email);
        Assert.AreEqual("adm", admDb.Nome);
        Assert.AreEqual("teste", admDb.Senha);
    }

    [TestMethod]
    public void TestandoBuscaPorTodosOsObjetosNoBanco()
    {
        // Act / Assert
        Assert.AreEqual(0, administradorRepo.BuscarTodos().Count);

        // Arrange
        var adm1 = CriarAdministrador("adm1@teste.com");
        administradorRepo.Salvar(adm1);

        var adm2 = CriarAdministrador("adm2@teste.com");
        administradorRepo.Salvar(adm2);

        // Act
        var admsDb = administradorRepo.BuscarTodos();

        // Assert
        Assert.AreEqual(2, admsDb.Count);
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
}