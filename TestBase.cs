using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium;

public class TestBase
{
    protected WebDriver driver;
    protected WebDriverWait wait;
    protected JObject testData;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var option = new ChromeOptions();
        option.AddAdditionalChromeOption("useAutomationExtension", false);
        option.AddExcludedArgument("enable-automation"); 
        
        driver = new ChromeDriver(option);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.Navigate().GoToUrl("https://ufa.rabota.ru/");

        var path = Utils.GetFilePathByFileName("TestData.json");
        testData = JObject.Parse(File.ReadAllText(path));
    }
    
    [OneTimeTearDown]
    public void OneTimeTeardown()
    {
        driver.Quit();
    }
    
    protected void Authorization(string login, string password)
    {
        driver.FindElement(By.Name("login")).SendKeys(login);
        driver.FindElement(By.ClassName(Locators.submitButton)).Click();
        wait.Until(e=> e.FindElement(By.ClassName("auth-authorization-step")).Displayed);
        driver.FindElement(By.Name("password")).SendKeys(password);
        driver.FindElement(By.CssSelector("button[aria-label='Продолжить']")).Click();
    }

    protected void Registration()
    {
        string email = Utils.getRandomData() + "@mail.ru";
        driver.FindElement(By.Name("login")).SendKeys(email);
        driver.FindElement(By.ClassName(Locators.submitButton)).Click();
        wait.Until(e=> e.FindElement(By.ClassName("auth-registration-step")).Displayed);
        string password = Utils.getRandomData();
        driver.FindElement(By.Name("password")).SendKeys(password);
        driver.FindElement(By.CssSelector("button[aria-label='Зарегистрироваться']")).Click();
    }
}