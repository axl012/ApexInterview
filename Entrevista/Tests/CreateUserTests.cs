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
   // [Parallelizable(ParallelScope.Fixtures)]
    class CreateUserTests : BaseTest
    {
        public CreateUserTests(BrowserName browser) : base(browser) { }

        [Test]
        [Description("VerIfies that the user can go to the create user page")]
        public void VerifyUserPageWorks()
        {
            var loginPage = new LoginPage(Page);
            var createUserPage = new CreateUserPage(Page);
            loginPage.goToLoginPage();
            Assert.IsTrue(loginPage.verifyLoginPage());
            loginPage.goToCreateUserPage();
            Assert.IsTrue(createUserPage.verifyCreateUserPage());
        }

        [Test]
        [Description("VerIfies that the user can create a new user account")]
        public void VerifyUserCanCreateAccount()
        {
            var loginPage = new LoginPage(Page);
            var createUserPage = new CreateUserPage(Page);
            loginPage.goToLoginPage();
            Assert.IsTrue(loginPage.verifyLoginPage());
            loginPage.goToCreateUserPage();
            Assert.IsTrue(createUserPage.verifyCreateUserPage());
            createUserPage.insertEmailAndPassword();
            Assert.IsTrue(createUserPage.verifyCreateUserFormPage());
            createUserPage.fillPersonalData();
            Assert.IsTrue(createUserPage.verifyCellphoneVerificationPage());
        }
    }
}
