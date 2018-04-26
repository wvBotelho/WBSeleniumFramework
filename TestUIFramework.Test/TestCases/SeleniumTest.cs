using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using TestUIFramework.Test.Pages;
using WVB.TestUIFramework.Configuration;
using WVB.TestUIFramework.Enums;
using WVB.TestUIFramework.Page;
using WVB.TestUIFramework.TestCase;

namespace TestUIFramework.Test.TestCases
{
    [TestClass]
    public class SeleniumTest : BaseUITest
    {
        private string BaseURL = ConfigurationManager.AppSettings["BaseURL"];

        private string ErrorPath = ConfigurationManager.AppSettings["PathError"];

        private string FolderPath = ConfigurationManager.AppSettings["Drivers"];

        [TestInitialize]
        public void Initialize()
        {
            base.Initialize(Browsers.Chrome, FolderPath, BaseURL, TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void Finaliza()
        {
            base.CleanUp();
        }

        [TestMethod]
        public void Teste()
        {
            GooglePage googlePage = new GooglePage();

            googlePage.NavigationToPage();
            googlePage.Pesquisar("ps3brasil");
            googlePage.TakeScreeshot("Teste", ErrorPath, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
        }

        [TestMethod]
        public void Teste2()
        {
            Page.GetPage<GooglePage>().NavigationToPage();
            Page.GetPage<GooglePage>().Pesquisar("MeuPS4");
        }
    }
}
