using Entrevista.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Entrevista.PageObjects
{
    class LoginPage : Elements
    {
        public LoginPage(BasePage page) : base(page)
        {
        }

        public void goToLoginPage()
        {
            //getElement("ventana").Click();
            closeEmergentWindow();
            getElement("btnLogin").Click();
        }

        public void goToCreateUserPage()
        {
            getElement("lblCreateUser").Click();
        }

        public bool verifyLoginPage()
        {
            bool isLoginPage = false;
            if(getElement("lblLoginTitle").Text.Contains("Inicia sesión"))
            {
                isLoginPage = true;
            }
            return isLoginPage;
        }

        public bool verifyCreateUserPage()
        {
            bool isLoginPage = false;
            if (getElement("lblCreateUserTitle").Text.Contains("Crear cuenta"))
            {
                isLoginPage = true;
            }
            return isLoginPage;
        }
    }
}
