using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WVB.TestUIFramework.Enums;
using WVB.TestUIFramework.Page;

namespace TestUIFramework.Test.Pages
{
    public class ContextPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "context-menu-one")]
        [CacheLookup]
        private IWebElement ContextButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "context-menu-icon-edit")]
        [CacheLookup]
        private IWebElement OptionEdit { get; set; }

        [FindsBy(How = How.ClassName, Using = "context-menu-icon-cut")]
        [CacheLookup]
        private IWebElement OptionCut { get; set; }

        [FindsBy(How = How.ClassName, Using = "context-menu-icon-copy")]
        [CacheLookup]
        private IWebElement OptionCopy { get; set; }

        [FindsBy(How = How.ClassName, Using = "context-menu-icon-paste")]
        [CacheLookup]
        private IWebElement OptionPaste { get; set; }

        [FindsBy(How = How.ClassName, Using = "context-menu-icon-delete")]
        [CacheLookup]
        private IWebElement OptionDelete { get; set; }

        [FindsBy(How = How.ClassName, Using = "context-menu-icon-quit")]
        [CacheLookup]
        private IWebElement OptionQuit { get; set; }

        public ContextPage() : base(TimeoutExplicitWait.BasicTimeout)
        {
        }

        public void NavigationToPage(string baseURL)
        {
            BaseURL = baseURL;
            Driver.Navigate().GoToUrl(BaseURL);
        }

        public void SelectOptionInContextMenu()
        {
            Actions.SelectOptionOnContextMenu(ContextButton, OptionPaste);
        }
    }
}
