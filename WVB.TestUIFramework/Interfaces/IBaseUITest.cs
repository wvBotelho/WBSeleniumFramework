using System;
using WVB.TestUIFramework.Enums;

namespace WVB.TestUIFramework.Interfaces
{
    public interface IBaseUITest
    {
        // <summary>
        /// Configuração de inicialização dos testes
        /// </summary>        
        /// <param name="browsers">Browser que irá utilizar. Ex: Chrome, Edge, IE</param>
        /// <param name="folderPath">Diretório que está o caminho da pasta com o Driver</param>
        /// <param name="baseURL">URL base</param>
        /// <param name="tempo">Quantidade de tempo que o Driver irá aguardar (Milisegundos/Segundos/Minutos).</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless". Optional</param>
        void Initialize(Browsers browsers, string folderPath, string baseURL, TimeSpan tempo, params string[] options);

        /// <summary>
        /// Configuração de encerramento dos testes
        /// </summary>
        void CleanUp();
    }
}
