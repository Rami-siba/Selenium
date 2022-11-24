using System.Drawing;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.pages;
using SeleniumExtras.PageObjects;

namespace Selenium;


    [TestFixture]
    public class RabotaRuTests : TestBase
    {
        private bool isNeedScreenEtalon = true;
        
        
    [SetUp]
    public void Setup()
    {
        driver.Navigate().GoToUrl("https://ufa.rabota.ru/");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        driver.Close();
    }
    
    
    [Test]
    public void ScreenTesting()
    {
        string expectedPathFile = Utils.GetFilePathByFileName(@"/Screenshot/expect/main.png");
        string actualPathFile = Utils.GetFilePathByFileName(@"/Screenshot/actual/main.png");
        if (isNeedScreenEtalon)
        {
            Screenshot screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(expectedPathFile);
        }

        Screenshot screenActual = driver.GetScreenshot();
        screenActual.SaveAsFile(actualPathFile);
        byte[] expectedFile = File.ReadAllBytes(expectedPathFile);
        byte[] actualFile = File.ReadAllBytes(actualPathFile);
        Assert.AreEqual(expectedFile, actualFile);
    }
    
    
    [Test]
    [TestCase(800, 600)]
    [TestCase( 300, 600)]
    [TestCase(740, 1280)]
    public void CheckSizeWindowsJSTesting(int height, int weight)
    {
        driver.Manage().Window.Size = new Size(height, weight);
        IJavaScriptExecutor executor = driver;
            
        Boolean heightScroll =
            (Boolean) executor.ExecuteScript("return document.documentElement.scrollHeight > document.documentElement.clientHeight");
        Boolean weightScroll =
            (Boolean) executor.ExecuteScript(
                "return document.documentElement.scrollWidth > document.documentElement.clientWidth");
        Assert.False(weightScroll);
        Assert.True(heightScroll);
    }

    [Test]
    public void jsExampleTesting()
    {
        IJavaScriptExecutor executor = driver;
        var element = driver.FindElement(By.CssSelector("a[aria-label='Работодателю']"));
        executor.ExecuteScript("arguments[0].textContent = 'OLOLOLOLO'",
            element);
    }

    [Test]
    public void aFactoryTesting()
    {
        var mainPage = new MainPage(driver, wait);
        PageFactory.InitElements(driver, mainPage);
        mainPage.Vacancy.Click();
        wait.Until(e => e.FindElement(By.ClassName("vacancy-search-page")).Displayed);
        Assert.AreEqual("https://ufa.rabota.ru/vacancy", driver.Url);
    }

    [Test]
    public void CheckStyleTesting()
    {
        var element = driver.FindElement(By.CssSelector("button[aria-label='Удаленная работа']"));
        JObject styles = JObject.Parse(File.ReadAllText(Utils.GetFilePathByFileName(@"css/cssExpected.json")));
        Console.WriteLine(styles["fontSize"]);
        Console.WriteLine(element.GetCssValue("fontSize"));
        Assert.AreEqual(styles["fontWeight"].ToString(), element.GetCssValue("fontWeight"));
    }
}