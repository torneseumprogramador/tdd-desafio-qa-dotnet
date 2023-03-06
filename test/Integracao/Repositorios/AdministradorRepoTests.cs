using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios;
using tdd_desafio_qa_dotnet.Contexto;

namespace test.Integracao.Repositorios;

[TestClass]
public class AdministradorRepoTests
{
    private static AdministradorRepo administradorRepo;

    [ClassInitialize]
    public static void SetUp(TestContext context)
    {
        administradorRepo = new AdministradorRepo(new DbContexto());
    }
    
    [TestMethod]
    public void TestandoSalvarOsDadosNoBanco()
    {
        // Arrange
        var email = "adm@teste.com";
        var adm = new Administrador()
        {
            Email = email,
            Nome = "adm",
            Senha = "teste"
        };

        // Act
        administradorRepo.Salvar(adm);
        var admDb = administradorRepo.BuscaPorEmail(email);

        // Assert
        Assert.IsNotNull(admDb);
    }

}