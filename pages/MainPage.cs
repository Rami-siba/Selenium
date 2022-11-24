using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Selenium.pages;

public class MainPage
{
    private WebDriver _driver;
        private WebDriverWait _wait;

        [FindsBy(How = How.CssSelector, Using = "a[aria-label='Вакансии']")]
        public IWebElement Vacancy { get; set; }

        public MainPage(WebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }
}
