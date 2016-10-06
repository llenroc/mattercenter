﻿// ****************************************************************************************
// Assembly         : Microsoft.Legal.MatterCenter.Selenium
// Author           : MAQ Software
// Created          : 11-09-2016
//
// ***********************************************************************
// <copyright file="DocumentSearch.cs" company="Microsoft">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>This file is used to perform verification of document search page </summary>
// ****************************************************************************************

namespace Microsoft.Legal.MatterCenter.Selenium
{
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
    public class DocumentSearch
    {
        string URL = ConfigurationManager.AppSettings["DocumentSearch"];
        string initialState;
        static IWebDriver webDriver = CommonHelperFunction.GetDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        CommonHelperFunction common = new CommonHelperFunction();

        #region 01. Open the browser and load search document page
        [When(@"user enters credentials on document search page")]
        public void WhenUserEntersCredentialsOnDocumentSearchPage()
        {
            common.GetLogin(webDriver, URL);
        }

        [Then(@"document search page should be loaded with element '(.*)'")]
        public void ThenDocumentSearchPageShouldBeLoadedWithElement(string checkId)
        {
            Assert.IsTrue(common.ElementPresent(webDriver, checkId, Selector.Id));
        }
        #endregion

        #region 07. Verify the document drop down menu
        [When(@"user clicks on My Documents item from drop down menu")]
        public void WhenUserClicksOnMyDocumentsItemFromDropDownMenu()
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[1].click();");
            Thread.Sleep(3000);
        }

        [Then(@"it should display My Documents in header")]
        public void ThenItShouldDisplayMyDocumentsInHeader()
        {
            string myDocuments = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer')[0].innerText;return links");
            Assert.IsTrue(myDocuments.Contains("My Documents"));
        }

        [When(@"user clicks on All Documents item from drop down menu")]
        public void WhenUserClicksOnAllDocumentsItemFromDropDownMenu()
        {
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[0].click();");
            Thread.Sleep(3000);
        }

        [Then(@"it should display All Documents in header")]
        public void ThenItShouldDisplayAllDocumentsInHeader()
        {
            string allDocuments = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer')[0].innerText;return links");
            Assert.IsTrue(allDocuments.Contains("All Documents"));
        }

        [When(@"user clicks on Pinned Documents item from drop down menu")]
        public void WhenUserClicksOnPinnedDocumentsItemFromDropDownMenu()
        {
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[2].click();");
            Thread.Sleep(3000);
        }

        [Then(@"it should display Pinned Documents in header")]
        public void ThenItShouldDisplayPinnedDocumentsInHeader()
        {
            string pinnedDocuments = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer')[0].innerText;return links");
            Assert.IsTrue(pinnedDocuments.Contains("Pinned Documents"));
        }
        #endregion

        #region  06. Verify the column picker
        [When(@"user clicks on column picker and checks all columns")]
        public void WhenUserClicksOnColumnPickerAndChecksAllColumns()
        {
            scriptExecutor.ExecuteScript("$('.showExpandIcon').click();");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#menuitem-9 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-11 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-13 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-15 > button > i').click();");
        }

        [Then(@"it should display all the columns in header")]
        public void ThenItShouldDisplayAllTheColumnsInHeader()
        {
            string client = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-item')[3].innerText;return links");
            string matterId = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-item')[5].innerText;return links");
            string date = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-item')[7].innerText;return links");
            string author = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-item')[9].innerText;return links");
            string version = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-item')[11].innerText;return links");
            string checkout = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-item')[13].innerText;return links");
            string createdDate = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-item')[15].innerText;return links");
            string clientcolumn = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label')[1].innerText;return links");
            string matterIdcolumn = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label')[2].innerText;return links");
            string datecolumn = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label')[3].innerText;return links");
            string authorcolumn = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label')[4].innerText;return links");
            string versioncolumn = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label')[5].innerText;return links");
            string checkoutcolumn = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label')[6].innerText;return links");
            string createdDatecolumn = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label')[7].innerText;return links");
            Int64 length = (Int64)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label').length;return links");

            Assert.IsTrue(clientcolumn.Contains(client.Trim()));
            Assert.IsTrue(matterIdcolumn.Contains(matterId.Trim()));
            Assert.IsTrue(datecolumn.Contains(date.Trim()));
            Assert.IsTrue(authorcolumn.Contains(author.Trim()));
            Assert.IsTrue(versioncolumn.Contains(version.Trim()));
            Assert.IsTrue(checkoutcolumn.Contains(checkout.Trim()));
            Assert.IsTrue(createdDatecolumn.Contains(createdDate.Trim()));
            Assert.IsTrue(length.ToString(CultureInfo.CurrentCulture).Equals("8"));
        }


