using Microsoft.VisualStudio.TestTools.UnitTesting;
using WVB.TestUIFramework.TestCase;

namespace Teste.Teste
{
    [TestClass]
    public class UnitTest1 : BaseUITest
    {
        public UnitTest1() : base (WVB.TestUIFramework.Enums.Browsers.Chrome, "Chrome")
        {
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
