using Entrevista.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entrevista.PageObjects
{
    class ProductDetailsPage : Elements
    {
        public ProductDetailsPage(BasePage page) : base(page)
        {
        }

        public void clickAddToCartButton()
        {
            try
            {
                IWebElement btnAdd = getElement("btnAddToCart");
                btnAdd.Click();
            }
            catch
            {
                IWebElement btnAdd = getElement("btnAddToCart");
                btnAdd.Click();
            }
        }

        public bool VerifySuccessMessage()
        {
            bool messageIsPresent = false;
            if (getElement("lblSuccessMessage").Text.Contains("Agregaste un producto a tu bolsa"))
            {
                messageIsPresent = true;
            }
            return messageIsPresent;
        }
    }
}
