using Entrevista.PageObjects;
using Entrevista.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Entrevista.Tests
{
    [TestFixture(BrowserName.CH)]
    //[Parallelizable(ParallelScope.Fixtures)]
    public class CaralogueTests : BaseTest
    {
        public CaralogueTests(BrowserName browser) : base(browser) { }

        [Test]
        [Description("VerIfy that the user can select a product")]
        public void verifyUserCanSelectItem()
        {
            var searchBarPage = new SearchBar(Page);
            var cataloguePage = new CatalogueProductPage(Page);
            searchBarPage.typeValidCatalogueProduct();
            searchBarPage.clickSearchButton();
            cataloguePage.selectValidItem();
            Assert.IsTrue(cataloguePage.verifyAddToCartButtonExsts());
        }

        [Test]
        [Description("VerIfies that the user can add an aitem to the shopping cart")]
        public void verifyUserCanAddItemToCart()
        {
            var searchBarPage = new SearchBar(Page);
            var cataloguePage = new CatalogueProductPage(Page);
            var productDetailPage = new ProductDetailsPage(Page);
            searchBarPage.typeValidCatalogueProduct();
            searchBarPage.clickSearchButton();
            cataloguePage.selectValidItem();
            productDetailPage.clickAddToCartButton();
            Assert.IsTrue(productDetailPage.VerifySuccessMessage());
        }

        [Test]
        [Description("VerIfies that the user can add an aitem to the shopping cart based on brand, size and price")]
        public void verifyUserCanFilterAndAddItemToCart()
        {
            var searchBarPage = new SearchBar(Page);
            var cataloguePage = new CatalogueProductPage(Page);
            var productDetailPage = new ProductDetailsPage(Page);
            searchBarPage.typeValidCatalogueProduct();
            searchBarPage.clickSearchButton();
            cataloguePage.selectPriceRange();
            cataloguePage.selectScreenSize();
            cataloguePage.selectBrandCheckbox();
            cataloguePage.selectFirstTv();
            productDetailPage.clickAddToCartButton();
            Assert.IsTrue(productDetailPage.VerifySuccessMessage());
        }

    }
}
