using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using WVB.TestUIFramework.Helpers;
using WVB.TestUIFramework.Interfaces;

namespace WVB.TestUIFramework.Action
{
    /// <summary>
    /// Classe de interações avançadas do usuário
    /// </summary>
    public class SeleniumActions : ISeleniumActions
    {
        public Actions Actions { get; set; }

        public WebDriverWait Wait { get; set; }

        /// <summary>
        /// Instancia uma nova instância da classe <see cref="SeleniumActions"/>
        /// </summary>
        /// <param name="driver">Driver de acesso ao DOM</param>
        public SeleniumActions(IWebDriver driver, WebDriverWait wait)
        {
            Actions = new Actions(driver);
            Wait = wait;
        }

        /// <summary>
        /// Seleciona uma opção em um menu de contexto
        /// </summary>
        /// <param name="element">Elemento que será clicado para ativar o menu de contexto</param>
        /// <param name="selectedOption">Elemento do menu de contexto que será selecionado</param>
        public void SelectOptionOnContextMenu(IWebElement element, IWebElement selectedOption)
        {
            ValidateElements.ValidaWebElements(new List<IWebElement>()
            {
                element, selectedOption
            });

            IAction contextMenu = Actions.MoveToElement(element)
                                  .ContextClick(element)
                                  .MoveToElement(selectedOption)
                                  .SendKeys(Keys.Enter)
                                  .Build();

            contextMenu.Perform();
        }

        /// <summary>
        /// Seleciona um opção em um autocomplete
        /// </summary>
        /// <param name="element">Campo texto autocomplete</param>
        /// <param name="text">Iniciais do texto que será usado</param>
        /// <param name="selectedOption">Elemento do menu do autocomplete</param>
        public void SelectAutoComplete(IWebElement element, string text, IWebElement selectedOption)
        {
            ValidateElements.ValidaWebElement(element);

            element.SendKeys(text);

            Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver target) =>
            {
                return selectedOption.Displayed;
            });

            Wait.Until(waitForElement);

            IAction selectAutoComplete = Actions.MoveToElement(selectedOption)
                                          .SendKeys(Keys.Enter)
                                          .Build();

            selectAutoComplete.Perform();
        }

        /// <summary>
        /// Move um elemento para outro elemento
        /// </summary>
        /// <param name="element">Elemento que será movido</param>
        /// <param name="destinationElement">Elemento destino</param>
        public void MoveElementToElement(IWebElement element, IWebElement destinationElement)
        {
            ValidateElements.ValidaWebElements(new List<IWebElement>()
            {
                element,
                destinationElement
            });

            IAction dragAndDrop = Actions.ClickAndHold(element)
                                  .MoveToElement(destinationElement)
                                  .Release(destinationElement)
                                  .Build();

            dragAndDrop.Perform();
        }

        public void SelectMultipleOptions(IReadOnlyCollection<IWebElement> elements)
        {

        }
    }
}
