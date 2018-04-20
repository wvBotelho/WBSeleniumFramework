using OpenQA.Selenium.Support.UI;

namespace WVB.TestUIFramework.Interfaces
{
    /// <summary>
    /// Define uma interface ao qual o usuário irá ter acesso a propriedades comuns de uma página
    /// </summary>
    public interface IBasePageBasic
    {
        /// <summary>
        /// Pega o título do browser atual aberto
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Pega a URL da página atual
        /// </summary>
        string URL { get; }

        /// <summary>
        /// Pega a fonte da página atual
        /// </summary>
        string PageSource { get; }

        /// <summary>
        /// URL base para acesso a página
        /// </summary>
        string BaseURL { get; set; }

        /// <summary>
        /// Explicit Waits: Aguarda uma condição ser verdadeira para executar um comando
        /// </summary>
        WebDriverWait Wait { get; set; }
    }
}
