// ****************************************************************************************
// Assembly         : Microsoft.Legal.MatterCenter.Selenium
// Author           : MAQ Software
// Created          : 11-09-2016
//
// ***********************************************************************
// <copyright file="DocumentLanding.cs" company="Microsoft">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>This file is used to perform verification of document landing page </summary>
// ****************************************************************************************

namespace Microsoft.Legal.MatterCenter.Selenium
{
    using VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using System.Configuration;
    using System.Threading;
    using TechTalk.SpecFlow;
    using System;
    using System.Web;

    [Binding]
    public class DocumentLanding
    {
        string URL = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["DocumentLandingPage"]);
        static IWebDriver webDriver = CommonHelperFunction.GetDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        CommonHelperFunction common = new CommonHelperFunction();

        #region 01. Open the browser and load document landing page
        [When(@"user will provide '(.*)' and '(.*)'")]
        public void WhenUserWillProvideAnd(string userName, string password)
        {
            webDriver.Navigate().GoToUrl(new Uri(URL));
            Assert.IsTrue(userName.Contains(ConfigurationManager.AppSettings["UserName"]));
            Assert.IsTrue(password.Contains(ConfigurationManager.AppSettings["Password"]));
        }

        [Then(@"document landing page should be loaded with element '(.*)'")]
        public void ThenDocumentLandingPageShouldBeLoadedWithElement(string checkId)
        {
            Assert.IsTrue(common.ElementPresent(webDriver, checkId, Selector.Id));
        }
        #endregion

        #region 02. Verify page components
        string initialState;
        string checkIn, checkOut;
        [When(@"user clicks on various components on the page")]
        public void WhenUserClicksOnVariousComponentsOnThePage()
        {
            Thread.Sleep(5000);
            initialState = (string)scriptExecutor.ExecuteScript("var links = $('#pinUnpinDocument span').eq(0).attr('class');return links");
            checkIn = (string)scriptExecutor.ExecuteScript("var links = $('#checkInDocument').attr('class');return links");
            checkOut = (string)scriptExecutor.ExecuteScript("var links = $('#checkOutDocument').attr('class');return links");
            Thread.Sleep(5000);
        }

