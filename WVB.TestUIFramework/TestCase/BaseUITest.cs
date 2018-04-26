using System;
using WVB.TestUIFramework.Configuration;
using WVB.TestUIFramework.Enums;
using WVB.TestUIFramework.Interfaces;

namespace WVB.TestUIFramework.TestCase
{
    /// <summary>
    /// Classe base para os testes usando a biblioteca Selenium WebDriver
    /// </summary>
    public abstract class BaseUITest : IBaseUITest
    {
        private SeleniumConfiguration SeleniumConfiguration { get; set; }

        /// <summary>
        /// Configuração de inicialização dos testes
        /// </summary>        
        /// <param name="browsers">Browser que irá utilizar. Ex: Chrome, Edge, IE</param>
        /// <param name="folderPath">Diretório que está o caminho da pasta com o Driver</param>
        /// <param name="baseURL">URL base</param>
        /// <param name="tempo">Quantidade de tempo que o Driver irá aguardar (Milisegundos/Segundos/Minutos).</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless". Optional</param>
        public virtual void Initialize(Browsers browsers, string folderPath, string baseURL, TimeSpan tempo, params string[] options)
        {
            SeleniumConfiguration = new SeleniumConfiguration();

            SeleniumConfiguration.InicializaDriver(browsers, folderPath, options);
            SeleniumConfiguration.BaseURL = baseURL;
            SeleniumConfiguration.ManagerImplicitWaitDriver(tempo);
            SeleniumConfiguration.MaximizeWindow();
        }

        /// <summary>
        /// Configuração de encerramento dos testes
        /// </summary>
        public void CleanUp()
        {
            SeleniumConfiguration.Quit();
        }
    }
}
