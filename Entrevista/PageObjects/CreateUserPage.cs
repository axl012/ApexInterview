using Entrevista.Resources;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Entrevista.PageObjects
{
    class CreateUserPage : Elements
    {
        public CreateUserPage(BasePage page) : base(page)
        {
        }

        public void insertEmailAndPassword()
        {
            var rd = RandomString();
            getElement("txtEmail").SendKeys(rd + "@hotmail.com");
            getElement("txtPassword").SendKeys(getTestInitAttribute("User", "CreateNewUserPassword"));
            getElement("btnCreateAccount").Click();
        }

        public void fillPersonalData()
        {
            getElement("txtNombre").SendKeys(getTestInitAttribute("User", "ValidFirstName"));
            getElement("txtPaterno").SendKeys(getTestInitAttribute("User", "ValidLastName1"));
            getElement("txtMaterno").SendKeys(getTestInitAttribute("User", "ValidLastName2"));
            changeDropDownValue(getElement("btnDay"),"12");
            changeDropDownValue(getElement("btnMonth"), "Abr");
            changeDropDownValue(getElement("btnYear"), "1997");
            getElementById("male").Click();
            getElement("btnCrear").Click();
        }

        public void selectDay(string day)
        {
            
            //create select element object 
            var selectDay = new SelectElement(getElement("btnDay"));
            //select by value
            selectDay.SelectByValue(day);
        }

        public bool verifyCreateUserPage()
        {
            bool isLoginPage = false;
            if (getElement("lblCreateAccount").Text.Contains("Crear cuenta"))
            {
                isLoginPage = true;
            }
            return isLoginPage;
        }

        public bool verifyCreateUserFormPage()
        {
            bool iFormPage = false;
            if (getElement("FormTitle").Text.Contains("Ingresa los siguientes datos"))
            {
                iFormPage = true;
            }
            return iFormPage;
        }
        public bool verifyCellphoneVerificationPage()
        {
            bool isVerificationPage = false;
            if (getElement("lblCellphoneVerification").Text.Contains("Verificación de celular"))
            {
                isVerificationPage = true;
            }
            return isVerificationPage;
        }
        public static string RandomString()
        {
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            int length = 5;
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
