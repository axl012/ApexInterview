using Entrevista.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Entrevista.PageObjects
{
    class CatalogueProductPage : Elements
    {
        public CatalogueProductPage(BasePage page) : base(page)
        {
        }
        public bool VerifySearchResults(string product = null)
        {
            bool isResultCorrect = false;
            product = getTestInitAttribute("SearchProduct", "ValidProduct");
            if (getElement("CatalogueSection").Text.Contains(product))
            {
                isResultCorrect = true;
            }
            return isResultCorrect;
        }

        public void selectValidItem()
        {
            IWebElement btnItem = getElement("btnFirstItem");
            btnItem.Click();
        }

        public bool VerifyNoresultsMessage()
        {
            bool isNotFound = false;
            if (getElement("lblNoResults").Text.Contains("Tu Búsqueda \"tsuru volante azul 2009\" arrojó \"0\" resultados"))
            {
                isNotFound = true;
            }
            return isNotFound;
        }

        public bool verifyAddToCartButtonExsts()
        {
            bool exists = checkIfElementExist("btnAddToCart");
            return exists;
        }

        public void selectBrandCheckbox()
        {
            getElement("lblVerMas").Click();
            getElementById("brand-SAMSUNG").Click();
            getElementById("variants.normalizedSize-50 pulgadas");
        }

        public void selectScreenSize()
        {
             IWebElement element = BaseDriver.FindElement(By.Id("variants.normalizedSize-50 pulgadas"));
             element.Click();
        }

        public void selectPriceRange()
        {
            string minRange = getTestInitAttribute("CatalogueProducts", "PriceRange1");
            string maxRange = getTestInitAttribute("CatalogueProducts", "PriceRange2");
            getElement("txtMinPrice").SendKeys(minRange);
            getElement("txtMaxPrize").SendKeys(maxRange);
            getElement("enterPriceRange").Click();
        }

        public void selectFirstTv()
        {
            getElement("firstTV").Click();
        }

    }
}
