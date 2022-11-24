using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.pages;
using SeleniumExtras.PageObjects;

namespace Selenium;

[TestFixture]
public class RabotaRuRegistrationTests : TestBase
{
    private JObject testUser1;
    private JObject testUser2;
    
    [SetUp]
    public void Setup()
    {
        driver.Navigate().GoToUrl("https://ufa.rabota.ru/");
        driver.FindElement(By.CssSelector(Locators.submitButton1)).Click();
        testUser1 = (JObject)testData["testUser1"];
        testUser2 = (JObject)testData["testUser2"];
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        driver.Close();
    }

    [Test]
    public void RegistrationTesting()
    {
        Registration();
        bool isDisplayed = wait.Until(e=> e.FindElement(By.ClassName("split-wrapper__bg-article")).Displayed);
        Assert.IsTrue(isDisplayed);
    }

    [Test]
    public void RegistrationNegativeTesting()
    {
        string number = Utils.getRandomData();
        driver.FindElement(By.Name("login")).SendKeys(number);
        driver.FindElement(By.ClassName(Locators.submitButton)).Click(); 
        bool isDisplayed = wait.Until(e=> e.FindElement(By.CssSelector("div[class='auth-main-step__error auth-main-step__error_show']")).Displayed);
        Assert.IsTrue(isDisplayed);
    }
    
    [Test]
    public void AuthPositiveTesting()
    {
        Authorization(testUser1["login"].ToString(), testUser1["password"].ToString());
        bool isDisplayed = wait.Until(e => e.FindElement(By.ClassName("user-profile-menu__user")).Displayed);
        Assert.IsTrue(isDisplayed);
    }
  
    [Test]
    public void AuthNegativeTesting()
    {
        Authorization(testUser2["login"].ToString(), testUser2["password"].ToString());
        bool isDisplayed = wait.Until(e => e.FindElement(By.ClassName("auth-authorization-step__error")).Displayed);
        Assert.IsTrue(isDisplayed);
    }

}