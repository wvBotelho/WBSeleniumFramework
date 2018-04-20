using System;

namespace WVB.TestUIFramework.Helpers
{
    /// <summary>
    /// Classe helper para as funções de ScreenShot
    /// </summary>
    public class ScreenShotHelper
    {
        /// <summary>
        /// Formata o nome do arquivo
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        /// <param name="pasta">O caminho da pasta, onde ele vai baixado</param>
        /// <returns>Retorna uma string com o nome do arquivo formatado</returns>
        public static string FormatFileName(string nomeArquivo, string pasta)
        {
            string fileName = $"{FormatName(nomeArquivo, pasta)}_{GetCurrentDateTime()}";

            return fileName;
        }

        /// <summary>
        /// Pega o tempo atual da máquina
        /// </summary>
        /// <returns>Retorna a data atual com horas e minutos</returns>
        private static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("dd-MM-yyyy_HH-mm");
        }

        /// <summary>
        /// Formata o nome do arquivo
        /// </summary>
        /// <param name="nomeArquivo">O nome do arquivo</param>
        /// <param name="pasta">O caminho da pasta, onde ele vai baixado</param>
        /// <returns>Retorna uma string com o nome do arquivpo formatado</returns>
        private static string FormatName(string nomeArquivo, string pasta)
        {
            if (string.IsNullOrEmpty(nomeArquivo) || string.IsNullOrEmpty(pasta))
                throw new ArgumentException("Não pode passar valores nulos");

            return $@"{pasta}\{nomeArquivo}";
        }
    }
}
