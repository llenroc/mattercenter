﻿// ****************************************************************************************
// Assembly         : Microsoft.Legal.MatterCenter.Selenium
// Author           : MAQ Software
// Created          : 11-09-2016
//
// ***********************************************************************
// <copyright file="MatterLanding.cs" company="Microsoft">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>This file is used to perform verification of matter landing page </summary>
// ****************************************************************************************

namespace Microsoft.Legal.MatterCenter.Selenium
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Threading;
    using TechTalk.SpecFlow;

    [Binding]
    public class MatterLanding
    {
        string URL = ConfigurationManager.AppSettings["MatterLanding"];
        string initialState;
        static IWebDriver webDriver = CommonHelperFunction.GetDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        CommonHelperFunction common = new CommonHelperFunction();

        #region 01. Open the browser and load matter landing page
        [When(@"user pass '(.*)' and '(.*)'")]
        public void WhenUserPassAnd(string userName, string password)
        {
            common.GetLogin(webDriver, URL);
            Assert.IsTrue(userName.Contains(ConfigurationManager.AppSettings["UserName"]));
            Assert.IsTrue(password.Contains(ConfigurationManager.AppSettings["Password"]));
        }

        [Then(@"matter landing page should be loaded with element '(.*)'")]
        public void ThenMatterLandingPageShouldBeLoadedWithElement(string checkId)
        {
            Assert.IsTrue(common.ElementPresent(webDriver, checkId, Selector.Id));
        }
        #endregion

        #region 02. Verify the matter components
        [When(@"user checks all the components")]
        public void WhenUserChecksAllTheComponents()
        {
            common.GetLogin(webDriver, URL);
            initialState = (string)scriptExecutor.ExecuteScript("var links = $('#PinMatter img').eq(0).attr('class');return links");
            scriptExecutor.ExecuteScript("$('#PinMatter img').click()");
            Thread.Sleep(5000);
        }

        [Then(@"all components should be present")]
        public void ThenAllComponentsShouldBePresent()
        {
            int checkEmptyTask = (webDriver.FindElements(By.CssSelector(".taskBoard .emptyItems"))).Count;
            int taskPresent = (webDriver.FindElements(By.CssSelector(".taskBoard .emptyItems"))).Count;
            int checkEmptyEvent = (webDriver.FindElements(By.CssSelector(".eventBoard .emptyItems"))).Count;
            int eventPresent = (webDriver.FindElements(By.CssSelector(".eventBody"))).Count;

            Assert.IsTrue(checkEmptyTask > 0 || taskPresent > 0);
            Assert.IsTrue(checkEmptyEvent > 0 || eventPresent > 0);

            if (common.ElementPresent(webDriver, "rssPane", Selector.Id) == true)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

            if (common.ElementPresent(webDriver, "matterInfo", Selector.Id) == true)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
            string finalState = (string)scriptExecutor.ExecuteScript("var links = $('#PinMatter img').eq(0).attr('class');return links");
            if ((initialState.ToLower(CultureInfo.CurrentCulture).Contains("pinimg") && finalState.ToLower(CultureInfo.CurrentCulture).Contains("hide pinimg")) || (initialState.ToLower(CultureInfo.CurrentCulture).Contains("hide pinimg") && finalState.ToLower(CultureInfo.CurrentCulture).Contains("pinimg")))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }
        #endregion

        #region 03. Verify the matter profile and matter description
        [When(@"user clicks on matter profile tab")]
        public void WhenUserClicksOnMatterProfileTab()
        {
            scriptExecutor.ExecuteScript(" $('.matterProfileTitle').click()");
            Thread.Sleep(2000);
        }

        [Then(@"all the matter details should be seen")]
        public void ThenAllTheMatterDetailsShouldBeSeen()
        {
            string clientName = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailTitle')[0].innerText ;return links");
            string clientMatterId = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailTitle')[1].innerText ;return links");
            string practiceGroup = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailTitle')[2].innerText ;return links");
            string areaOfLaw = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailTitle')[3].innerText;return links");
            string responsibleAttorney = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailTitle')[4].innerText ;return links");
            string checkClientName = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailText')[0].innerText;return links");
            string checkClientMatterId = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailText')[1].innerText;return links");
            string checkpracticeGroup = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailText')[2].innerText ;return links");
            string checkAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailText')[3].innerText;return links");
            string checkResonsibleAttorney = (string)scriptExecutor.ExecuteScript("var links = $('.matterDetailText')[4].innerText;return links");
            if (checkClientName != null && checkClientMatterId != null && checkpracticeGroup != null && checkAreaOfLaw != null && checkResonsibleAttorney != null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

            Assert.IsTrue(clientName.Contains("Project Name"));
            Assert.IsTrue(clientMatterId.Contains("Client & Matter ID"));
            Assert.IsTrue(practiceGroup.Contains("Practice Group"));
            Assert.IsTrue(areaOfLaw.Contains("Area of Law"));
            Assert.IsTrue(responsibleAttorney.Contains("Responsible Attorney"));
        }

        [When(@"user clicks on matter description tab")]
        public void WhenUserClicksOnMatterDescriptionTab()
        {
            scriptExecutor.ExecuteScript(" $('.matterDescriptionTitle ').click()");
            Thread.Sleep(2000);
        }

        [Then(@"matter description should be seen")]
        public void ThenMatterDescriptionShouldBeSeen()
        {
            Assert.IsTrue(common.ElementPresent(webDriver, "matterDescriptionBody", Selector.Class));
        }
        #endregion

        #region 04. Verify the footer
        [When(@"user navigates to footer")]
        public void WhenUserNavigatesToFooter()
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(4000);
        }

        [Then(@"all links should be present in the footer")]
        public void ThenAllLinksShouldBePresentInTheFooter()
        {
            string checkFeedbackAndSupport = (string)scriptExecutor.ExecuteScript("var links = $('.footerLink a')[0].href;return links");
            string checkPrivacyAndCookies = (string)scriptExecutor.ExecuteScript("var links = $('.footerLink a')[1].href;return links");
            string checkTermsOfUse = (string)scriptExecutor.ExecuteScript("var links = $('.footerLink a')[2].href;return links");
            string checkMicrosoft = (string)scriptExecutor.ExecuteScript("var links = $('.footerLink span')[0].innerText;return links");
            string checkMicrosoftLogo = (string)scriptExecutor.ExecuteScript("var links = $('.footerLogo a img').eq(0).attr('title');return links");

            Assert.IsTrue(checkMicrosoftLogo.Contains("Microsoft"));
            Assert.IsTrue(checkFeedbackAndSupport.Contains("https://msmatter.sharepoint.com/sites/microsoft/SitePages/[Enter%20URL%20for%20Support,%20e.g.%20mailto:support@supportsite.com"));
            Assert.IsTrue(checkPrivacyAndCookies.Contains("https://msmatter.sharepoint.com/sites/microsoft/SitePages/[Enter%20URL%20for%20Privacy%20terms,%20e.g.%20privacy.supportsite.com"));
            Assert.IsTrue(checkTermsOfUse.Contains("https://msmatter.sharepoint.com/sites/microsoft/SitePages/[Enter%20URL%20for%20Terms%20of%20use,%20e.g.%20termofuse.supportsite.com"));
            Assert.IsTrue(checkMicrosoft.Contains("2016 Microsoft"));
        }
        #endregion

        #region 05. Verify the hamburger menu

        [When(@"user clicks on hamburger menu")]
        public void WhenUserClicksOnHamburgerMenu()
        {
            scriptExecutor.ExecuteScript("$('#menu').click();");
            Thread.Sleep(4000);
        }

        [Then(@"hamburger menu should be seen")]
        public void ThenHamburgerMenuShouldBeSeen()
        {
            string checkMatters = (string)scriptExecutor.ExecuteScript("var links = $('.menuFlyoutColumn a')[0].innerText;return links");
            string checkDocuments = (string)scriptExecutor.ExecuteScript("var links = $('.menuFlyoutColumn a')[1].innerText;return links");
            string checkSettings = (string)scriptExecutor.ExecuteScript("var links = $('.menuFlyoutColumn a')[2].innerText;return links");

            Assert.IsTrue(checkMatters.Contains("Matters"));
            Assert.IsTrue(checkDocuments.Contains("Documents"));
            Assert.IsTrue(checkSettings.Contains("Settings"));

            scriptExecutor.ExecuteScript("$('#menu').click();");
        }
        #endregion
    }
}