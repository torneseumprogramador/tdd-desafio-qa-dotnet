using System.Net;
using System.Net.Http.Json;
using tdd_desafio_qa_dotnet.Contexto;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios;

namespace test.Funcionais.Repositorios;

[TestClass]
public class AdministradorRequestTests
{
    // Este executa uma vez ao iniciar o teste
    [ClassInitialize]
    public static void SetUp(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    // Este executa uma vez ao terminar o teste
    [ClassCleanup]
    public static void SetDown()
    {
        Setup.ClassCleanup();
    }

    private const string URL = "http://localhost:5135";

    [TestMethod]
    public async Task TestandoSalvarOsDadosNoBanco()
    {
        // Arrange
        var administrador = new Administrador
        {
            Nome = "Fulano",
            Email = "fulano@test.com",
            Senha = "senha"
        };

        // Act
        var response = await Setup.client.PostAsJsonAsync($"{URL}/api/administradores", administrador);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var administradorSalvo = await response.Content.ReadFromJsonAsync<Administrador>();
        Assert.IsNotNull(administradorSalvo);
        Assert.IsTrue(administradorSalvo.Id > 0);
        Assert.AreEqual(administrador.Nome, administradorSalvo.Nome);
        Assert.AreEqual(administrador.Email, administradorSalvo.Email);
    }


    [TestMethod]
    public async Task TestandoListarAdministradores()
    {
        // Arrange
        await postFake();

        // Act
        var response = await Setup.client.GetAsync($"{URL}/api/administradores");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var administradores = await response.Content.ReadFromJsonAsync<List<Administrador>>();
        Assert.IsNotNull(administradores);
        Assert.IsTrue(administradores.Count > 0);
    }

    [TestMethod]
    public async Task TestandoBuscarAdministradorPorId()
    {
        // Arrange
        var administrador = new Administrador
        {
            Nome = "Fulano",
            Email = "fulano@test.com",
            Senha = "senha"
        };
        var responseCreate = await Setup.client.PostAsJsonAsync($"{URL}/api/administradores", administrador);
        var administradorCriado = await responseCreate.Content.ReadFromJsonAsync<Administrador>();

        // Act
        var response = await Setup.client.GetAsync($"{URL}/api/administradores/{administradorCriado.Id}");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var administradorEncontrado = await response.Content.ReadFromJsonAsync<Administrador>();
        Assert.IsNotNull(administradorEncontrado);
        Assert.AreEqual(administradorCriado.Id, administradorEncontrado.Id);
        Assert.AreEqual(administradorCriado.Nome, administradorEncontrado.Nome);
        Assert.AreEqual(administradorCriado.Email, administradorEncontrado.Email);
    }

    [TestMethod]
    public async Task TestandoAtualizarAdministrador()
    {
        // Arrange
        var administrador = new Administrador
        {
            Nome = "Fulano",
            Email = "fulano@test.com",
            Senha = "senha"
        };
        var responseCreate = await Setup.client.PostAsJsonAsync($"{URL}/api/administradores", administrador);
        var administradorCriado = await responseCreate.Content.ReadFromJsonAsync<Administrador>();

        var administradorAtualizado = new Administrador
        {
            Id = administradorCriado.Id,
            Nome = "Beltrano",
            Email = "beltrano@test.com",
            Senha = "senha123"
        };

        // Act
        var response = await Setup.client.PutAsJsonAsync($"{URL}/api/administradores/{administradorCriado.Id}", administradorAtualizado);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var administradorAtual = await response.Content.ReadFromJsonAsync<Administrador>();
        Assert.IsNotNull(administradorAtual);
        Assert.AreEqual(administradorAtualizado.Id, administradorAtual.Id);
        Assert.AreEqual(administradorAtualizado.Nome, administradorAtual.Nome);
        Assert.AreEqual(administradorAtualizado.Email, administradorAtual.Email);
    }


    [TestMethod]
    public async Task TestandoExcluirAdministrador()
    {
        // Arrange
        var administrador = new Administrador
        {
            Nome = "Fulano",
            Email = "fulano@test.com",
            Senha = "senha"
        };
        var responseCreate = await Setup.client.PostAsJsonAsync($"{URL}/api/administradores", administrador);
        var administradorCriado = await responseCreate.Content.ReadFromJsonAsync<Administrador>();

        // Act
        var response = await Setup.client.DeleteAsync($"{URL}/api/administradores/{administradorCriado.Id}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        var responseGet = await Setup.client.GetAsync($"{URL}/api/administradores/{administradorCriado.Id}");
        Assert.AreEqual(HttpStatusCode.NotFound, responseGet.StatusCode);
    }


    private async Task postFake()
    {
       var administrador = new Administrador
        {
            Nome = "FulanoFake",
            Email = "fulanofake@test.com",
            Senha = "senhafake"
        };

        await Setup.client.PostAsJsonAsync($"{URL}/api/administradores", administrador);
    }
}