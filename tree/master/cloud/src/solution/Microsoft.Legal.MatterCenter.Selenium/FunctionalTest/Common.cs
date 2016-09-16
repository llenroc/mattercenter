using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Threading;

namespace Microsoft.Legal.MatterCenter.Selenium
{
    public enum Selector { ID, CLASS, CSS_SELECTOR, XPATH, LINK_TEXT };
    public class Common
    {
        static IWebDriver webDriver = new InternetExplorerDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;

        public void Authenticate(IWebDriver webDriver)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
            webDriver.FindElement(By.Id(ConfigurationManager.AppSettings["UserIDTextBox"])).Click();
            scriptExecutor.ExecuteScript(ConfigurationManager.AppSettings["UserIDSelector"]);
            Thread.Sleep(5000);
            webDriver.FindElement(By.Id(ConfigurationManager.AppSettings["UserIDTextBox"])).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.Id(ConfigurationManager.AppSettings["UserIDTextBox"])).Clear();
            scriptExecutor.ExecuteScript(ConfigurationManager.AppSettings["UserPasswordSelector"]);
            Thread.Sleep(3000); 
            scriptExecutor.ExecuteScript(ConfigurationManager.AppSettings["KeepSignInButtonCheckBox"]);
            Thread.Sleep(2000);
            webDriver.FindElement(By.Id(ConfigurationManager.AppSettings["SignInButton"])).Click();
            Thread.Sleep(1000);
        }

        public bool ElementPresent(IWebDriver webDriver, string elementName, Selector elementType)
        {
            try
            {
                if (elementType == Selector.ID)
                {
                    webDriver.FindElement(By.Id(elementName));
                }
                else if (elementType == Selector.CLASS)
                {
                    webDriver.FindElement(By.ClassName(elementName));
                }
                else if (elementType == Selector.CSS_SELECTOR)
                {
                    webDriver.FindElement(By.CssSelector(elementName));
                }
                else if (elementType == Selector.XPATH)
                {
                    webDriver.FindElement(By.XPath(elementName));
                }
                else if (elementType == Selector.LINK_TEXT)
                {
                    webDriver.FindElement(By.LinkText(elementName));
                }
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Cleanup(IWebDriver webDriver)
        {
            webDriver.Quit();
        }

        //public void LogWrite(string logmessage)
        //{
        //   // string logfile = ConfigurationManager.AppSettings[Constants.LOGFILEPATH];
        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@logfile, true))
        //    {
        //        file.WriteLine(logmessage);
        //    }
        //}

        public IWebElement ElementProperty(IWebDriver webDriver, string elementName, int elementType)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(7));
            // Test the autocomplete response - Explicit Wait
            if (0 == elementType)
            {
                return wait.Until(x => x.FindElement(By.Id(elementName)));
            }
            else if (1 == elementType)
            {
                return wait.Until(x => x.FindElement(By.ClassName(elementName)));
            }
            else if (2 == elementType)
            {
                return wait.Until(x => x.FindElement(By.CssSelector(elementName)));
            }
            else if (3 == elementType)
            {
                return wait.Until(x => x.FindElement(By.XPath(elementName)));
            }
            else if (4 == elementType)
            {
                return wait.Until(x => x.FindElement(By.LinkText(elementName)));
            }
            return null;
        }

        public string getGridData(IWebDriver webDriver)
        {
            try
            {
                //checked All option
                webDriver.FindElement(By.CssSelector("div.AppContent")).Click();
                Thread.Sleep(3000);
                webDriver.FindElement(By.Id("columnPickerIcon")).Click();
                Thread.Sleep(3000);
                webDriver.FindElement(By.CssSelector("span.ms-Label.columnOptionName")).Click();
                Thread.Sleep(3000);
                webDriver.FindElement(By.CssSelector("div.AppContent")).Click();
                Thread.Sleep(3000);
                string datachunk = (ElementProperty(webDriver, "listViewContainer_Grid", 0)).Text;

                return datachunk;
            }
            catch (Exception)
            {
                return null;
            }
        }

        static public IWebDriver getDriver()
        {
            return webDriver;
        }

        public void getLogin(IWebDriver webDriver, string URL)
        {
            webDriver.Navigate().GoToUrl(URL);
            if (ElementPresent(webDriver, ConfigurationManager.AppSettings["UseAnotherAccount"], Selector.CLASS))
            {
                webDriver.FindElement(By.ClassName(ConfigurationManager.AppSettings["UseAnotherAccount"])).Click();
                Authenticate(webDriver);
            }
            else if (ElementPresent(webDriver, "ms-spo-solutionItem", Selector.CLASS))
            {
                webDriver.FindElement(By.LinkText("Click here to sign in with a different account to this site.")).Click();
                Thread.Sleep(5000);
                webDriver.FindElement(By.ClassName(ConfigurationManager.AppSettings["UseAnotherAccount"])).Click();
                Authenticate(webDriver);

            }
            else if (ElementPresent(webDriver, ConfigurationManager.AppSettings["UserIDTextBox"], 0))
            {
                Authenticate(webDriver);

            }
            Thread.Sleep(5000);
        }

    }
}
