using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using WVB.TestUIFramework.Configuration;
using WVB.TestUIFramework.Interfaces;
using WVB.TestUIFramework.SeleniumCustomExceptions;

namespace WVB.TestUIFramework.Page
{
    /// <summary>
    /// Page genérica com as funções básicas de acesso ao DOM, usando Selenium WebDriver e PageObject  
    /// </summary>
    public abstract class BasePage : IBasePage, IBasePageControls
    {
        #region Propriedades

        /// <summary>
        /// Tem acesso ao controle do browser, podendo acessar o DOM de uma página 
        /// </summary>
        protected IWebDriver Driver
        {
            get
            {
                return SeleniumConfiguration.Driver;
            }
        }

        /// <summary>
        /// Pega o título do browser atual aberto
        /// </summary>
        public string Title
        {
            get
            {
                return Driver.Title;
            }
        }

        /// <summary>
        /// Pega a URL da página atual
        /// </summary>
        public string URL
        {
            get
            {
                return Driver.Url;
            }
        }

        /// <summary>
        /// Pega a fonte da página atual
        /// </summary>
        public string PageSource
        {
            get
            {
                return Driver.PageSource;
            }
        }

        /// <summary>
        /// URL base para acesso a página
        /// </summary>
        public string BaseURL { get; set; }

        /// <summary>
        /// Explicit Waits: Aguarda uma condição ser verdadeira para executar um comando
        /// </summary>
        public WebDriverWait Wait { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor da Page
        /// </summary>
        public BasePage()
        {
            //TODO: Testar o novo diretório do pageFactory (SeleniumExtras)

            PageFactory.InitElements(Driver, this);
            BaseURL = ConfigurationManager.AppSettings["BaseURL"];
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Executa um comando Javascript na página atual
        /// </summary>
        /// <param name="js">Código Javascript a ser executado</param>
        /// <param name="parametros">Argumentos do Script</param>
        /// <returns>O valor retornado pelo Script</returns>
        public object ExecutedJavascript(string js, params object[] parametros)
        {
            IJavaScriptExecutor javaScript = Driver as IJavaScriptExecutor;

            if (javaScript == null)
                throw new NotJavascriptConvertException("Não foi possível converter o Driver em javascript.");

            return javaScript.ExecuteScript(js, parametros);
        }

        /// <summary>
        /// Executa um comanda Assíncrono Javascript na página atual
        /// </summary>
        /// <param name="js">Código Javascript a ser executado</param>
        /// <param name="parametros">Argumentos do Script</param>
        /// <returns>O valor retornado pelo Script</returns>
        public object ExecutedAsyncJavascript(string js, params object[] parametros)
        {
            IJavaScriptExecutor javaScript = Driver as IJavaScriptExecutor;

            if (javaScript == null)
                throw new NotJavascriptConvertException("Não foi possível converter o Driver em javascript.");

            return javaScript.ExecuteAsyncScript(js, parametros);
        }

        /// <summary>
        /// Tira um print da página atual e salva no arquivo especificado
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        public void TakeScreeshot(string nomeArquivo)
        {
            string path = ConfigurationManager.AppSettings["PathError"];

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            ITakesScreenshot screenshot = Driver as ITakesScreenshot;
            Screenshot shot = screenshot.GetScreenshot();
            
            shot.SaveAsFile($@"{path}\{nomeArquivo}", ScreenshotImageFormat.Jpeg);
        }

        /// <summary>
        /// Encontra o primeiro elemento no DOM usando o seletor informado
        /// </summary>
        /// <param name="by">Seletor para encontrar o elemento</param>
        /// <returns></returns>
        public IWebElement FindElement(By by)
        {
            try
            {
                return Driver.FindElement(by);
            }
            catch (NoSuchElementException e)
            {
                TakeScreeshot("NoElementFound");
                throw new NoSuchElementException(e.Message.ToString());
            }
        }

        /// <summary>
        /// Encontra todos os elementos no DOM usando o seletor informado
        /// </summary>
        /// <param name="by">Seletor para encontrar o elemento</param>
        /// <returns></returns>
        public IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return Driver.FindElements(by);
            }
            catch (NoSuchElementException e)
            {
                TakeScreeshot("NoElementsFound");
                throw new NoSuchElementException(e.Message.ToString());
            }
        }

        /// <summary>
        /// Aceita um alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        public void AceitarAlert(IWebElement element)
        {
            if (element == null)
                throw new ArgumentException("Elemento não foi encontrado.");

            if (!element.Displayed)
                throw new NoSuchElementException("Elemento não está visivel no DOM.");

            ExecutedJavascript("arguments[0].click()", element);
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Accept();
        }

        /// <summary>
        /// Recusa um alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        public void RecusarAlert(IWebElement element)
        {
            if (element == null)
                throw new ArgumentException("Elemento não foi encontrado.");

            if (!element.Displayed)
                throw new NoSuchElementException("Elemento não encontrado");

            ExecutedJavascript("arguments[0].click()", element);
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Dismiss();
        }

        /// <summary>
        /// Pega o texto que está presente no alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        /// <returns>Retorna o texto</returns>
        public string GetAlertMessage(IWebElement element)
        {
            if (element == null)
                throw new ArgumentException("Elemento não foi encontrado.");

            if (!element.Displayed)
                throw new NoSuchElementException("Elemento não encontrado");

            ExecutedJavascript("arguments[0].click()", element);
            IAlert alert = Driver.SwitchTo().Alert();

            return alert.Text;
        }

        /// <summary>
        /// Faz o Download do arquivo na url especificada
        /// </summary>
        /// <param name="url">URL onde se encontra o arquivo</param>
        /// <param name="localPath">Local que o arquivo será baixado</param>
        public void DownloadFile(string url, string localPath)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(localPath))
                throw new ArgumentException("É necessário informar a URL do Arquivo e o Caminho da pasta onde o arquivo será baixado.");

            if (!Directory.Exists(localPath))
                Directory.CreateDirectory(localPath);

            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Cookie] = CookieString();

            webClient.DownloadFile(url, localPath);
        }

        private string CookieString()
        {
            IReadOnlyCollection<OpenQA.Selenium.Cookie> cookies = Driver.Manage().Cookies.AllCookies;

            return string.Join("; ", cookies.Select(cookie => $"{cookie.Name}={cookie.Value}"));
        }

        #endregion        
    }
}
