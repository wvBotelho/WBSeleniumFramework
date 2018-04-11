using System;
using System.Collections.Generic;

namespace WVB.TestUIFramework.Interfaces
{
    public interface ISeleniumConfiguration
    {
        /// <summary>
        /// Inicializa uma nova instância do ChromeDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Chrome</param>
        /// <param name="preferences">Preferências do usuário. Opcional</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless". Opcional</param>
        void ChromeInstance(string folderPath, Dictionary<string, object> preferences = null, params string[] options);

        /// <summary>
        /// Inicializa uma nova instância do EdgeDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Edge</param>
        void EdgeInstance(string folderPath);

        /// <summary>
        /// Inicializa uma nova instância do InternetExplorerDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Internet Explorer</param>
        void IEInstance(string folderPath);

        /// <summary>
        /// Inicializa uma nova instância do OperaDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Opera</param>
        /// <param name="preferences">Preferências do usuário. Opcional</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless"</param>
        void OperaInstance(string folderPath, Dictionary<string, object> preferences = null, params string[] options);

        /// <summary>
        /// Inicializa uma nova instância do SafariDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Safari</param>
        void SafariInstance(string folderPath);

        /// <summary>
        /// Inicializa uma nova instância do FirefoxDriver
        /// </summary>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless"</param>
        void FoxInstance(params string[] options);

        /// <summary>
        /// Configura o tempo máximo que o Driver aguarda Implicit waits
        /// </summary>
        /// <param name="timeSpan">Quantidade de tempo que o Driver irá aguardar (Milisegundos/Segundos/Minutos).</param>
        void ManagerImplicitWaitDriver(TimeSpan timeSpan);

        /// <summary>
        /// Maximiza o tamanho do browser
        /// </summary>
        void MaximizeWindow();

        /// <summary>
        /// Minimiza o tamanho do browser
        /// </summary>
        void MinimizeWindow();

        /// <summary>
        /// Encerra o Driver e fecha o browser
        /// </summary>
        void Quit();
    }
}
