using tdd_desafio_qa_dotnet.Servicos;

namespace test.Unidades.Servicos;

[TestClass]
public class ValidadorTest
{
    [TestMethod]
	public void ValidarCPFValido()
    {
		// Arrange
		bool cpfValido;

		// Act
		cpfValido = Validar.ValidarCPF("071.342.760-40");

		// Assert
		Assert.AreEqual(true, cpfValido);
	}

	[TestMethod]
    public void TestValidarCPFInvalido()
    {
        // Arrange
        bool cpfValido;

        // Act
        cpfValido = Validar.ValidarCPF("071.342.760-41");

        // Assert
        Assert.IsFalse(cpfValido);
    }

    [TestMethod]
    public void TestValidarCPFNoFormatoErrado()
    {
        // Arrange
        bool cpfValido;

        // Act
        cpfValido = Validar.ValidarCPF("sjnadslkjdas");

        // Assert
        Assert.IsFalse(cpfValido);
    }

    [TestMethod]
    public void TestValidarCPFVazio()
    {
        // Arrange
        bool cpfValido;

        // Act
        cpfValido = Validar.ValidarCPF("");

        // Assert
        Assert.IsFalse(cpfValido);
    }

    [TestMethod]
    public void TestValidarCPFValidoComEspaco()
    {
        // Arrange
        bool cpfValido;

        // Act
        cpfValido = Validar.ValidarCPF("071.342.760-40 ");

        // Assert
        Assert.IsTrue(cpfValido);
    }

    [TestMethod]
    public void TestValidarCPFNull()
    {
        // Arrange
        bool cpfValido;

        // Act
        cpfValido = Validar.ValidarCPF(null);

        // Assert
        Assert.IsFalse(cpfValido);
    }
}