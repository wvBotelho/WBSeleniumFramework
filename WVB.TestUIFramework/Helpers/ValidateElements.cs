using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace WVB.TestUIFramework.Helpers
{
    /// <summary>
    /// Classe que valida se os elementos no DOM estão válidos para serem interagidos
    /// </summary>
    public class ValidateElements
    {
        /// <summary>
        /// Valida se o elemento está válido para ser interagido
        /// </summary>
        /// <param name="element">Elemento a ser validado</param>
        public static void ValidaWebElement(IWebElement element)
        {
            if (element == null)
                throw new ArgumentException("Elemento não foi encontrado.");

            //if (!element.Displayed)
            //    throw new NoSuchElementException("Elemento não está visível no DOM.");

            if (!element.Enabled)
                throw new ElementNotInteractableException("Elemento está em modo readonly.");
        }

        /// <summary>
        /// Valida se os elementos na collections estão válidos para serem interagidos
        /// </summary>
        /// <param name="elements">Elementos a serem validados</param>
        public static void ValidaWebElements(IReadOnlyCollection<IWebElement> elements)
        {
            foreach (IWebElement element in elements)
            {
                ValidaWebElement(element);
            }
        }
    }
}
