using Entrevista.PageObjects;
using Entrevista.Resources;
using NUnit.Framework;

namespace Entrevista.Tests
{

    [TestFixture(BrowserName.CH)]
    //[Parallelizable(ParallelScope.Fixtures)]
    public class SearchBarTests : BaseTest
    {
        public SearchBarTests(BrowserName browser) : base(browser) { }

        [Test]
        [Description("VerIfy that the search bar shows popular items while typing")]
        public void VerifySearchBarContainsPopularProducts()
        {
            var searchBarPage = new SearchBar(Page);
            searchBarPage.typeValidProduct();
            Assert.IsTrue(searchBarPage.verifyLoMasBuscadoTitle());
            Assert.IsTrue(searchBarPage.verifyItemIsInSearchBar());

        }

        [Test]
        [Description("VerIfy that the search bar shows brand section")]
        public void VerifySearchBarContainsBrands()
        {
            var searchBarPage = new SearchBar(Page);
            searchBarPage.typePartialProductName("nintendo switch");
            Assert.IsTrue(searchBarPage.verifySideBarShowsBrands());

        }

        [Test]
        [Description("VerIfy that the search bars shows correct results")]
        public void VerifySearchBarShowsCorrectResults()
        {
            var searchBarPage = new SearchBar(Page);
            var cataloguePage = new CatalogueProductPage(Page);
            searchBarPage.typeValidProduct();
            searchBarPage.clickSearchButton();
            Assert.IsTrue(cataloguePage.VerifySearchResults());

        }

        [Test]
        [Description("VerIfy that the user gets notified when an item is not found")]
        public void VerifyIfNotifiedWhenItemIsNotFound()
        {
            var searchBarPage = new SearchBar(Page);
            var cataloguePage = new CatalogueProductPage(Page);
            searchBarPage.typeNonExistenProduct();
            searchBarPage.clickSearchButton();
            Assert.IsTrue(cataloguePage.VerifyNoresultsMessage());

        }
    }
}