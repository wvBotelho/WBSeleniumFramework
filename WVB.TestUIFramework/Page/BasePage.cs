using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WVB.TestUIFramework.Action;
using WVB.TestUIFramework.Configuration;
using WVB.TestUIFramework.Enums;
using WVB.TestUIFramework.Helpers;
using WVB.TestUIFramework.Interfaces;
using WVB.TestUIFramework.SeleniumCustomExceptions;

namespace WVB.TestUIFramework.Page
{
    /// <summary>
    /// Page genérica com as funções básicas de acesso ao DOM, usando Selenium WebDriver e PageObject  
    /// </summary>
    public abstract class BasePage : IBasePage
    {
        #region Propriedades

        /// <summary>
        /// Pega o Driver que dá acesso ao controle do browser, podendo acessar os elementos de uma página 
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
        public string BaseURL
        {
            get
            {
                return SeleniumConfiguration.BaseURL;
            }
            set
            {
                SeleniumConfiguration.BaseURL = value;
            }
        }

        /// <summary>
        /// Explicit Waits: Aguarda uma condição ser verdadeira para executar um comando
        /// </summary>
        public WebDriverWait Wait { get; set; }

        /// <summary>
        /// Pega uma nova instancia da classe helper <see cref="SeleniumActions"/>
        /// </summary>
        protected ISeleniumActions Actions { get; private set; }

        #endregion

        #region Construtor

        /// <summary>
        /// Instancia uma nova instância da classe <see cref="BasePage"/>
        /// </summary>
        /// <param name="timeout">O valor indicando quanto tempo esperar para executar certas ações no Browser.</param>
        public BasePage(TimeoutExplicitWait timeout)
        {
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds((int)timeout));
            Actions = new SeleniumActions(Driver, Wait);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Navega para a página desejada
        /// </summary>
        /// <exception cref="NoSuchWindowException">Joga uma exceção do tipo <see cref="NoSuchWindowException"/> caso a página não seja encontrada.</exception>
        public void NavigationToPage()
        {
            try
            {
                Driver.Navigate().GoToUrl(BaseURL);

                if (!URL.Equals(BaseURL))
                    throw new NoSuchWindowException($"Página não encontrada. URL atual: {URL}");
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverTimeoutException(e.Message.ToString());
            }
        }

        /// <summary>
        /// Navega para a página desejada
        /// </summary>
        /// <param name="url">Parte da URL para ser concatenada com a BaseURL</param>
        public void NavigationPage(string url)
        {
            try
            {
                BaseURL = BaseURL + url;
                Driver.Navigate().GoToUrl(BaseURL);

                if (!URL.Equals(BaseURL))
                    throw new NoSuchWindowException($"Página não encontrada. URL atual: {URL}");
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverTimeoutException(e.Message.ToString());
            }
        }

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
        /// <param name="filePath">Diretório onde a imagem será salva</param>
        /// <param name="format">Formato da imagem salva</param>
        public void TakeScreeshot(string nomeArquivo, string filePath, ScreenshotImageFormat format)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentException("É necessário informar um diretório onde o arquivo será salvo.");

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                ITakesScreenshot screenshot = Driver as ITakesScreenshot;
                Screenshot shot = screenshot.GetScreenshot();

                shot.SaveAsFile(ScreenShotHelper.FormatFileName(nomeArquivo, filePath, format), format);
            }
            catch (WebDriverException e)
            {
                throw new WebDriverException(e.Message.ToString());
            }            
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
                throw new NoSuchElementException(e.Message.ToString());
            }
        }

        /// <summary>
        /// Aceita um alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        public void AceitarAlert(IWebElement element)
        {
            try
            {
                ValidateElements.ValidaWebElement(element);

                ExecutedJavascript("arguments[0].click()", element);

                Wait.Until(ExpectedConditions.AlertIsPresent());

                IAlert alert = Driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException e)
            {
                throw new NoAlertPresentException(e.Message.ToString());
            }            
        }

        /// <summary>
        /// Aceita um alert
        /// </summary>
        public void AceitarAlert()
        {
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException e)
            {
                throw new NoAlertPresentException(e.Message.ToString());
            }
        }

        /// <summary>
        /// Recusa um alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        public void RecusarAlert(IWebElement element)
        {
            try
            {
                ValidateElements.ValidaWebElement(element);

                ExecutedJavascript("arguments[0].click()", element);

                Wait.Until(ExpectedConditions.AlertIsPresent());

                IAlert alert = Driver.SwitchTo().Alert();
                alert.Dismiss();
            }
            catch (NoAlertPresentException e)
            {
                throw new NoAlertPresentException(e.Message.ToString());
            }            
        }

        /// <summary>
        /// Pega o texto que está presente no alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        /// <returns>Retorna o texto presente no alert</returns>
        public string GetAlertMessage(IWebElement element)
        {
            try
            {
                ValidateElements.ValidaWebElement(element);

                ExecutedJavascript("arguments[0].click()", element);

                Wait.Until(ExpectedConditions.AlertIsPresent());

                IAlert alert = Driver.SwitchTo().Alert();

                return alert.Text;
            }            
            catch (NoAlertPresentException e)
            {
                throw new NoAlertPresentException(e.Message.ToString());
            }
        }

        /// <summary>
        /// Pega o texto que está presente no alert
        /// </summary>
        /// <returns>Retorna o texto presente no alert</returns>
        public string GetAlertMessage()
        {
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();

                return alert.Text;
            }
            catch (NoAlertPresentException e)
            {
                throw new NoAlertPresentException(e.Message.ToString());
            }
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