        [Then(@"all components should work as per design")]
        public void ThenAllComponentsShouldWorkAsPerDesign()
        {
            string checkDownload = (string)scriptExecutor.ExecuteScript("var links = $('#spanDownload')[0].innerText;return links");
            string checkShare = (string)scriptExecutor.ExecuteScript("var links = $('#spanShare')[0].innerText;return links");
            string checkSend = (string)scriptExecutor.ExecuteScript("var links = $('#spanSendLink')[0].innerText;return links");
            string finalState = (string)scriptExecutor.ExecuteScript("var links = $('#pinUnpinDocument span').eq(2).attr('class');return links");
            Assert.IsTrue(checkDownload.Contains("download"));
            Assert.IsTrue(checkShare.Contains("share"));
            Assert.IsTrue(checkSend.Contains("send link"));

            if ((string.IsNullOrEmpty(checkIn) && checkOut.Contains("hide")) || (checkIn.Contains("hide") && string.IsNullOrEmpty(checkOut)))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
            if ((string.IsNullOrEmpty(initialState) && finalState.Contains("hide")) || (initialState.Contains("hide") && string.IsNullOrEmpty(finalState)))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
        #endregion

        #region 03. Verify file properties
        [When(@"user navigates to file properties section")]
        public void WhenUserNavigatesToFilePropertiesSection()
        {
            scriptExecutor.ExecuteScript("$('#fileProperties img')[0].click()");
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('#fileProperties img')[0].click()");
            Thread.Sleep(1000);

        }

        [Then(@"all file properties should be present")]
        public void ThenAllFilePropertiesShouldBePresent()
        {
            string checkFileName = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[2].innerText;return links");
            string checkClient = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[4].innerText;return links");
            string checkMatter = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[6].innerText;return links");
            string checkMatterId = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[8].innerText;return links");
            string checkDocumnentId = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[10].innerText;return links");
            string checkCheckedOutTo = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[16].innerText;return links");
            string checkAuthor = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[22].innerText;return links");
            string checkDateCreated = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[24].innerText;return links");
            string checkPracticeGroup = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[26].innerText;return links");
            string checkAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[28].innerText;return links");
            string checkSubAreaofLaw = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[30].innerText;return links");
            string checkFileType = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[32].innerText;return links");
            string checkFileSize = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[34].innerText;return links");
            string checkFileAccess = (string)scriptExecutor.ExecuteScript("var links = $('#accessValue')[0].href;return links");
            string checkViewMore = (string)scriptExecutor.ExecuteScript("var links = $('#viewMoreFileProperties')[0].innerText;return links");
            string checkFileNameHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[1].innerText;return links");
            string checkClientHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[3].innerText;return links");
            string checkMatterHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[5].innerText;return links");
            string checkClientMatterIdHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[7].innerText;return links");
            string checkDocumentIdHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[9].innerText;return links");
            string checkCheckedOutToHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[11].innerText;return links");
            string checkAuthorHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[17].innerText;return links");
            string checkDateCreatedHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[23].innerText;return links");
            string checkPracticeGroupHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[25].innerText;return links");
            string checkAreaOfLawHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[27].innerText;return links");
            string checkSubAreaOfLawHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[29].innerText;return links");
            string checkFileTypeHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[31].innerText;return links");
            string checkFileSizeHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[33].innerText;return links");
            string checkFileAccessHeader = (string)scriptExecutor.ExecuteScript("var links = $('#metadataProperties li span')[35].innerText;return links");


            if ((!string.IsNullOrEmpty(checkFileName) && !string.IsNullOrEmpty(checkClient) && !string.IsNullOrEmpty(checkMatter) && !string.IsNullOrEmpty(checkMatterId) && !string.IsNullOrEmpty(checkDocumnentId) && !string.IsNullOrEmpty(checkCheckedOutTo) && !string.IsNullOrEmpty(checkAuthor) && !string.IsNullOrEmpty(checkDateCreated) && !string.IsNullOrEmpty(checkPracticeGroup) && !string.IsNullOrEmpty(checkAreaOfLaw) && !string.IsNullOrEmpty(checkSubAreaofLaw) && !string.IsNullOrEmpty(checkFileType) && !string.IsNullOrEmpty(checkFileSize) && !string.IsNullOrEmpty(checkFileAccess)))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(checkViewMore.Contains("View More"));
            Assert.IsTrue(checkClientHeader.Contains("Client:"));
            Assert.IsTrue(checkFileNameHeader.Contains("File Name:"));
            Assert.IsTrue(checkMatterHeader.Contains("Matter:"));
            Assert.IsTrue(checkClientMatterIdHeader.Contains("Client.MatterID:"));
            Assert.IsTrue(checkDocumentIdHeader.Contains("Document ID:"));
            Assert.IsTrue(checkCheckedOutToHeader.Contains("Checked out to:"));
            Assert.IsTrue(checkAuthorHeader.Contains("Author:"));
            Assert.IsTrue(checkDateCreatedHeader.Contains("Date created:"));
            Assert.IsTrue(checkPracticeGroupHeader.Contains("Practice group:"));
            Assert.IsTrue(checkAreaOfLawHeader.Contains("Area of law:"));
            Assert.IsTrue(checkSubAreaOfLawHeader.Contains("Sub area of law:"));
            Assert.IsTrue(checkFileTypeHeader.Contains("File type:"));
            Assert.IsTrue(checkFileSizeHeader.Contains("File size:"));
            Assert.IsTrue(checkFileAccessHeader.Contains("File access:"));

        }
        #endregion

        #region 04. Verify version details

        [When(@"user navigates to version section")]
        public void WhenUserNavigatesToVersionSection()
        {
            scriptExecutor.ExecuteScript("$('.viewMoreContainer img')[0].click()");
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('.viewMoreContainer img')[0].click()");
            Thread.Sleep(1000);
        }

        [Then(@"all versions of the document should be seen")]
        public void ThenAllVersionsOfTheDocumentShouldBeSeen()
        {
            string checkVersion = (string)scriptExecutor.ExecuteScript("var links = $('#versionHeader span')[0].innerText;return links");
            string checkModified = (string)scriptExecutor.ExecuteScript("var links = $('#versionHeader span')[1].innerText;return links");
            string checkModifiedBy = (string)scriptExecutor.ExecuteScript("var links = $('#versionHeader span')[2].innerText;return links");
            string checkViewMore = (string)scriptExecutor.ExecuteScript("var links = $('#viewMoreVersions')[0].innerText;return links");
            string checkVersionNumber = (string)scriptExecutor.ExecuteScript("var links = $('.versionNumber ')[0].innerText;return links");
            Assert.IsTrue(!string.IsNullOrEmpty(checkVersionNumber));
            Assert.IsTrue(checkVersion.Contains("Version"));
            Assert.IsTrue(checkModified.Contains("Modified"));
            Assert.IsTrue(checkModifiedBy.Contains("Modified By"));
            Assert.IsTrue(checkViewMore.Contains("View More"));
        }
        #endregion

        #region 05. Verify the footer links
        [When(@"user navigates to footer on document landing page")]
        public void WhenUserNavigatesToFooterOnDocumentLandingPage()
        {
            webDriver.Navigate().GoToUrl(new Uri(URL));
            Thread.Sleep(5000);
        }

        [Then(@"all links should be present on footer on document landing page")]
        public void ThenAllLinksShouldBePresentOnFooterOnDocumentLandingPage()
        {
            string checkFeedbackAndSupport = (string)scriptExecutor.ExecuteScript("var links =$('#feedbackSupport')[0].innerText;return links");
            string checkPrivacyAndCookies = (string)scriptExecutor.ExecuteScript("var links =$('#privacyLink')[0].href;return links");
            string checkTermsOfUse = (string)scriptExecutor.ExecuteScript("var links =$('#termsOfUse')[0].href;return links");
            string checkMicrosoft = (string)scriptExecutor.ExecuteScript("var links =$('.footerLink span')[0].innerText;return links");
            string checkMicrosoftLogo = (string)scriptExecutor.ExecuteScript("var links =$('.footerLogo a img')[0].title;return links");

            Thread.Sleep(1000);

            Assert.IsTrue(checkMicrosoftLogo.Contains("Microsoft"));
            Assert.IsTrue(checkFeedbackAndSupport.Contains("Feedback & Support"));
            Assert.IsTrue(checkPrivacyAndCookies.Contains("privacy"));
            Assert.IsTrue(checkTermsOfUse.Contains("termofuse"));
            Assert.IsTrue(checkMicrosoft.Contains("2016 Microsoft"));
        }

        #endregion


    }
}
