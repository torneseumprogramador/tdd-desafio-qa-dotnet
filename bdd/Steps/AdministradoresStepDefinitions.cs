﻿using System;
using System.Threading;
using BDDValidator.Helpers;
using HtmlAgilityPack;
using NUnit.Framework;
using OpenQA.Selenium;
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
        Config.Limpar();

        _scenarioContext = scenarioContext;
        // var chromeDriverPath = Environment.GetEnvironmentVariable("PATH_CHROMEDRIVER");
        // _chromeDriver = new ChromeDriver(chromeDriverPath);
        _chromeDriver = new ChromeDriver();
        _host = Environment.GetEnvironmentVariable("HOST_CHROMEDRIVER");
    }

    [Given(@"que estou logado como administrador")]
    public void DadoQueEstouLogadoComoAdministrador()
    {
        _chromeDriver.Navigate().GoToUrl(_host + "/login");

        // Thread.Sleep(1000);
        _chromeDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        var emailField = _chromeDriver.FindElement(By.Id("form2Example1"));
        var passwordField = _chromeDriver.FindElement(By.Id("form2Example2"));

        // Enter email and password values
        emailField.SendKeys("test@example.com");
        // Thread.Sleep(1000);
        passwordField.SendKeys("password123");
        // Thread.Sleep(1000);

        // Submit the form
        var submitButton = _chromeDriver.FindElement(By.CssSelector("button[type='submit']"));
        submitButton.Click();
        // Thread.Sleep(2000);
    }

    [When(@"eu crio um novo administrador com nome ""(.*)"", email ""(.*)"", senha ""(.*)"" e confirmação de senha ""(.*)""")]
    public void QuandoEuCrioUmNovoAdministradorComNomeEmailSenhaEConfirmacaoDeSenha(string nome, string email, string senha, string csenha)
    {
        cadastroBasico(nome, email, senha, csenha);
    }

    [Then(@"o administrador com nome ""(.*)"" e email ""(.*)"" é adicionado com sucesso")]
    public void EntaoOAdministradorComNomeEEmailEAdicionadoComSucesso(string nome, string email)
    {
        var emailExists = validaNomeEmail(nome, email);

        // Assert
        Assert.True(emailExists);

       _chromeDriver.Close();
    }

    [Given(@"que existem os administradores cadastrados:")]
    public void DadoQueExistemOsAdministradoresCadastrados()
    {
        _chromeDriver.Navigate().GoToUrl(_host + "/Administradores");
        cadastroBasico("John", "jon@teste.com", "133", "133");
    }

    [Then(@"devo ver a lista com pelo menos (.*) administrador")]
    public void EntaoDevoVerAListaComPeloMenosAdministrador(int p0)
    {
        var html = _chromeDriver.PageSource;
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Find the table element by class
        var table = doc.DocumentNode.SelectSingleNode("//table[@class='table']");

        int quantidade = table.SelectNodes("tbody/tr").Count;

        Assert.True(quantidade > 0);

       _chromeDriver.Close();
    }

    [Given(@"que o administrador com nome ""(.*)"" e email ""(.*)"" já existe")]
    public void DadoQueOAdministradorComNomeEEmailJaExiste(string nome, string email)
    {
        cadastroBasico(nome, email, "senha", "csenha");
    }

    [When(@"eu atualizo o administrador com nome ""(.*)"" e email ""(.*)"" para o nome ""(.*)"", email ""(.*)"" e senha ""(.*)""")]
    public void QuandoEuAtualizoOAdministradorComNomeEEmailParaONomeEmailESenha(string nomeBusca, string emailBusca, string nome, string email, string senha)
    {
        localizaNomeEmail(nomeBusca, emailBusca, "Edit");
    }

    [Then(@"o administrador com nome ""(.*)"" e email ""(.*)"" é atualizado com sucesso")]
    public void EntaoOAdministradorComNomeEEmailEAtualizadoComSucesso(string nome, string email)
    {
        atualizaNomeEmail(nome, email);

        var emailExists = validaNomeEmail(nome, email);
        // Assert
        Assert.True(emailExists);

        _chromeDriver.Close();
    }
     
    [When(@"eu excluo o administrador com nome ""(.*)"" e email ""(.*)""")]
    public void QuandoEuExcluoOAdministradorComNomeEEmail(string nome, string email)
    {
        localizaNomeEmail(nome, email, "Delete");
        var submitButton = _chromeDriver.FindElement(By.CssSelector("input[type='submit']"));
        submitButton.Click();
    }

    [Then(@"o administrador com nome ""(.*)"" e email ""(.*)"" é removido com sucesso")]
    public void EntaoOAdministradorComNomeEEmailERemovidoComSucesso(string nome, string email)
    {
        var emailExists = validaNomeEmail(nome, email);
        // Assert
        Assert.False(emailExists);

        _chromeDriver.Close();
    }

    private void cadastroBasico(string nome, string email, string senha, string csenha)
    {
        _chromeDriver.Navigate().GoToUrl(_host + "/Administradores/Create");
        
        // Find the name, email, password and confirm password fields by ID
        var nameField = _chromeDriver.FindElement(By.Id("Nome"));
        var emailField = _chromeDriver.FindElement(By.Id("Email"));
        var passwordField = _chromeDriver.FindElement(By.Id("Senha"));
        var confirmPasswordField = _chromeDriver.FindElement(By.Id("csenha"));

        // Enter administrator details
        nameField.SendKeys(nome);
        emailField.SendKeys(email);
        passwordField.SendKeys(senha);
        confirmPasswordField.SendKeys(csenha);

        // Submit the form
        var submitButton = _chromeDriver.FindElement(By.CssSelector("input[type='submit']"));
        submitButton.Click();
    }

    private void atualizaNomeEmail(string nome, string email)
    {
        // Find the name, email, password and confirm password fields by ID
        var nameField = _chromeDriver.FindElement(By.Id("Nome"));
        var emailField = _chromeDriver.FindElement(By.Id("Email"));
        var passwordField = _chromeDriver.FindElement(By.Id("Senha"));

        // Enter administrator details
        nameField.Clear();
        nameField.SendKeys(nome);

        emailField.Clear();
        emailField.SendKeys(email);

        passwordField.Clear();
        passwordField.SendKeys("123456");

        // Submit the form
        var submitButton = _chromeDriver.FindElement(By.CssSelector("input[type='submit']"));
        submitButton.Click();
    }

    private void localizaNomeEmail(string nomeBusca, string emailBusca, string textoBotao)
    {
        var html = _chromeDriver.PageSource;
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Find the table element by class
        var table = doc.DocumentNode.SelectSingleNode("//table[@class='table']");

        // Loop through the table rows and check the name and email values
        var itens = table.SelectNodes("tbody/tr");
        if(itens is null) return;

        foreach (var row in itens)
        {
            var nomeHtml = row.SelectSingleNode("td[1]").InnerText.Trim();
            var emailHtml = row.SelectSingleNode("td[2]").InnerText.Trim();

            if (nomeHtml == nomeBusca && emailHtml == emailBusca)
            {
                var editLink = row.SelectSingleNode("td[4]/a[contains(text(), '" + textoBotao + "')]");
                var editUrl = editLink.Attributes["href"].Value;
                _chromeDriver.Navigate().GoToUrl(_host + editUrl);
                break;
            }
        }
    }

    private bool validaNomeEmail(string nome, string email)
    {
        _chromeDriver.Navigate().GoToUrl(_host + "/Administradores");

        var html = _chromeDriver.PageSource;
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Find the table element by class
        var table = doc.DocumentNode.SelectSingleNode("//table[@class='table']");

        // Loop through the table rows and check the email value
        var emailExists = false;
        var itens = table.SelectNodes("tbody/tr");
        if(itens is null) return false;

        foreach (var row in itens)
        {
            var nomeEncontrado = row.SelectSingleNode("td[1]").InnerText.Trim();
            var emailEncontrado = row.SelectSingleNode("td[2]").InnerText.Trim();
            if (nomeEncontrado == nome && emailEncontrado == email)
            {
                emailExists = true;
                break;
            }
        }

        return emailExists;
    }
}