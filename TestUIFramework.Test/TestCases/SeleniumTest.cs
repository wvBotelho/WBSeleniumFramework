using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using TestUIFramework.Test.Pages;
using WVB.TestUIFramework.Enums;
using WVB.TestUIFramework.TestCase;

namespace TestUIFramework.Test.TestCases
{
    [TestClass]
    public class SeleniumTest : BaseUITest
    {
        private string BaseURL = ConfigurationManager.AppSettings["BaseURL"];

        private string ErrorPath = ConfigurationManager.AppSettings["PathError"];

        public SeleniumTest() : base(Browsers.Chrome, ConfigurationManager.AppSettings["Drivers"])
        {
        }

        [TestInitialize]
        public void Initialize()
        {
            base.Initialize(TimeSpan.FromSeconds(30), true);
        }

        [TestCleanup]
        public void Finaliza()
        {
            base.CleanUp();
        }

        [TestMethod]
        public void Teste()
        {
            GooglePage googlePage = new GooglePage(BaseURL);

            googlePage.NavigationToPage();
            googlePage.Pesquisar("ps3brasil");
            googlePage.TakeScreeshot("Teste", ErrorPath);
        }
    }
}
