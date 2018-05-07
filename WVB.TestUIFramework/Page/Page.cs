using OpenQA.Selenium.Support.PageObjects;
//using SeleniumExtras.PageObjects;
using WVB.TestUIFramework.Configuration;

namespace WVB.TestUIFramework.Page
{
    /// <summary>
    /// Classe genérica que instancia uma nova page passada pelo usuário
    /// </summary>
    public static class Page
    {
        /// <summary>
        /// Pega uma nova instância da page especificada
        /// </summary>
        /// <typeparam name="TPage">Page ao qual será criada a nova instância</typeparam>
        /// <returns>Uma nova instância da page especificada</returns>
        public static TPage GetPage<TPage>() where TPage : new()
        {
            //TODO: Testar o novo diretório do pageFactory (SeleniumExtras)
            var page = new TPage();
            PageFactory.InitElements(SeleniumConfiguration.Driver, page);
            return page;
        }
    }
}
