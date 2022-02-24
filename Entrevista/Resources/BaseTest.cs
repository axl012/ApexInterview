using Allure.Commons;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Entrevista.Resources
{
    public abstract class BaseTest 
    {
        protected BrowserName BrowserTest { get; }
        protected BasePage Page { get; set; }
        protected JObject TestInit { get; set; }

        /// <summary>
        /// Constructor inicial, se manda a llamar cuando inicia un test
        /// </summary>
        /// <param name="browser"></param>
        public BaseTest(BrowserName browser)
        {
            BrowserTest = browser;
            string initFile = Directory.GetCurrentDirectory();
         
            initFile += "\\Resources\\TestInitDefault.json";
    
            using (StreamReader r = new StreamReader(initFile, Encoding.GetEncoding("iso-8859-1")))//Utilizamos una codificación ampliada para leer caracteres especiales
            {
                TestInit ??= JObject.Parse(r.ReadToEnd());
            }
        }//

        /// <summary>
        /// Constructor de referencia, se utiliza cuando utilizamos un Test dentro de otro Test, 
        /// nos ayuda a pasar las referencias necesarias para que el Test coexista con otros Test.
        /// </summary>
        /// <param name="baseTest"></param>
        public BaseTest(BaseTest baseTest)
        {
            BrowserTest = baseTest.BrowserTest;
            Page = baseTest.Page;
            TestInit = baseTest.TestInit;
        }

        /// <summary>
        /// Setup inicial del Test, solamente se manda a llamar una sola vez durante el inicio del ciclo de vida del Test y se encarga de
        /// crear el TestKey, elecutar los browsers, crear drivers, inicializar variables y ejecutar el login. 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string browserInit = getTestInitAttribute("Test", "Browser");
            if (!browserInit.Equals("All") && !browserInit.Contains(BrowserTest.ToString()))
            {//Si el browser no se encuentra en el TestInit no se ejecuta
                Assert.Inconclusive(BrowserTest + " Is not in TestInit");
            }
            else if (BrowserTest == BrowserName.IE)
            {
                Thread.Sleep(10000);//IE tiene que ser el ultimo navegador en abrir, si no marca error...
            }

            var factory = new PageFactory();
            var driver = factory.CreateWebDriver(BrowserTest);
            Page = new BasePage(driver, TestInit);//se crea una instancia de la pagina base
            Page.open();
        }
        /// <summary>
        /// TearDown se ejecuta al finalizar el ciclo de vida del Test
        /// se encarga de eliminar productos creados durante el Test, lanzar reportes y cerrar la sesion del navegador.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (bool.Parse(getTestInitAttribute("Test", "CloseBrowserAtTheEnd")))
            {
                Page.close();
                Page.quit();
            }
        }

        /// <summary>
        /// Accede a los atributos guardados en el archivo TestInitDefault
        /// </summary>
        /// <param name="TestInitName"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string getTestInitAttribute(string TestInitName, string attribute)
        {
            return (string)TestInit[TestInitName][attribute];
        }
    }
}
