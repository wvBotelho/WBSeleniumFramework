using OpenQA.Selenium;
using System.Collections.Generic;

namespace WVB.TestUIFramework.Interfaces
{
    public interface ISeleniumActions
    {
        /// <summary>
        /// Seleciona uma opção em um menu de contexto
        /// </summary>
        /// <param name="element">Elemento que será clicado para ativar o menu de contexto</param>
        /// <param name="selectedOption">Elemento do menu de contexto que será selecionado</param>
        void SelectOptionOnContextMenu(IWebElement element, IWebElement selectedOption);

        /// <summary>
        /// Seleciona um opção em um autocomplete
        /// </summary>
        /// <param name="element">Campo texto autocomplete</param>
        /// <param name="text">Iniciais do texto que será usado</param>
        /// <param name="selectedOption">Elemento do menu do autocomplete</param>
        void SelectAutoComplete(IWebElement element, string text, IWebElement selectedOption);

        /// <summary>
        /// Move um elemento para outro elemento
        /// </summary>
        /// <param name="element">Elemento que será movido</param>
        /// <param name="destinationElement">Elemento destino</param>
        void MoveElementToElement(IWebElement element, IWebElement destinationElement);

        /// <summary>
        /// Seleciona várias opções em um dropdown
        /// </summary>
        /// <param name="elements">Elementos que serão selecionados</param>
        void SelectMultipleOptions(IReadOnlyCollection<IWebElement> elements);        
    }
}
