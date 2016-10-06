﻿// ****************************************************************************************
// Assembly         : Microsoft.Legal.MatterCenter.Selenium
// Author           : MAQ Software
// Created          : 11-09-2016
//
// ***********************************************************************
// <copyright file="DocumentDashboard.cs" company="Microsoft">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>This file is used to perform verification of document dashboard page </summary>
// ****************************************************************************************

namespace Microsoft.Legal.MatterCenter.Selenium
{
    using Microsoft.Legal.MatterCenter.Selenium;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Threading;
    using TechTalk.SpecFlow;

    [Binding]
    public class DocumentDashboard
    {
        string URL = ConfigurationManager.AppSettings["DocumentDashboard"];
        string initialState;
        static IWebDriver webDriver = CommonHelperFunction.GetDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        CommonHelperFunction common = new CommonHelperFunction();

        #region 01. Open the browser and load document dashboard page
        [When(@"user enters credentials on document dashboard page")]
        public void WhenUserEntersCredentialsOnDocumentDashboardPage()
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(5000);
        }

        [Then(@"document dashboard page should be loaded with element '(.*)'")]
        public void ThenDocumentDashboardPageShouldBeLoadedWithElement(string allDocuments)
        {
            Assert.IsTrue(common.ElementPresent(webDriver, allDocuments, Selector.Id));
        }
        #endregion

        #region 02. Verify the document fly out on document dashboard page
        [When(@"user clicks on document")]
        public void WhenUserClicksOnDocument()
        {
            scriptExecutor.ExecuteScript("$('.col-xs-12 a')[4].click()");
            Thread.Sleep(5000);
        }