        [When(@"user clicks on column picker and remove all checked columns")]
        public void WhenUserClickOnColumnPickerAndRemoveAllCheckedColumns()
        {
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.showExpandIcon').click();");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#menuitem-3 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-5 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-7 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-9 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-11 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-13 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-15 > button > i').click();");
        }

        [Then(@"it should not display any columns except document column in header")]
        public void ThenItShouldNotDisplayAnyColumnsExceptDocumentColumnInHeader()
        {
            long length = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label').length;return links");
            Assert.IsTrue(length.ToString(CultureInfo.CurrentCulture).Equals("1"));
            scriptExecutor.ExecuteScript("$('.closeColumnPicker').click();");
            Thread.Sleep(2000);
            Thread.Sleep(3000);
            // To display all the columns again
            scriptExecutor.ExecuteScript("$('.showExpandIcon').click();");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#menuitem-3 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-5 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-7 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-9 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-11 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-13 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-15 > button > i').click();");
        }
        #endregion

        #region 05. Verify the document search box
        [When(@"user searches with keyword '(.*)'")]
        public void WhenUserSearchesWithKeyword(string searchText)
        {
            // search 
            scriptExecutor.ExecuteScript("document.getElementsByClassName('form-control')[0].value='" + searchText + "'");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#basic-addon1').click();");
            Thread.Sleep(3000);
        }

        [Then(@"it should display all the document which consist of '(.*)' keyword")]
        public void ThenItShouldDisplayAllTheDocumentWhichConsistOfKeyword(string searchText)
        {
            int searchCount = 0;
            long length = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-row').length;return links");
            for (int count = 0; count < length; count++)
            {
                string gridData = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-row')[" + count + "].innerText;return links");
                if (!String.IsNullOrEmpty(searchText) && gridData.ToLower(CultureInfo.CurrentCulture).Contains(searchText.ToLower(CultureInfo.CurrentCulture)))
                    searchCount++;
            }
            Assert.IsTrue(searchCount > 1);
            scriptExecutor.ExecuteScript("$('.form-control')[0].value=''");
            scriptExecutor.ExecuteScript("$('#basic-addon1').click();");
            Thread.Sleep(3000);
        }
        #endregion

        #region 04. Verify the document sort
        [When(@"user click on column name to sort the document in Ascending order")]
        public void WhenUserClickOnColumnNameToSortTheDocumentInAscendingOrder()
        {
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
        }

        [Then(@"it should sort the document in ascending order")]
        public void ThenItShouldSortTheDocumentInAscendingOrder()
        {
            int toalElement = 0, documentCount = 0;
            char[] delimiters = new char[] { '\r', '\n' };

            long length = (long)scriptExecutor.ExecuteScript("var links = $('#documentPopup a.btn-link').length;return links");
            string sortedDocuments = "[";
            string[] documentlist = new string[length];
            string duplicateDocuments = null;
            for (int documentCounter = 0; documentCounter < length; documentCounter++)
            {
                string documentData = (string)scriptExecutor.ExecuteScript("var links = $('#documentPopup a.btn-link')[" + documentCounter + "].innerText;return links");
                string[] rows = documentData.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                if (!(rows[0].Equals(duplicateDocuments)))
                {

                    if (rows[0] != null)
                        documentlist[documentCounter] = rows[0];
                    duplicateDocuments = rows[0];
                }
            }
            var tempDocumentList = new List<string>();
            foreach (var documentName in documentlist)
            {
                if (!string.IsNullOrEmpty(documentName))
                {
                    tempDocumentList.Add(documentName);
                    sortedDocuments += "'" + documentName + "',";
                }
            }
            sortedDocuments.TrimEnd(',');
            sortedDocuments += "]";
            var data = scriptExecutor.ExecuteScript("var arr = " + sortedDocuments + ".sort();return arr");
            foreach (string element in (IEnumerable)data)
            {
                if (string.Equals(element.Trim(), tempDocumentList[documentCount].Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    toalElement++;
                }
                documentCount++;
            }
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(2000);
            Assert.IsTrue(toalElement >= 5);
        }
        #endregion

        #region 08. Verify the document filter search
        string keyword;
        [When(@"user clicks on column filter to filter the documents using keyword '(.*)' on My Documents")]
        public void WhenUserClicksOnColumnFilterToFilterTheDocumentsUsingKeywordOnMyDocuments(string filterKeyword)
        {
            // Navigate to 'My documents' section
            common.GetLogin(webDriver, URL);
            Thread.Sleep(2000);
            keyword = filterKeyword;
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[1].click();");
            Thread.Sleep(4000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("a.prisma-header-dropdown-anchor > img")).Click();
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.form-control')[2].value = '" + filterKeyword + "'");
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("(//button[@type='button'])[4]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//div[@id='filterResultsContainer']/div")).Click();
            Thread.Sleep(2000);
        }

        [Then(@"it should filter the document based on filtered keyword")]
        public void ThenItShouldFilterTheDocumentBasedOnFilteredKeyword()
        {
            int documentCount = 0;
            long length = (long)scriptExecutor.ExecuteScript("var links = $('#documentPopup a.btn-link').length;return links");
            for (int documentCounter = 0; documentCounter < length; documentCounter++)
            {
                string documentContent = (string)scriptExecutor.ExecuteScript("var links = $('#documentPopup a.btn-link')[" + documentCounter + "].innerText;return links");
                if (documentContent.ToLower(CultureInfo.CurrentCulture).Contains(keyword.ToLower(CultureInfo.CurrentCulture)))
                    documentCount++;
            }
            Assert.IsTrue(documentCount >= 1);
        }

        [When(@"user clicks on column filter to filter the documents using keyword '(.*)' on All Documents")]
        public void WhenUserClicksOnColumnFilterToFilterTheDocumentsUsingKeywordOnAllDocuments(string filterKeyword)
        {
            // Navigate to 'All documents' section
            common.GetLogin(webDriver, URL);
            Thread.Sleep(2000);
            keyword = filterKeyword;
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[0].click();");
            Thread.Sleep(4000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("a.prisma-header-dropdown-anchor > img")).Click();
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.form-control')[2].value = '" + filterKeyword + "'");
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("(//button[@type='button'])[4]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//div[@id='filterResultsContainer']/div")).Click();
            Thread.Sleep(2000);
        }
        #endregion

        #region 02. Verify the document ECB menu
        [When(@"user clicks on ECB menu in document search page")]
        public void WhenUserClicksOnECBMenuInDocumentSearchPage()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
        }

        [Then(@"a fly out should open")]
        public void ThenAFlyOutShouldOpen()
        {
            string openThisDocument = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[0].innerText;return links");
            Assert.IsTrue(openThisDocument.Contains("Open this Document"));
            string viewMatterDetails = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[1].innerText;return links");
            Assert.IsTrue(viewMatterDetails.Contains("View Matter Details"));
            string pinMethod = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[2].innerText;return links");

            if (pinMethod.Contains("Unpin this Document") == true || pinMethod.Contains("Pin this Document") == true)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [When(@"user clicks on open this document")]
        public void WhenUserClicksOnOpenThisDocument()
        {
            //scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[0].click()");
            Thread.Sleep(1000);
        }

        [Then(@"that document should open")]
        public void ThenThatDocumentShouldOpen()
        {
            string openDocument = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[0].href;return links");
            Assert.IsTrue(openDocument.Contains("https://msmatter.sharepoint.com/sites/microsoft"));
        }

        [When(@"user clicks on view matter details in fly out")]
        public void WhenUserClicksOnViewMatterDetailsInFlyOut()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
            Thread.Sleep(1000);
            //scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[1].click()");
        }

        [Then(@"user should be redirected to matter landing page")]
        public void ThenUserShouldBeRedirectedToMatterLandingPage()
        {
            string viewMatterDetails = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[1].innerText;return links");
            Assert.IsTrue(viewMatterDetails.Contains("View Matter Details"));
        }

        [When(@"user clicks on pin this document or unpin this document")]
        public void WhenUserClicksOnPinThisDocumentOrUnpinThisDocument()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
            Thread.Sleep(1000);
            initialState = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[2].innerText;return links");
            scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[2].click()");
            Thread.Sleep(5000);
        }

        [Then(@"document should be pinned or unpinned")]
        public void ThenDocumentShouldBePinnedOrUnpinned()
        {
            string finalState = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[2].innerText;return links");
            if ((initialState.Contains("Pin") && finalState.Contains("Unpin")) || (initialState.Contains("Unpin") && finalState.Contains("Pin")))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
        #endregion

        #region 03. Verify the document fly out
        [When(@"user clicks on document on document search page")]
        public void WhenUserClicksOnDocumentOnDocumentSearchPage()
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript(" $('.col-xs-8 a')[0].click()");
            Thread.Sleep(1000);
        }

        [Then(@"a document fly out should open")]
        public void ThenADocumentFlyOutShouldOpen()
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
            string flyoutModifiedDate = (string)scriptExecutor.ExecuteScript("var links =  $('.ms-Callout-content .ms-font-m')[9].innerText;return links");
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

        [When(@"user clicks on open this document in document fly out")]
        public void WhenUserClicksOnOpenThisDocumentInDocumentFlyOut()
        {
            //scriptExecutor.ExecuteScript("$('.ms-Callout-content')[6].click()");
            Thread.Sleep(5000);
        }

        [Then(@"that document should open when clicked")]
        public void ThenThatDocumentShouldOpenWhenClicked()
        {
            string openDocument = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[0].href;return links");
            Assert.IsTrue(openDocument.Contains(ConfigurationManager.AppSettings["OpenDocument"]));
        }

        [When(@"user clicks on view document details")]
        public void WhenUserClicksOnViewDocumentDetails()
        {
            //scriptExecutor.ExecuteScript("$('.ms-Callout-content')[7].click()");
            Thread.Sleep(5000);
        }

        [Then(@"document landing page should open")]
        public void ThenDocumentLandingPageShouldOpen()
        {
            string viewDocuments = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[7].innerText;return links");
            Assert.IsTrue(viewDocuments.Contains("View document details"));
        }
        #endregion

    }
}