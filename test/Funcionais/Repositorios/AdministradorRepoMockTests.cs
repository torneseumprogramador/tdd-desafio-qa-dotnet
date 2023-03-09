using Moq;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios;
using test.Mocks;
using tdd_desafio_qa_dotnet.Contexto;

namespace test.Funcionais.Repositorios;

[TestClass]
public class AdministradorRepoMockTests
{
    private Mock<AdministradorRepo> administradorRepo;

    // Este executa uma todas as vezes antes de cada cenário de teste
    [TestInitialize]
    public void SetUp()
    {
        administradorRepo = new Mock<AdministradorRepo>();
    }

    [TestMethod]
    public void TestandoSalvarOsDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        administradorRepo.Setup(x => x.Salvar(adm));
        administradorRepo.Setup(x => x.BuscaPorEmail(email)).Returns(CriaMockId(adm));

        // Act
        administradorRepo.Object.Salvar(adm);
        var admDb = administradorRepo.Object.BuscaPorEmail(email);

        // Assert
        Assert.IsNotNull(admDb);
    }

    [TestMethod]
    public void TestandoAtualizacaoDeDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        administradorRepo.Object.Salvar(adm);

        // Atualiza o nome do Administrador
        adm.Nome = "adm atualizado";
        administradorRepo.Setup(x => x.BuscaPorEmail(email)).Returns(new Administrador(){
            Nome = adm.Nome
        });

        // Act
        administradorRepo.Object.Salvar(adm);
        var admDb = administradorRepo.Object.BuscaPorEmail(email);

        // Assert
        Assert.AreEqual("adm atualizado", admDb.Nome);
    }

    [TestMethod]
    public void TestandoExclusaoDeDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        administradorRepo.Object.Salvar(adm);

        // Act
        administradorRepo.Object.Excluir(adm);
        var admDb = administradorRepo.Object.BuscaPorEmail(email);

        // Assert
        Assert.IsNull(admDb);
    }


    [TestMethod]
    public void TestandoBuscaPorObjetoInexistenteNoBanco()
    {
        // Arrange
        var email = "emailinexistente@teste.com";

        // Act
        var admDb = administradorRepo.Object.BuscaPorEmail(email);

        // Assert
        Assert.IsNull(admDb);
    }

    [TestMethod]
    public void TestandoBuscaPorObjetoExistenteNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = CriarAdministrador(email);
        administradorRepo.Object.Salvar(adm);
        administradorRepo.Setup(x => x.BuscaPorEmail(email)).Returns(new Administrador(){
            Email = adm.Email,
            Nome = adm.Nome,
            Senha = adm.Senha
        });

        // Act
        var admDb = administradorRepo.Object.BuscaPorEmail(email);

        // Assert
        Assert.AreEqual(email, admDb.Email);
        Assert.AreEqual("adm", admDb.Nome);
        Assert.AreEqual("teste", admDb.Senha);
    }


    [TestMethod]
    public void TestandoBuscaPorTodosOsObjetosNoBanco()
    {
        // Act / Assert
        administradorRepo.Setup(x => x.BuscarTodos()).Returns(new List<Administrador>());
        Assert.AreEqual(0, administradorRepo.Object.BuscarTodos().Count);

        // Arrange
        var adm1 = CriarAdministrador("adm1@teste.com");
        administradorRepo.Object.Salvar(adm1);

        var adm2 = CriarAdministrador("adm2@teste.com");
        administradorRepo.Object.Salvar(adm2);


        var mockList = new List<Administrador>();
        mockList.Add(new Administrador());
        mockList.Add(new Administrador());
        administradorRepo.Setup(x => x.BuscarTodos()).Returns(mockList);

        // Act
        var admsDb = administradorRepo.Object.BuscarTodos();

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

    private Administrador CriaMockId(Administrador adm)
    {
        adm.Id = 1;
        return adm;
    }
}