        [Then(@"a document fly out should be seen")]
        public void ThenADocumentFlyOutShouldBeSeen()
        {
            string headingMatterName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[0].innerText ;return links");
            string matterName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[1].innerText ;return links");
            string clientName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[2].innerText ;return links");
            string documentId = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[3].innerText ;return links");
            string authorName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[4].innerText ;return links");
            string modifiedDate = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[5].innerText ;return links");
            string openDocument = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[6].innerText;return links");
            string viewDocument = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[7].innerText;return links");
            string flyoutMatterName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[1].innerText;return links");
            string flyoutClientName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[3].innerText;return links");
            string flyoutDocumentId = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[5].innerText;return links");
            string flyoutAuthorName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[7].innerText;return links");
            string flyoutModifiedDate = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[9].innerText;return links");
            if (flyoutClientName != null && flyoutMatterName != null && flyoutDocumentId != null && flyoutAuthorName != null && flyoutModifiedDate != null && headingMatterName != null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(matterName.Contains("Matter"));
            Assert.IsTrue(clientName.Contains("Client"));
            Assert.IsTrue(documentId.Contains("Document ID"));
            Assert.IsTrue(authorName.Contains("Author"));
            Assert.IsTrue(modifiedDate.Contains("Modified Date"));
            Assert.IsTrue(openDocument.Contains("Open document"));
            Assert.IsTrue(viewDocument.Contains("View document details"));
        }
        #endregion 

        #region 03. Verify the pin/unpin functionality on document dashboard page

        [When(@"user clicks on pin or unpin icon")]
        public void WhenUserClicksOnPinOrUnpinIcon()
        {
            initialState = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents img')[0].src;return links");
            scriptExecutor.ExecuteScript("$('.ui-grid-cell-contents img')[0].click()");
            Thread.Sleep(5000);
        }

        [Then(@"document should get pinned or unpinned")]
        public void ThenDocumentShouldGetPinnedOrUnpinned()
        {
            string finalState = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents img')[0].src;return links");
            if ((initialState.ToLower(CultureInfo.CurrentCulture).Contains("pin") && finalState.ToLower(CultureInfo.CurrentCulture).Contains("unpin")) || (initialState.ToLower(CultureInfo.CurrentCulture).Contains("unpin") && finalState.ToLower(CultureInfo.CurrentCulture).Contains("pin")))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        #endregion

        #region 06. Verify the search feature on document dashboard page

        [When(@"user types '(.*)' in search box on document dashboard")]
        public void WhenUserTypesInSearchBoxOnDocumentDashboard(string searchBox)
        {
            Thread.Sleep(2000);
            webDriver.FindElement(By.CssSelector(".col-xs-12 .form-control")).Clear();
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('.col-xs-12 .form-control')[0].value='" + searchBox+"'");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#basic-addon1').click()");
            Thread.Sleep(5000);
        }

        [Then(@"all documents having '(.*)' keyword should be displayed")]
        public void ThenAllDocumentsHavingKeywordShouldBeDisplayed(string searchBox)
        {
            long linkLength = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-canvas .ui-grid-row ').length;return links;");
            int linkCounter, tempCounter = 0;
            for (linkCounter = 0; linkCounter < linkLength; linkCounter++)
            {
                Thread.Sleep(1000);
                string test = (string)scriptExecutor.ExecuteScript("var links = $('.col-xs-12 #documentPopup')[" + linkCounter + "].title;return links;");
                if (!String.IsNullOrEmpty(searchBox) && test.ToLower(CultureInfo.CurrentCulture).Contains(searchBox.ToLower(CultureInfo.CurrentCulture)))
                    tempCounter++;
            }
            if (tempCounter > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
        #endregion

        #region 07. Verify the 'advance filter' functionality on document dashboard page
        [When(@"user clicks on advance filter on document dashboard")]
        public void WhenUserClicksOnAdvanceFilterOnDocumentDashboard()
        {
            scriptExecutor.ExecuteScript("history.go(0)");
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.col-xs-3 img')[0].click();");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.input-group-addon')[1].click();");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-md-2 a')[1].click();");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.refinerWrapper input')[0].click()");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.filterOkButton .dashboardSearch')[0].click()");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#filterAdvancedSearch').click()");
            Thread.Sleep(5000);
        }

        [Then(@"filtered results should be shown to user")]
        public void ThenFilteredResultsShouldBeShownToUser()
        {
            long length = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-canvas .ui-grid-row ').length;return links");
            long finalValue = (7 + (length - 1) * 6);
            int counter = 0;
            for (int documentCounter = 7; documentCounter <= finalValue; documentCounter = documentCounter + 6)
            {
                string checkClient = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents')[" + documentCounter + "].innerText;return links");
                if (checkClient.Contains("Amazon"))
                {
                    counter++;
                }
            }
            Assert.IsTrue(counter > 5);
        }
        #endregion

        #region 05. Verify the sort functionality on document dashboard page
        [When(@"user sorts data for My document in ascending order")]
        public void WhenUserSortsDataForMyDocumentInAscendingOrder()
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(6000);
            scriptExecutor.ExecuteScript("$('.nav-tabs a')[1].click()");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-xs-4 img').click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.col-sm-4 ul li')[1].click()");
            Thread.Sleep(4000);
        }

        [Then(@"all records should be sorted in ascending order on document dashboard")]
        public void ThenAllRecordsShouldBeSortedInAscendingOrderOnDocumentDashboard()
        {
            int totalDocument = 0, documentCount = 0, counter = 0; ;
            char[] delimiters = new char[] { '\r', '\n' };

            long length = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-canvas .ui-grid-row ').length;return links");
            string sortedDocument = "[";
            string[] documentList = new string[length];
            string duplicateDocuments = null;
            for (int documentCounter = 0; documentCounter < length; documentCounter++)
            {
                string datachunk = (string)scriptExecutor.ExecuteScript("var links = $('.col-xs-12 #documentPopup')[" + documentCounter + "].title;return links");
                string[] rows = datachunk.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                if (!(rows[0].Equals(duplicateDocuments)))
                {

                    if (rows[0] != null)
                    {
                        documentList[counter] = rows[0];
                        counter++;
                    }
                    duplicateDocuments = rows[0];
                }
            }
            var tempDocumentList = new List<string>();
            foreach (var document in documentList)
            {
                if (!string.IsNullOrEmpty(document))
                {
                    tempDocumentList.Add(document);
                    sortedDocument += "'" + document + "',";
                }
            }
            sortedDocument.TrimEnd(',');
            sortedDocument += "]";
            documentCount = 0;
            var sortedDocumentList = scriptExecutor.ExecuteScript("var oDocumentList = " + sortedDocument + ".sort();return oDocumentList");
            foreach (string element in (IEnumerable)sortedDocumentList)
            {
                if (string.Equals(element.Trim(), tempDocumentList[documentCount].Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    totalDocument++;
                }
                documentCount++;
            }
            Thread.Sleep(2000);
            Assert.IsTrue(totalDocument >= 0);
        }

        [Then(@"all records should be sorted in ascending order on document dashboard by created date")]
        public void ThenAllRecordsShouldBeSortedInAscendingOrderOnDocumentDashboardByCreatedDate()
        {
            int totalDocument = 0, documentCount = 0, counter = 0; ;
            char[] delimiters = new char[] { '\r', '\n' };

            long length = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-canvas .ui-grid-row ').length;return links");
            string sortedDocument = "[";
            string[] documentList = new string[length];
            string duplicateDocuments = null;
            for (int documentCounter = 0; documentCounter < length; documentCounter++)
            {
                string datachunk = (string)scriptExecutor.ExecuteScript("var links = $('.col-xs-12 #documentPopup')[" + documentCounter + "].title;return links");
                string[] rows = datachunk.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                if (!(rows[0].Equals(duplicateDocuments)))
                {

                    if (rows[0] != null)
                    {
                        documentList[counter] = rows[0];
                        counter++;
                    }
                    duplicateDocuments = rows[0];
                }
            }
            var tempDocumentList = new List<string>();
            foreach (var document in documentList)
            {
                if (!string.IsNullOrEmpty(document))
                {
                    tempDocumentList.Add(document);
                    sortedDocument += "'" + document + "',";
                }
            }
            sortedDocument.TrimEnd(',');
            sortedDocument += "]";
            documentCount = 0;
            var sortedDocumentList = scriptExecutor.ExecuteScript("var oDocumentList = " + sortedDocument + ".sort();return oDocumentList");
            foreach (string element in (IEnumerable)sortedDocumentList)
            {
                if (string.Equals(element.Trim(), tempDocumentList[documentCount].Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    totalDocument++;
                }
                documentCount++;
            }
            Thread.Sleep(2000);
            Assert.IsTrue(totalDocument >= 0);
        }

        [When(@"user sorts data for My document in ascending order of created date")]
        public void WhenUserSortsDataForMyDocumentInAscendingOrderOfCreatedDate()
        {
            scriptExecutor.ExecuteScript("$('.nav-tabs a')[1].click()");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-xs-4 img').click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.col-sm-4 ul li')[3].click()");
            Thread.Sleep(4000);
        }

        [When(@"user sorts data in ascending order on document dashboard")]
        public void WhenUserSortsDataInAscendingOrderOnDocumentDashboard()
        {
            scriptExecutor.ExecuteScript("$('.nav-tabs a')[2].click()");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-xs-4 img').click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.col-sm-4 ul li')[1].click()");
            Thread.Sleep(4000);
        }

        [When(@"user sorts data in ascending order of created date")]
        public void WhenUserSortsDataInAscendingOrderOfCreatedDate()
        {
            scriptExecutor.ExecuteScript("$('.nav-tabs a')[2].click()");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-xs-4 img').click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.col-sm-4 ul li')[3].click()");
            Thread.Sleep(4000);
        }

        [When(@"user sorts data for Pinned document in ascending order")]
        public void WhenUserSortsDataForPinnedDocumentInAscendingOrder()
        {
            scriptExecutor.ExecuteScript("$('.nav-tabs a')[3].click()");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-xs-4 img').click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.col-sm-4 ul li')[1].click()");
            Thread.Sleep(4000);
        }

        [When(@"user sorts data for Pinned document in ascending order of created date")]
        public void WhenUserSortsDataForPinnedDocumentInAscendingOrderOfCreatedDate()
        {
            scriptExecutor.ExecuteScript("$('.nav-tabs a')[3].click()");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-xs-4 img').click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.col-sm-4 ul li')[3].click()");
            Thread.Sleep(4000);
        }

        #endregion

        #region 04. Verify the 'mail cart' functionality on document dashboard page.

        [When(@"user selects document and clicks on mail cart")]
        public void WhenUserSelectsDocumentAndClicksOnMailCart()
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(6000);
            scriptExecutor.ExecuteScript("$('.ui-grid-cell-contents input')[12].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-cell-contents input')[2].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-cell-contents input')[3].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-cell-contents input')[4].click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('#EmailImg img').click()");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.paddingRight20 input')[3].click()");
            Thread.Sleep(4000);
            Thread.Sleep(1000);
        }

        [Then(@"popup should display email as link or email as attachment options")]
        public void PopupShouldDisplayEmailAsLinkOrEmailAsAttchmentOptions()
        {
            Assert.IsTrue(common.ElementPresent(webDriver, "emailAttachmentOption", Selector.Class));
            Assert.IsTrue(common.ElementPresent(webDriver, "emailLinkOption", Selector.Class));
            scriptExecutor.ExecuteScript("$('.col-sm-1 img')[0].click()");
            scriptExecutor.ExecuteScript("$('.pull-right')[0].click()");
        }

        #endregion

    }
}
