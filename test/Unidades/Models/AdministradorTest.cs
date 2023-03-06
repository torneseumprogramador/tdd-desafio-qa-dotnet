using tdd_desafio_qa_dotnet.Models;

namespace test.Unidades.Models;

[TestClass]
public class AdministradorTest
{
    private static Administrador administrador = new Administrador();

    [ClassInitialize]
    public static void SetUp(TestContext context)
    {
        administrador = new Administrador();
    }
    

    [TestMethod]
    public void TestandoSeterDaPropriedadeNome()
    {
        // Arrange
        string name = "John Doe";

        // Act
        administrador.Nome = name;

        // Assert
        Assert.AreEqual(name, administrador.Nome);
    }

    [TestMethod]
    public void ShouldSetAndGetEmail()
    {
        // Arrange
        string email = "johndoe@example.com";

        // Act
        administrador.Email = email;

        // Assert
        Assert.AreEqual(email, administrador.Email);
    }

    [TestMethod]
    public void ShouldSetAndGetPassword()
    {
        // Arrange
        string password = "password123";

        // Act
        administrador.Senha = password;

        // Assert
        Assert.AreEqual(password, administrador.Senha);
    }
}