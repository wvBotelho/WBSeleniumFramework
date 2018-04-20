using OpenQA.Selenium;
using System.Collections.Generic;

namespace WVB.TestUIFramework.Interfaces
{
    /// <summary>
    /// Define a interface ao qual o usuário irá interagir com os elementos de uma página
    /// </summary>
    public interface IBasePageControls
    {
        /// <summary>
        /// Executa um comando Javascript na página atual
        /// </summary>
        /// <param name="js">Código Javascript a ser executado</param>
        /// <param name="parametros">Argumentos do Script</param>
        /// <returns>O valor retornado pelo Script</returns>
        object ExecutedJavascript(string js, params object[] parametros);

        /// <summary>
        /// Executa um comanda Assíncrono Javascript na página atual
        /// </summary>
        /// <param name="js">Código Javascript a ser executado</param>
        /// <param name="parametros">Argumentos do Script</param>
        /// <returns>O valor retornado pelo Script</returns>
        object ExecutedAsyncJavascript(string js, params object[] parametros);

        /// <summary>
        /// Tira um print da página atual e salva no arquivo especificado
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        /// <param name="filePath">Diretório onde a imagem será salva</param>
        /// /// <param name="format">Formato da imagem salva. Opcional</param>
        void TakeScreeshot(string nomeArquivo, string filePath, ScreenshotImageFormat format = ScreenshotImageFormat.Jpeg);

        /// <summary>
        /// Encontra o primeiro elemento no DOM usando o seletor informado
        /// </summary>
        /// <param name="by">Seletor para encontrar o elemento</param>
        /// <returns></returns>
        IWebElement FindElement(By by);

        /// <summary>
        /// Encontra todos os elementos no DOM usando o seletor informado
        /// </summary>
        /// <param name="by">Seletor para encontrar o elemento</param>
        /// <returns></returns>
        IReadOnlyCollection<IWebElement> FindElements(By by);

        /// <summary>
        /// Aceita um alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        void AceitarAlert(IWebElement element);

        /// <summary>
        /// Recusa um alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        void RecusarAlert(IWebElement element);

        /// <summary>
        /// Pega o texto que está presente no alert
        /// </summary>
        /// <param name="element">Elemento que levanta o alert</param>
        /// <returns>Retorna o texto</returns>
        string GetAlertMessage(IWebElement element);

        /// <summary>
        /// Faz o Download do arquivo na url especificada
        /// </summary>
        /// <param name="url">URL onde se encontra o arquivo</param>
        /// <param name="localPath">Local que o arquivo será baixado</param>
        void DownloadFile(string url, string localPath);
    }
}
