using Allure.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Entrevista.Resources
{
    public class BasePage 
    {
        public IWebDriver BaseDriver { get; set; }
        protected JObject BaseElements { get; set; }
        protected JObject BaseTestInit { get; set; }
        protected bool TestVelocityOn { get; set; }
        protected int TestVelocity { get; set; }

        public BasePage(IWebDriver driver, JObject testInit)//Constructor de inicio. Es utilizado al momento de inicar el Test
        {
            BaseDriver = driver;
            BaseTestInit = testInit;
            TestVelocity = int.Parse(getTestInitAttribute("Test", "TestVelocityInMilliseconds"));
            TestVelocityOn = TestVelocity > 0;
        }
        public BasePage(string jsonFile, BasePage page)//Constructor de modulos. Es utilizado por cada modulo del sistema para inicializar su archivo json correspondiente
        {
            BaseElements = JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + jsonFile));
            BaseDriver = page.BaseDriver;
            BaseTestInit = page.BaseTestInit;
            TestVelocityOn = page.TestVelocityOn;
            TestVelocity = page.TestVelocity;
        }

        internal void open(string url = "https://www.liverpool.com.mx/tienda/home")
        {
            BaseDriver.Manage().Window.Maximize();
            BaseDriver.Navigate().GoToUrl(url);
        }

        internal void close()
        {
            BaseDriver.Close();
        }
        internal void quit()
        {
            BaseDriver.Quit();
        }

        public IWebElement? getElement(string elementNameOrXpath, int waitSeconds = 10)
        {
            IWebElement element;
            if (TestVelocityOn)
            {
                Thread.Sleep(TestVelocity);
            }
            By elementFindBy = (elementNameOrXpath.Substring(0, 2).Equals("//") || elementNameOrXpath.Substring(0, 2).Equals("/h")) ? By.XPath(elementNameOrXpath) : By.XPath((string)BaseElements[elementNameOrXpath]["XPath"]);
            try
            {
                if (checkIfElementExist(elementNameOrXpath, waitSeconds))//si el elemento no existe, se espera unos segundos a que aparezca 
                {
                    if (checkIfElementInteractable(elementFindBy))//si el elemento existe y se puede interactuar con el, se regresa el elemento
                    {
                        element = BaseDriver.FindElement(elementFindBy);
                    }
                    else//si no se puede interactuar con el elemento esperamos a que aparezca 
                    {
                        element = getElementAfterDisplay(elementFindBy, waitSeconds);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            return element;
        }

        public IWebElement getElementById(string elementId)
        {
           IWebElement element = BaseDriver.FindElement(By.Id(elementId));
            return element;
        }

        public bool checkIfElementExist(string elementNameOrXpath, int waitTime = 10)
        {
            By elementFindBy = (elementNameOrXpath.Substring(0, 2).Equals("//") || elementNameOrXpath.Substring(0, 2).Equals("/h")) ? By.XPath(elementNameOrXpath) : By.XPath((string)BaseElements[elementNameOrXpath]["XPath"]);
            var wait = new WebDriverWait(BaseDriver, new TimeSpan(0, 0, waitTime));
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = BaseDriver.FindElement(elementFindBy);
                    return elementToBeDisplayed != null;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            return element;
        }

        private bool checkIfElementInteractable(By elementFindBy)
        {
            var element = BaseDriver.FindElement(elementFindBy);
            return (element != null && element.Displayed && element.Enabled);
        }

        private IWebElement getElementAfterDisplay(By elementFindBy, int waitSeconds)
        {
            new WebDriverWait(BaseDriver, TimeSpan.FromSeconds(waitSeconds))
                              .Until(drv => BaseDriver.FindElement(elementFindBy).Displayed);
            return BaseDriver.FindElement(elementFindBy);
        }

        public void changeInputValue(IWebElement element, string value)
        {
            if (value != null)
            {
                element.Clear();
                element.SendKeys(value);
            }
        }

        public void pressEnter(IWebElement element)
        {
            element.SendKeys(Keys.Return);
        }

        public void clickElement(IWebElement element, bool clickByScript = false)//Workaround de click para Internet Explorer
        {
            try
            {
                if (clickByScript)
                {
                    throw new Exception();
                }
                element.Click();
            }
            catch
            {
                BaseDriver.ExecuteJavaScript("arguments[0].click();", element);
            }
        }

        public void changeDropDownValue(IWebElement element, string value)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByValue(value);
        }

        public void closeEmergentWindow()
        {
            try
            {
                getElement("ventana").Click();
            }
            catch
            {
                Console.WriteLine("Emergent window not visible");
            }
        }

        public string getTestInitAttribute(string TestInitName, string attribute)
        {
            return (string)BaseTestInit[TestInitName][attribute];
        }
    }
}
