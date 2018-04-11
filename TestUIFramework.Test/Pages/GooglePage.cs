using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WVB.TestUIFramework.Page;

namespace TestUIFramework.Test.Pages
{
    public class GooglePage : BasePage
    {
        [FindsBy(How = How.Id, Using = "lst-ib")]
        [CacheLookup]
        private IWebElement CampoPesquisa { get; set; }

        [FindsBy(How = How.Name, Using = "btnK")]
        [CacheLookup]
        private IWebElement BotaoPesquisar { get; set; }

        public void NavigationToPage()
        {
            Driver.Navigate().GoToUrl(BaseURL);

            if (!URL.Equals(BaseURL))
                throw new NoSuchWindowException("deu ruim");
        }

        public void Pesquisar(string pesquisa)
        {
            CampoPesquisa.SendKeys(pesquisa);

            BotaoPesquisar.Click();
        }
        
    }
}
