using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUIFramework.Test.Pages;
using WVB.TestUIFramework.Enums;
using WVB.TestUIFramework.TestCase;

namespace TestUIFramework.Test.TestCases
{
    [TestClass]
    public class SeleniumTest : BaseUITest
    {
        public SeleniumTest() : base (Browsers.Chrome, "Drivers")
        {
        }

        [TestInitialize]
        public void Initialize()
        {
            base.Initialize(true);
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
        }
    }
}
