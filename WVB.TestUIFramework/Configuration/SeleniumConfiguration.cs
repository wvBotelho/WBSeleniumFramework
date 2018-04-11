﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.IO;
using WVB.TestUIFramework.Interfaces;

namespace WVB.TestUIFramework.Configuration
{
    /// <summary>
    /// Classe de configuração do Driver de manipulação do DOM
    /// </summary>
    public class SeleniumConfiguration : ISeleniumConfiguration
    {
        public static IWebDriver Driver { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância do ChromeDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Chrome</param>
        /// <param name="preferences">Preferências do usuário. Opcional</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless". Opcional</param>
        public void ChromeInstance(string folderPath, Dictionary<string, object> preferences = null, params string[] options)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("É necessário passar o caminho que está localizado o Driver do Browser");

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments(options);

            if (preferences != null)
            {
                foreach (KeyValuePair<string, object> preference in preferences)
                    chromeOptions.AddUserProfilePreference(preference.Key, preference.Value);
            }

            Driver = new ChromeDriver(folderPath, chromeOptions);
        }

        /// <summary>
        /// Inicializa uma nova instância do EdgeDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Edge</param>
        public void EdgeInstance(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("É necessário passar o caminho que está localizado o Driver do Browser");

            Driver = new EdgeDriver(folderPath);
        }

        /// <summary>
        /// Inicializa uma nova instância do InternetExplorerDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Internet Explorer</param>
        public void IEInstance(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("É necessário passar o caminho que está localizado o Driver do Browser");

            Driver = new InternetExplorerDriver(folderPath);
        }

        /// <summary>
        /// Inicializa uma nova instância do OperaDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Opera</param>
        /// <param name="preferences">Preferências do usuário. Opcional</param>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless"</param>
        public void OperaInstance(string folderPath, Dictionary<string, object> preferences = null, params string[] options)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("É necessário passar o caminho que está localizado o Driver do Browser");

            OperaOptions operaOptions = new OperaOptions();
            operaOptions.AddArguments(options);

            if (preferences != null)
            {
                foreach (KeyValuePair<string, object> preference in preferences)
                    operaOptions.AddUserProfilePreference(preference.Key, preference.Value);
            }

            Driver = new OperaDriver(folderPath, operaOptions);
        }

        /// <summary>
        /// Inicializa uma nova instância do SafariDriver
        /// </summary>
        /// <param name="folderPath">Pasta onde se encontra o Driver do Safari</param>
        public void SafariInstance(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("É necessário passar o caminho que está localizado o Driver do Browser");

            Driver = new SafariDriver(folderPath);
        }

        /// <summary>
        /// Inicializa uma nova instância do FirefoxDriver
        /// </summary>
        /// <param name="options">Opções para configurar na inicialização do navegador. Ex: "headless"</param>
        public void FoxInstance(params string[] options)
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArguments(options);

            Driver = new FirefoxDriver(firefoxOptions);
        }

        /// <summary>
        /// Configura o tempo máximo que o Driver aguarda Implicit waits
        /// </summary>
        /// <param name="timeSpan">Quantidade de tempo que o Driver irá aguardar (Milisegundos/Segundos/Minutos).</param>
        public void ManagerImplicitWaitDriver(TimeSpan timeSpan)
        {
            Driver.Manage().Timeouts().ImplicitWait = timeSpan;
            Driver.Manage().Timeouts().PageLoad = timeSpan;
            Driver.Manage().Timeouts().AsynchronousJavaScript = timeSpan;
        }

        /// <summary>
        /// Maximiza o tamanho do browser
        /// </summary>
        public void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Minimiza o tamanho do browser
        /// </summary>
        public void MinimizeWindow()
        {
            Driver.Manage().Window.Minimize();
        }

        /// <summary>
        /// Encerra o Driver e fecha o browser
        /// </summary>
        public void Quit()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                ITakesScreenshot screenshot = Driver as ITakesScreenshot;
                Screenshot shot = screenshot.GetScreenshot();

                shot.SaveAsFile($@"{folderPath}\Reports\Error", ScreenshotImageFormat.Jpeg);

                Driver.Quit();
            }
        }
    }
}
