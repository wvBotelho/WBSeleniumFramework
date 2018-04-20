using System;

namespace WVB.TestUIFramework.Interfaces
{
    public interface IBaseUITest
    {
        /// <summary>
        /// Configuração de inicialização dos testes
        /// </summary>
        /// <param name="tempo">Quantidade de tempo que o Driver irá aguardar (Milisegundos/Segundos/Minutos).</param>
        /// <param name="maximiza">Indica se a janela será maximizada, caso não tenha feito isso no pelo options do Driver. Optional</param>
        void Initialize(TimeSpan tempo, bool maximiza = false);

        /// <summary>
        /// Configuração de encerramento dos testes
        /// </summary>
        void CleanUp();
    }
}
