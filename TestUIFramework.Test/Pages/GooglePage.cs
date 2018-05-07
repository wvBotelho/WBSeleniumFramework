using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WVB.TestUIFramework.Enums;
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

        [FindsBy(How = How.Id, Using = "sbse0")]
        private IWebElement OpcaoAutoComplete { get; set; }

        public GooglePage() : base(TimeoutExplicitWait.BasicTimeout)
        {
        }

        //public void NavigationToPage()
        //{
        //    try
        //    {
        //        Driver.Navigate().GoToUrl(BaseURL);

        //        if (!URL.Equals(BaseURL))
        //            throw new NoSuchWindowException("deu ruim");
        //    }
        //    catch (WebDriverTimeoutException e)
        //    {
        //        throw new WebDriverTimeoutException(e.Message.ToString());
        //    }            
        //}

        public void Pesquisar(string pesquisa)
        {
            CampoPesquisa.SendKeys(pesquisa);

            BotaoPesquisar.Click();
        }

        public void PesquisarUsandoAutoComplete(string partialPesquisa)
        {
            Actions.SelectAutoComplete(CampoPesquisa, partialPesquisa, OpcaoAutoComplete);
        }
    }
}
