using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace BDDValidator.Steps;

[Binding]
public sealed class ValidateCPFStepDefinitions
{
    private string _host;
    private ChromeDriver _chromeDriver;
    private readonly ScenarioContext _scenarioContext;

    public ValidateCPFStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        var chromeDriverPath = Environment.GetEnvironmentVariable("PATH_CHROMEDRIVER");
        _host = Environment.GetEnvironmentVariable("HOST_CHROMEDRIVER");
        _chromeDriver = new ChromeDriver(chromeDriverPath);
    }

    [Given(@"que estou logado como administrador")]
    public void DadoQueEstouLogadoComoAdministrador()
    {
        _chromeDriver.Navigate().GoToUrl(_host);

        Thread.Sleep(2000);

        _chromeDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
    }

    [When(@"eu crio um novo administrador com nome ""(.*)"", email ""(.*)"", senha ""(.*)"" e confirmação de senha ""(.*)""")]
    public void QuandoEuCrioUmNovoAdministradorComNomeEmailSenhaEConfirmacaoDeSenha(string p0, string p1, string p2, string p3)
    {
        // _scenarioContext.Pending();
    }

    [Then(@"o administrador com nome ""(.*)"" e email ""(.*)"" é adicionado com sucesso")]
    public void EntaoOAdministradorComNomeEEmailEAdicionadoComSucesso(string p0, string p1)
    {
       _chromeDriver.Close();
    }
}





// [Binding]
// public sealed class ValidateCPFStepDefinitions
// {
//     private ChromeDriver _chromeDriver;

//     private readonly ScenarioContext _scenarioContext;

//     public ValidateCPFStepDefinitions(ScenarioContext scenarioContext)
//     {
//         _scenarioContext = scenarioContext;
//         _chromeDriver = new ChromeDriver(@"C:\Users\Administrador\source\repos\WebTddBdd\BDDValidator\Drivers\windows");
//     }

//     [Given(@"i'm in home page")]
//     public void GivenImInHomePage()
//     {
//         _chromeDriver.Url = "https://localhost:5001/";
//     }

//     [Given(@"I type the CPF ""(.*)""")]
//     public void GivenITypeTheCPF(string cpf)
//     {
//         _chromeDriver.FindElementByCssSelector("#Nome").SendKeys("Danilo BDD");
//         _chromeDriver.FindElementByCssSelector("#CPF").SendKeys("666.116.300-31");
//     }

//     [When(@"I click on button validate")]
//     public void WhenIClickOnButtonValidate()
//     {
//         _chromeDriver.FindElementByCssSelector("input[type='submit']").Click();
//     }

//     [Then(@"I have the successful result")]
//     public void ThenIHaveTheSuccessfulResult()
//     {
//         Assert.IsTrue(_chromeDriver.FindElementsByCssSelector(".alert-success").Count > 0);
//         _chromeDriver.Close();
//         _chromeDriver.Dispose();
//     }
// }

