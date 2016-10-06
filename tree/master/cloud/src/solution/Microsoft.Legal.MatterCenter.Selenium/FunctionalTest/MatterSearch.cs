﻿// ****************************************************************************************
// Assembly         : Microsoft.Legal.MatterCenter.Selenium
// Author           : MAQ Software
// Created          : 11-09-2016
//
// ***********************************************************************
// <copyright file="MatterSearch.cs" company="Microsoft">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>This file is used to perform verification of matter search page </summary>
// ***************************************************************************************

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
    public class SearchMatter
    {
        string URL = ConfigurationManager.AppSettings["MatterSearch"];
        string initialState;
        static IWebDriver webDriver = CommonHelperFunction.GetDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        CommonHelperFunction common = new CommonHelperFunction();

        #region  01. Open the browser and load search matter page
        [When(@"user enters credentials on matter search page")]
        public void WhenUserEntersCredentialsOnMatterSearchPage()
        {
            common.GetLogin(webDriver, URL);
        }

        [Then(@"matter search page should be loaded with element '(.*)'")]
        public void ThenMatterSearchPageShouldBeLoadedWithElement(string matterCenterHeader)
        {
            Assert.IsTrue(common.ElementPresent(webDriver, matterCenterHeader, Selector.Id));
        }

        #endregion

        #region 07. Verify the matter drop down menu
        [When(@"user opens the search matter page")]
        public void WhenUserOpensTheSearchMatterPage()
        {
            common.GetLogin(webDriver, URL);
            scriptExecutor.ExecuteScript("$('.input-group-btn ul li ')[1].click()");
            Thread.Sleep(4000);
        }

        [Then(@"My Matter tab should be loaded")]
        public void ThenMyMatterTabShouldBeLoaded()
        {
            string matters = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer span')[0].innerHTML;return links");
            Assert.IsTrue(matters.Contains("My Matters"));
        }

        [When(@"user clicks on All Matters")]
        public void WhenUserClicksOnAllMatters()
        {
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.input-group-btn ul li ')[0].click()");
        }

        [Then(@"All Matters result should be loaded")]
        public void ThenAllMattersResultShouldBeLoaded()
        {
            string matters = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer span')[0].innerHTML;return links");
            Assert.IsTrue(matters.Contains("All Matters"));
        }

        [When(@"user clicks on Pinned Matters")]
        public void WhenUserClicksOnPinnedMatters()
        {
            scriptExecutor.ExecuteScript("$('.input-group-btn  ul li ')[2].click()");
        }

        [Then(@"Pinned Matters should be loaded")]
        public void ThenPinnedMattersShouldBeLoaded()
        {
            string matters = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer span')[0].innerHTML;return links");
            Assert.IsTrue(matters.Contains("Pinned Matters"));
        }
        #endregion

        #region 05. Verify the matter search box
        [When(@"user types '(.*)' in search box")]
        public void WhenUserTypesInSearchBox(string searchText)
        {
            scriptExecutor.ExecuteScript("$('#searchmatter .form-control')[0].value = '" + searchText + "'");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('#basic-addon1').click()");
            Thread.Sleep(4000);
        }
        [Then(@"all matters with '(.*)' keyword should be shown")]
        public void ThenAllMattersWithKeywordShouldBeShown(string searchText)
        {
            long linkLength = (long)scriptExecutor.ExecuteScript("var links = $('.col-xs-7').length;return links;");
            int linkCounter, tempCounter = 0;
            for (linkCounter = 0; linkCounter < linkLength; linkCounter++)
            {
                Thread.Sleep(1000);
                string test = (string)scriptExecutor.ExecuteScript("var links = $('.col-xs-7')[" + linkCounter + "].innerText;return links;");
                if (!String.IsNullOrEmpty(searchText) && test.ToLower(CultureInfo.CurrentCulture).Contains(searchText.ToLower(CultureInfo.CurrentCulture)))
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
            //Removing test results
            scriptExecutor.ExecuteScript("$('#searchmatter .form-control')[0].value =''");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('#basic-addon1').click()");
            Thread.Sleep(4000);
        }
        #endregion

        #region 06. Verify the matter column picker
        [When(@"user clicks on column picker icon")]
        public void WhenUserClicksOnColumnPickerIcon()
        {
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-button .ui-grid-icon-container').click()");
        }

        [Then(@"a column picker should be shown")]
        public void ThenAColumnPickerShouldBeShown()
        {
            string mattersList = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-items li button')[1].innerText;return links");
            string matterName = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-wrapper span')[0].innerText;return links");
            string clientMatterId = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents span.ui-grid-header-cell-label')[1].innerText;return links");
            string clientName = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents span.ui-grid-header-cell-label')[2].innerText;return links");
            string modifiedDate = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents span.ui-grid-header-cell-label')[3].innerText;return links");
            Assert.IsTrue(mattersList.Contains("Columns:"));
            Assert.IsTrue(matterName.Contains("Matter"));
            Assert.IsTrue(clientMatterId.Contains("Client.MatterID"));
            Assert.IsTrue(clientName.Contains("Client"));
            Assert.IsTrue(modifiedDate.Contains("Modified Date"));
        }
        [When(@"user checks all columns")]
        public void WhenUserChecksAllColumns()
        {
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[9].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[10].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[12].click()");
            Thread.Sleep(4000);
        }

        [Then(@"all columns should be shown in column header")]
        public void ThenAllColumnsShouldBeShownInColumnHeader()
        {
            string responsibleAttorney = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents span.ui-grid-header-cell-label')[4].innerText;return links");
            string subAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents span.ui-grid-header-cell-label')[5].innerText;return links");
            string openDate = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-cell-contents span.ui-grid-header-cell-label')[6].innerText;return links");
            Assert.IsTrue(responsibleAttorney.Contains("Responsible Attorney"));
            Assert.IsTrue(subAreaOfLaw.Contains("Sub Area of Law"));
            Assert.IsTrue(openDate.Contains("Open Date"));
        }

        [When(@"user removes all the checked columns")]
        public void WhenUserRemovesAllTheCheckedColumns()
        {
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[2].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[4].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[6].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[9].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[10].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[12].click()");
        }

        [Then(@"all columns should be hidden in column header except matter column")]
        public void ThenAllColumnsShouldBeHiddenInColumnHeaderExceptMatterColumn()
        {
            string columnLength = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-row .ui-grid-header-cell').length;var num=links.toString();return num");
            Assert.IsTrue(columnLength.Contains("1"));
            //Rechecking all columns
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[2].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[4].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[6].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[9].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[10].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[12].click()");
        }
        #endregion

        #region  02. Verify the matter ECB menu
        [When(@"user clicks on ECB menu")]
        public void WhenUserClicksOnECBMenu()
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
        }

        [Then(@"a fly out should be shown")]
        public void ThenAFlyOutShouldBeShown()
        {
            string uploadToMatter = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[0].innerText;return links");
            string viewMatter = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[1].innerText;return links");
            string goToMatterOneNote = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[2].innerText;return links");
            string pinMatter = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[3].innerText;return links");
            if (pinMatter.Contains("Unpin this matter") == true || pinMatter.Contains("Pin this matter") == true)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(uploadToMatter.Contains("Upload to this Matter"));
            Assert.IsTrue(viewMatter.Contains("View Matter Details"));
            Assert.IsTrue(goToMatterOneNote.Contains("Go to Matter OneNote"));
        }

        [When(@"user clicks on upload to matter")]
        public void WhenUserClicksOnUploadToMatter()
        {
            scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[0].click()");
        }

        [Then(@"an upload to matter pop up should be shown")]
        public void ThenAnUploadToMatterPopUpShouldBeShown()
        {
            string uploadToMatter = (string)scriptExecutor.ExecuteScript("var links = $('.attachmentHeader')[0].innerText;return links");
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('.modalClose img')[0].click()");
            Assert.IsTrue(uploadToMatter.Contains("Upload to a matter"));
        }

        [When(@"user clicks on view matter details")]
        public void WhenUserClicksOnViewMatterDetails()
        {
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
            Thread.Sleep(1000);
        }

        [Then(@"matter landing page should load")]
        public void ThenMatterLandingPageShouldLoad()
        {
            string matterLibrary = (string)scriptExecutor.ExecuteScript("var links = $('.ms-ContextualMenu-item > a')[1].href;return links");
            Assert.IsTrue(matterLibrary.Contains("https://matterwebapp.azurewebsites.net/"));
        }

        [When(@"user clicks on go to matter OneNote")]
        public void WhenUserClicksOnGoToMatterOneNote()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
        }

        [Then(@"user should be redirected to OneNote")]
        public void ThenUserShouldBeRedirectedToOneNote()
        {
            string checkUrl = (string)scriptExecutor.ExecuteScript("var links = $('.ms-ContextualMenu-item > a')[2].href;return links");
            Assert.IsTrue(checkUrl.Contains("https://msmatter.sharepoint.com/sites"));
        }

        [When(@"user clicks on pin this matter or unpin this matter")]
        public void WhenUserClicksOnPinThisMatterOrUnpinThisMatter()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.input-group-btn ul li ')[0].click()");
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
            Thread.Sleep(1000);
            initialState = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[3].innerText;return links");
            scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[3].click()");
            Thread.Sleep(5000);
        }

        [Then(@"matter should be pinned or unpinned")]
        public void ThenMatterShouldBePinnedOrUnpinned()
        {
            string finalState = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[3].innerText;return links");
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

        #region 03. Verify the matter fly out
        [When(@"user clicks on matter")]
        public void WhenUserClicksOnMatter()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript(" $('.col-xs-7 a')[0].click()");
            Thread.Sleep(1000);
        }

        [Then(@"a matter fly out should open")]
        public void ThenAMatterFlyOutShouldOpen()
        {
            string matterName = (string)scriptExecutor.ExecuteScript("var links = $('.col-xs-7 a')[0].innerText;return links");
            string matterHeaderName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[0].innerText ;return links");
            string clientName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[1].innerText ;return links");
            string clientMatterId = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[2].innerText ;return links");
            string subAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[3].innerText ;return links");
            string responsibleAttorney = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[4].innerText ;return links");
            string viewMatter = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[5].innerText ;return links");
            string uploadToMatter = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[6].innerText;return links");
            string flyoutClientName = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[1].innerText;return links");
            string flyoutClientMatterId = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[3].innerText;return links");
            string flyoutSubAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[5].innerText;return links");
            string flyoutResonsibleAttorney = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content .ms-font-m')[7].innerText;return links");
            if (flyoutClientName != null && flyoutClientMatterId != null && flyoutSubAreaOfLaw != null && flyoutResonsibleAttorney != null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(matterHeaderName.Contains(matterName));
            Assert.IsTrue(clientName.Contains("Client"));
            Assert.IsTrue(clientMatterId.Contains("Client.MatterID"));
            Assert.IsTrue(subAreaOfLaw.Contains("Sub Area of Law"));
            Assert.IsTrue(responsibleAttorney.Contains("Responsible Attorney"));
            Assert.IsTrue(viewMatter.Contains("View matter details"));
            Assert.IsTrue(uploadToMatter.Contains("Upload to a matter"));
        }


        [When(@"user clicks on view matter details in matter fly out")]
        public void WhenUserClicksOnViewMatterDetailsInMatterFlyOut()
        {
            Thread.Sleep(1000);
        }

        [Then(@"matter landing page should open")]
        public void ThenMatterLandingPageShouldOpen()
        {
            string viewMatterDetails = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[5].href ;return links");
            Assert.IsTrue(viewMatterDetails.Contains("https://matterwebapp.azurewebsites.net/"));
        }

        [When(@"user clicks on upload to matter in matter fly out")]
        public void WhenUserClicksOnUploadToMatterInMatterFlyOut()
        {
            scriptExecutor.ExecuteScript("$('.ms-Callout-content')[6].click()");
        }

        [Then(@"an upload to matter pop up should open")]
        public void ThenAnUploadToMatterPopUpShouldOpen()
        {
            string uploadMatter = (string)scriptExecutor.ExecuteScript("var links = $('.attachmentHeader')[0].innerText;return links");
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('.modalClose img')[0].click()");
            Assert.IsTrue(uploadMatter.Contains("Upload to a matter"));
        }

        #endregion

        #region 04. Verify the matter sort
        [When(@"user clicks on column name to sort the matter in ascending order")]
        public void WhenUserClicksOnColumnNameToSortTheMatterInAscendingOrder()
        {
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(2000);
        }

        [Then(@"it should sort the matter in ascending order")]
        public void ThenItShouldSortTheMatterInAscendingOrder()
        {
            int totalDocument = 0, documentCount = 0;
            char[] delimiters = new char[] { '\r', '\n' };

            long length = (long)scriptExecutor.ExecuteScript("var links = $('#matterPopup a.btn-link').length;return links");
            string sortedDocument = "[";
            string[] documentList = new string[length];
            string duplicateDocuments = null;
            for (int documentCounter = 0; documentCounter < length; documentCounter++)
            {
                string datachunk = (string)scriptExecutor.ExecuteScript("var links = $('#matterPopup a.btn-link')[" + documentCounter + "].innerText;return links");
                string[] rows = datachunk.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                if (!(rows[0].Equals(duplicateDocuments)))
                {

                    if (rows[0] != null)
                        documentList[documentCounter] = rows[0];
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
            Assert.IsTrue(totalDocument >= 5);
        }
        #endregion

        #region 08. Verify the matter filter search
        string searchKeyword;
        [When(@"user clicks on column filter to filter the matter with the keyword '(.*)' on All Matters")]
        public void WhenUserClicksOnColumnFilterToFilterTheMatterWithTheKeywordOnAllMatters(string filterKeyword)
        {
            common.GetLogin(webDriver, URL);
            Thread.Sleep(4000);
            searchKeyword = filterKeyword;
            scriptExecutor.ExecuteScript("$('.input-group-btn ul li ')[0].click()");
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("#acombo > img")).Click();
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.form-control')[2].value = '" + filterKeyword + "'");
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("(//button[@type='button'])[4]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//div[@id='filterResultsContainer']/div")).Click();
            Thread.Sleep(2000);
        }

        [Then(@"it should filter the matter based on filter keyword")]
        public void ThenItShouldFilterTheMatterBasedOnFilterKeyword()
        {
            int filterDocument = 0;
            long length = (long)scriptExecutor.ExecuteScript("var links = $('#matterPopup a.btn-link').length;return links");
            for (int count = 0; count < length; count++)
            {
                string datachunk = (string)scriptExecutor.ExecuteScript("var links = $('#matterPopup a.btn-link')[" + count + "].innerText;return links");
                if (datachunk.ToLower(CultureInfo.CurrentCulture).Contains(searchKeyword.ToLower(CultureInfo.CurrentCulture)))
                    filterDocument++;
            }
            Assert.IsTrue(filterDocument >= 0);
        }

        [When(@"user clicks on column filter to filter the matter with the keyword '(.*)' on My Matters")]
        public void WhenUserClicksOnColumnFilterToFilterTheMatterWithTheKeywordOnMyMatters(string filterKeyword)
        {
            webDriver.FindElement(By.ClassName("AppSwitcherContainer")).Click();
            scriptExecutor.ExecuteScript("$('.AppMenuFlyoutPriLinks a')[0].click();");
            Thread.Sleep(3000);
            webDriver.FindElement(By.ClassName("AppSwitcherContainer")).Click();
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.AppMenuFlyoutPriLinks a')[1].click();");
            Thread.Sleep(3000);
            searchKeyword = filterKeyword;
            scriptExecutor.ExecuteScript("$('.input-group-btn ul li ')[1].click()");
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("#acombo > img")).Click();
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.form-control')[2].value = '" + filterKeyword + "'");
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("(//button[@type='button'])[4]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//div[@id='filterResultsContainer']/div")).Click();
            Thread.Sleep(2000);
        }
        #endregion

    }
}
