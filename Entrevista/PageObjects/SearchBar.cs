using Entrevista.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Entrevista.PageObjects
{
    class SearchBar : Elements
    {
        public SearchBar(BasePage page) : base(page)
        {
        }
        public void typeValidProduct(string productName = null)
        {
            productName =  getTestInitAttribute("SearchProduct", "ValidProduct");
            getElement("ventana").Click();
            closeEmergentWindow();
            getElement("txtmainSearchbar").SendKeys(productName);

        }
        public void typeValidCatalogueProduct(string productName = null)
        {
            productName = getTestInitAttribute("CatalogueProducts", "ValidItem");
            closeEmergentWindow();
            getElement("txtmainSearchbar").SendKeys(productName);
        }

        public void typePartialProductName(string productName = null)
        {
            productName = getTestInitAttribute("SearchProduct", "ValidPartialProduct");
            //   getElement("ventana").Click();
            closeEmergentWindow();
            getElement("txtmainSearchbar").SendKeys(productName);

        }

        public void typeNonExistenProduct(string productName = null)
        {
            productName = getTestInitAttribute("SearchProduct", "InvalidProduct");
            closeEmergentWindow();
            getElement("txtmainSearchbar").SendKeys(productName);

        }

        public bool verifyLoMasBuscadoTitle(string titulo = null)
        {
           bool isEqual = false;
           if( getElement("loMasBuscadoTitle").Text == "Lo más buscado")
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool verifyItemIsInSearchBar(string productoBuscado = null)
        {
            productoBuscado = getTestInitAttribute("SearchProduct", "ValidProduct");
            bool contains = false;
            if (getElement("seccionProductosBuscados").Text.Contains(productoBuscado))
            {
                contains = true;
            }
            return contains;
        }

        public bool verifySideBarShowsBrands(string productoBuscado = null)
        {
            productoBuscado = getTestInitAttribute("SearchProduct", "ValidPartialProduct");
            bool containsBrands = false;
            if (getElement("BrandSearch").Text.Contains("Buscar en Marcas"))
            {
                containsBrands = true;
            }
            return containsBrands;
        }

        public void clickSearchButton()
        {
            IWebElement btnSearch = getElement("btnSearch");
            btnSearch.Click();
        }
    }
}
