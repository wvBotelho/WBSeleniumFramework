using System;
using System.Configuration;
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
        private ISeleniumConfiguration SeleniumConfiguration { get; set; }

        private string FolderPath { get; set; }

        /// <summary>
        /// Construtor da classe que recebe o browsers ao qual vai usar o Selenium WebDriver
        /// </summary>
        /// <param name="browsers">Browser que irá utilizar. Ex: Chrome, Edge, IE</param>
        /// <param name="indexName">Indice do ConfigurationManager que está o caminho da pasta com o Driver</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless". Optional</param>
        public BaseUITest(Browsers browsers, string indexName, params string[] options)
        {
            SeleniumConfiguration = new SeleniumConfiguration();

            FolderPath = ConfigurationManager.AppSettings[indexName];

            switch (browsers)
            {
                case Browsers.Chrome:
                    SeleniumConfiguration.ChromeInstance(FolderPath, null, options);
                    break;
                case Browsers.Edge:
                    SeleniumConfiguration.EdgeInstance(FolderPath);
                    break;
                case Browsers.IE:
                    SeleniumConfiguration.IEInstance(FolderPath);
                    break;
                case Browsers.Opera:
                    SeleniumConfiguration.OperaInstance(FolderPath, null, options);
                    break;
                case Browsers.Safari:
                    SeleniumConfiguration.SafariInstance(FolderPath);
                    break;
                default:
                    SeleniumConfiguration.FoxInstance(options);
                    break;
            }
        }

        /// <summary>
        /// Configuração de inicialização dos testes
        /// </summary>
        /// <param name="maximiza">Indica se a janela será maximizada, caso não tenha feito isso no pelo options do Driver. Optional</param>
        public void Initialize(bool maximiza = false)
        {
            TimeSpan tempo = TimeSpan.FromSeconds(10);
            SeleniumConfiguration.ManagerImplicitWaitDriver(tempo);

            if (maximiza)
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
