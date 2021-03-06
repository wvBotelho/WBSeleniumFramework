﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using TestUIFramework.Test.Pages;
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

        private ContextPage ContextPage
        {
            get
            {
                return Page.GetPage<ContextPage>();
            }
        }

        private GooglePage GooglePage
        {
            get
            {
                return Page.GetPage<GooglePage>();
            }
        }

        private AutoCompletePage AutoCompletePage
        {
            get
            {
                return Page.GetPage<AutoCompletePage>();
            }
        }

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
        public void SelecionarOpcaoEmUmAutoComplete()
        {
            AutoCompletePage.NavigateToAutoCompletePage("https://www.w3schools.com/howto/howto_js_autocomplete.asp");
            AutoCompletePage.SelecionarOpcaoEmAutoComplete("bra");
            Assert.AreEqual("Brazil", AutoCompletePage.GetTexto());

        }

        [TestMethod]
        public void SelecionarOpcaoNoMenuDeContexto()
        {
            ContextPage.NavigationToPage("http://medialize.github.io/jQuery-contextMenu/demo.html");
            ContextPage.SelectOptionInContextMenu();
            Assert.AreEqual("clicked: paste", ContextPage.GetAlertMessage(), "Era pra ser selecionado a opção paste");
        }
    }
}
