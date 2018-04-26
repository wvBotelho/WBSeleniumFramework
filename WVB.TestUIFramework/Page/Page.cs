using OpenQA.Selenium.Support.PageObjects;
using WVB.TestUIFramework.Configuration;

namespace WVB.TestUIFramework.Page
{
    public static class Page
    {
        public static TPage GetPage<TPage>() where TPage : new()
        {
            var page = new TPage();
            PageFactory.InitElements(SeleniumConfiguration.Driver, page);
            return page;
        }
    }
}
