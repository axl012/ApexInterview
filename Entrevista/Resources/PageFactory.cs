using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Entrevista.Resources
{
    class PageFactory
    {
        public IWebDriver CreateWebDriver(BrowserName browserName)
        {
            string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            switch (browserName)
            {
                case BrowserName.CH:
                    var op = new ChromeOptions { AcceptInsecureCertificates = true };
                    return new ChromeDriver(outPutDirectory, op);
                case BrowserName.FF:
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(outPutDirectory, "geckodriver.exe");
                    service.FirefoxBinaryPath = Environment.GetEnvironmentVariable("FireFoxPath");
                    var op2 = new FirefoxOptions { AcceptInsecureCertificates = true };
                    return new FirefoxDriver(service, op2);
                case BrowserName.IE:
                    var op3 = new InternetExplorerOptions
                    {
                        //AcceptInsecureCertificates = true,
                        RequireWindowFocus = true
                    };
                    return new InternetExplorerDriver(outPutDirectory, op3);
                case BrowserName.ED:
                    var op4 = new EdgeOptions { AcceptInsecureCertificates = true };
                    return new EdgeDriver(outPutDirectory, op4);
                default:
                    throw new ArgumentOutOfRangeException("No such browser exists");
            }
        }
    }

    public enum BrowserName
    {
        CH,//Chrome
        FF,//Firefox
        IE,//InternetExplorer
        ED//Edge
    }
}