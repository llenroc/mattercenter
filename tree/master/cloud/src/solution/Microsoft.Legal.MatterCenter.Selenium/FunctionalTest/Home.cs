using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace Microsoft.Legal.MatterCenter.Selenium
{
    [Binding]
    public class Home
    {
        const string URL = "https://matterwebapp.azurewebsites.net";
        static IWebDriver webDriver = Common.getDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        Common common = new Common();

        [When(@"We will provide (.*) and (.*)")]
        public void WhenWeWillProvideAnd(string userName, string password)
        {
            common.getLogin(webDriver, URL);
            Assert.IsTrue(userName.Contains("matteradmin@msmatter.onmicrosoft.com"));
            Assert.IsTrue(password.Contains("P@$$w0rd01"));
        }

        [Then(@"The home page will be loaded '(.*)'")]
        public void ThenTheHomePageWillBeLoaded(string pageName)
        {
        }

        [Then(@"The home page will be loaded")]
        public void ThenTheHomePageWillBeLoaded()
        {
            string PageURL = (string)scriptExecutor.ExecuteScript("var links = window.location.href;return links");
            Assert.IsTrue(PageURL.Contains("#/navigation"));

        }

        [When(@"user click on HamberGer Menu")]
        public void WhenUserClickOnHambergerMenu()
        {
            webDriver.FindElement(By.ClassName("AppSwitcherContainer")).Click();
        }

        [Then(@"HamberGer Menu should display '(.*)','(.*)','(.*)' and '(.*)' menu")]
        public void ThenHambergerMenuShouldDisplayAndMenu(string selectHome, string selectMatter, string selectDocument, string selectProvision)
        {
            string home = (string)scriptExecutor.ExecuteScript("var links = $('.AppMenuFlyoutPriLinks a')[0].text;return links");
            string matters = (string)scriptExecutor.ExecuteScript("var links = $('.AppMenuFlyoutPriLinks a')[1].text;return links");
            string documents = (string)scriptExecutor.ExecuteScript("var links = $('.AppMenuFlyoutPriLinks a')[2].text;return links");
            string matterProvision = (string)scriptExecutor.ExecuteScript("var links = $('.AppMenuFlyoutPriLinks a')[3].text;return links");

            Assert.IsTrue(home.Equals(selectHome));
            Assert.IsTrue(matters.Equals(selectMatter));
            Assert.IsTrue(documents.Equals(selectDocument));
            Assert.IsTrue(matterProvision.Equals(selectProvision));
            Thread.Sleep(3000);
            webDriver.FindElement(By.ClassName("AppSwitcherContainer")).Click();
        }

        [When(@"user click on Learn more and dismiss link")]
        public void WhenUserClickOnLearnMoreAndDismissLink()
        {
            scriptExecutor.ExecuteScript("$('#HomeContainer > header > span > a')[1].click();");

        }

        [Then(@"It should dismiss the link")]
        public void ThenItShouldDismissTheLink()
        {
            string learnMoreLink = (string)scriptExecutor.ExecuteScript("var links = $('#HomeContainer > header > span > a').attr('href');return links");
            Assert.IsTrue(common.ElementPresent(webDriver, "RemoveWelcomeBar", Selector.CLASS));
            Assert.IsTrue(learnMoreLink.Equals("http://www.microsoft.com/mattercenter"));
        }

        [When(@"user click on Matters link")]
        public void WhenUserClickOnMattersLink()
        {
            scriptExecutor.ExecuteScript("$('.MattersContainer a figure figcaption')[0].click();");
            Thread.Sleep(4000);
            
        }

        [Then(@"It should open the Matter Search page")]
        public void ThenItShouldOpenTheMatterSearchPage()
        {
            string PageURL = (string)scriptExecutor.ExecuteScript("var links = window.location.href;return links");
            Assert.IsTrue(PageURL.Contains("#/matters"));
            webDriver.Navigate().GoToUrl(URL);
            Thread.Sleep(4000);
            string matters = (string)scriptExecutor.ExecuteScript("var links = $('.MattersContainer a figure figcaption')[0].innerText;return links");
            Assert.IsTrue(matters.Contains("Matters"));
        }

        [When(@"user click on Documents link")]
        public void WhenUserClickOnDocumentsLink()
        {
            scriptExecutor.ExecuteScript("$('.DocumentsContainer figure figcaption')[0].click();");
            Thread.Sleep(4000);
           
        }

        [Then(@"It should open the Document search page")]
        public void ThenItShouldOpenTheDocumentSearchPage()
        {
            string PageURL = scriptExecutor.ExecuteScript("var links = window.location.href; return links").ToString();
            Assert.IsTrue(PageURL.Contains("#/documents"));
            webDriver.Navigate().GoToUrl(URL);
            Thread.Sleep(4000);
            string documents = (string)scriptExecutor.ExecuteScript("var links = $('.DocumentsContainer figure figcaption')[0].innerText;return links");
            Assert.IsTrue(documents.Contains("Documents"));
        }

        [When(@"user click on Upload attachments link")]
        public void WhenUserClickOnUploadAttachmentsLink()
        {
            scriptExecutor.ExecuteScript("$('.UploadAttachmentsLink').click();");
            Thread.Sleep(4000);
        }
        [Then(@"It should open the Matter Search page on click")]
        public void ThenItShouldOpenTheMatterSearchPageOnClick()
        {
            webDriver.Navigate().GoToUrl(URL);
            Thread.Sleep(4000);
            string matterPage = (string)scriptExecutor.ExecuteScript("var links = $('.UploadAttachmentsLink').attr('href');return links");
            Assert.IsTrue(matterPage.Contains("#/matters"));
        }

        [When(@"user click on Create a new matter")]
        public void WhenUserClickOnCreateANewMatter()
        {
            scriptExecutor.ExecuteScript("$('.CreateMatterLink').click();");
            Thread.Sleep(4000);
        }

        [Then(@"It should open the Matter Provision page")]
        public void ThenItShouldOpenTheMatterProvisionPage()
        {
            webDriver.Navigate().GoToUrl(URL);
            Thread.Sleep(4000);
            string matterProvision = (string)scriptExecutor.ExecuteScript("var links = $('.CreateMatterLink').attr('href');return links");
            Assert.IsTrue(matterProvision.Contains("#/createMatter"));
        }

        [When(@"user click on Go to Matter Center Home page")]
        public void WhenUserClickOnGoToMatterCenterHomePage()
        {
            scriptExecutor.ExecuteScript("$('.MatterDashboard').click();");
            Thread.Sleep(4000);
        }

        [Then(@"It should open the Matter page")]
        public void ThenItShouldOpenTheMatterPage()
        {
            webDriver.Navigate().GoToUrl(URL);
            Thread.Sleep(4000);
            string matterDashboard = (string)scriptExecutor.ExecuteScript("var links = $('.MatterDashboardLink').attr('href');return links");
            Assert.IsTrue(matterDashboard.Contains("https://msmatter.sharepoint.com/SitePages/MatterCenterHome.aspx?section=1"));
        }

        [When(@"user click on Matter Center Support link")]
        public void WhenUserClickOnMatterCenterSupportLink()
        {
            Assert.IsTrue(true);
        }

        [Then(@"It should open draft mail with recipient '(.*)' and subject as '(.*)'")]
        public void ThenItShouldOpenDraftMailWithRecipientAndSubjectAs(string emailId, string emailSubject)
        {
            string supportLink = (string)scriptExecutor.ExecuteScript("var links = $('.emailLink').attr('href'); return links");
            Assert.IsTrue(supportLink.Contains("mailto:" + emailId + "?subject=" + emailSubject));
        }

        [When(@"user click on contextual help icon\(\?\)")]
        public void WhenUserClickOnContextualHelpIcon()
        {
            scriptExecutor.ExecuteScript("$('.ContextualHelpLogo').click();");
        }

        [Then(@"It should open the contextual help menu'")]
        public void ThenItShouldOpenTheContextualHelpMenu()
        {
            int headerCount = 0;
            if (common.ElementPresent(webDriver, "ContextualHelpHeader", Selector.CLASS) && common.ElementPresent(webDriver, "contextualHelpSections", Selector.CLASS) && common.ElementPresent(webDriver, "ContextualHelpSupport", Selector.CLASS))
            {
                headerCount++;
            }
            Assert.IsTrue(headerCount.Equals(1));
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.closeContextualHelpFlyout').click();");
        }

        [When(@"user click on User Profile icon")]
        public void WhenUserClickOnUserProfileIcon()
        {
            scriptExecutor.ExecuteScript("$('.AppHeaderProfilePict').click();");
        }

        [Then(@"It should open User Profile details")]
        public void ThenItShouldOpenUserProfileDetails()
        {
            int flyoutInfoCount = 0;
            if (common.ElementPresent(webDriver, "PersonalInfoFlyout", Selector.CLASS) && common.ElementPresent(webDriver, "PersonaContainer", Selector.CLASS) && common.ElementPresent(webDriver, "PersonaPictureContainer", Selector.CLASS)
                && common.ElementPresent(webDriver, "PersonaPictureDetails", Selector.CLASS) && common.ElementPresent(webDriver, "SignOutLink", Selector.CLASS))
            {
                flyoutInfoCount++;
            }
            Assert.IsTrue(flyoutInfoCount.Equals(1));
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.AppHeaderProfilePict').click();");
            //common.Cleanup(driver);
        }
    }
}
