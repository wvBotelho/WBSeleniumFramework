using System;
using WVB.TestUIFramework.Enums;

namespace WVB.TestUIFramework.Interfaces
{
    public interface ISeleniumConfiguration
    {
        /// <summary>
        /// Inicializa uma nova instância do WebDriver
        /// </summary>
        /// <param name="browsers">Browser que irá utilizar. Ex: Chrome, Edge, IE</param>
        /// <param name="folderPath">Diretório que está o caminho da pasta com o Driver</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless". Optional</param>
        void InicializaDriver(Browsers browsers, string folderPath, params string[] options);

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
