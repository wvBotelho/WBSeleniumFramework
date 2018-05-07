using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WVB.TestUIFramework.Enums;
using WVB.TestUIFramework.Page;

namespace TestUIFramework.Test.Pages
{
    public class AutoCompletePage : BasePage
    {
        public AutoCompletePage() : base(TimeoutExplicitWait.MediumTimeout)
        {
        }

        [FindsBy(How = How.Id, Using = "myInput")]
        private IWebElement CampoTexto { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#myInputautocomplete-list.div")]
        private IWebElement OpcaoAutoComplete { get; set; }

        public string GetTexto()
        {
            return CampoTexto.Text;
        }

        public void NavigateToAutoCompletePage(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void SelecionarOpcaoEmAutoComplete(string partialTexto)
        {
            Actions.SelectAutoComplete(CampoTexto, partialTexto, OpcaoAutoComplete);
        }
    }
}
