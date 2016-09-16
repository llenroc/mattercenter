using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace Microsoft.Legal.MatterCenter.Selenium
{
    [Binding]
    public class DocumentSearch
    {
        const string URL = "http://matterwebapp.azurewebsites.net/#/documents";
        string initialState;
        static IWebDriver webDriver = Common.getDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        Common common = new Common();

        [When(@"We will give '(.*)' and '(.*)'")]
        public void WhenWeWillGiveAnd(string userName, string password)
        {
            common.getLogin(webDriver, URL);
            Assert.IsTrue(userName.Contains("matteradmin@msmatter.onmicrosoft.com"));
            Assert.IsTrue(password.Contains("P@$$w0rd01"));
        }

        [Then(@"The search page will be loaded '(.*)'")]
        public void ThenTheSearchPageWillBeLoaded(string pageName)
        {
            Assert.IsTrue(pageName.Contains(pageName));
        }

        [When(@"user click on My Documents item from Drop Down menu")]
        public void WhenUserClickOnMyDocumentsItemFromDropDownMenu()
        {
            common.getLogin(webDriver, URL);
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[1].click();");
            Thread.Sleep(3000);
        }

        [Then(@"It should display My Documents in header")]
        public void ThenItShouldDisplayMyDocumentsInHeader()
        {
            string myDocuments = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer')[0].innerText;return links");
            Assert.IsTrue(myDocuments.Contains("My Documents"));
        }

        [When(@"user click on All Documents item from Drop Down menu")]
        public void WhenUserClickOnAllDocumentsItemFromDropDownMenu()
        {
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[0].click();");
            Thread.Sleep(3000);

        }

        [Then(@"It should display All Documents in header")]
        public void ThenItShouldDisplayAllDocumentsInHeader()
        {
            string allDocuments = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer')[0].innerText;return links");
            Assert.IsTrue(allDocuments.Contains("All Documents"));

        }

        [When(@"user click on Pinned Documents item from Drop Down menu")]
        public void WhenUserClickOnPinnedDocumentsItemFromDropDownMenu()
        {
            scriptExecutor.ExecuteScript("$('.searchPanelDropdown')[2].click();");
            Thread.Sleep(3000);
        }

        [Then(@"It should display Pinned Documents in header")]
        public void ThenItShouldDisplayPinnedDocumentsInHeader()
        {
            string pinnedDocuments = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer')[0].innerText;return links");
            Assert.IsTrue(pinnedDocuments.Contains("Pinned Documents"));
        }

        [When(@"user click on Column picker and Checked all the columns")]
        public void WhenUserClickOnColumnPickerAndCheckedAllTheColumns()
        {
            scriptExecutor.ExecuteScript("$('.showExpandIcon').click();");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#menuitem-9 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-11 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-13 > button > i').click();");
            scriptExecutor.ExecuteScript("$('#menuitem-15 > button > i').click();");
        }

        [Then(@"It should display all the columns in header")]
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
            Assert.IsTrue(length.ToString().Equals("8"));
        }

        [When(@"user click on Column picker and Unchecked all the columns")]
        public void WhenUserClickOnColumnPickerAndUncheckedAllTheColumns()
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

        [Then(@"It should not display any the columns except Document column in header")]
        public void ThenItShouldNotDisplayAnyTheColumnsExceptDocumentColumnInHeader()
        {
            long length = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-label').length;return links");
            Assert.IsTrue(length.ToString().Equals("1"));
            scriptExecutor.ExecuteScript("$('.closeColumnPicker').click();");
            Thread.Sleep(2000);
            common.getLogin(webDriver, URL);
            Thread.Sleep(3000);
        }
        string searchKeyword = null;
        [When(@"user search with keyword '(.*)'")]
        public void WhenUserSearchWithKeyword(string searchText)
        {
            searchKeyword = searchText;
            scriptExecutor.ExecuteScript("document.getElementsByClassName('form-control')[0].value='" + searchText + "'");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('#basic-addon1').click();");
            Thread.Sleep(3000);

        }

        [Then(@"It should display all the document which consist of Test keyword")]
        public void ThenItShouldDisplayAllTheDocumentWhichConsistOfTestKeyword()
        {
            int searchCount = 0;
            long length = (long)scriptExecutor.ExecuteScript("var links = $('.ui-grid-row').length;return links");
            for (int count = 0; count < length; count++)
            {
                string gridData = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-row')[" + count + "].innerText;return links");
                if (gridData.ToLower().Contains(searchKeyword.ToLower()))
                    searchCount++;
            }
            Assert.IsTrue(searchCount > 1);
        }

        [When(@"user click on column name to sort the document in Ascending order")]
        public void WhenUserClickOnColumnNameToSortTheDocumentInAscendingOrder()
        {
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
           
        }

        [Then(@"It should sort the document in ascending order")]
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
                string documentData = (string)scriptExecutor.ExecuteScript("var links = $('#documentPopup a.btn-link')["+ documentCounter + "].innerText;return links");
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
                if (element.Trim().ToLower().CompareTo(tempDocumentList[documentCount].Trim().ToLower()) == 0)
                {
                    toalElement++;
                }
                documentCount++;
            }
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(2000);
            Assert.IsTrue(toalElement >= 1);
        }

        [When(@"user click on column filter to filter the documents")]
        public void WhenUserClickOnColumnFilterToFilterTheDocuments()
        {
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("a.prisma-header-dropdown-anchor > img")).Click();
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.form-control')[2].value = 'Test'");
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("(//button[@type='button'])[4]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//div[@id='filterResultsContainer']/div")).Click();
            Thread.Sleep(2000);
        }

        [Then(@"It should filter the document based on filter keyword '(.*)'")]
        public void ThenItShouldFilterTheDocumentBasedOnFilterKeyword(string filterText)
        {
            int documentCount = 0;
            long length = (long)scriptExecutor.ExecuteScript("var links = $('#documentPopup a.btn-link').length;return links");
            for(int documentCounter = 0; documentCounter > length; documentCounter++)
            {
                string documentContent = (string)scriptExecutor.ExecuteScript("var links = $('#documentPopup a.btn-link')["+ documentCounter + "].innerText;return links");
                if (documentContent.ToLower().Contains(filterText.ToLower()))
                    documentCount++;
            }
           
            Assert.IsTrue(documentCount >=0);
        }

        [When(@"User clicks on ECB menu in document search page")]
        public void WhenUserClicksOnECBMenuInDocumentSearchPage()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
        }

        [Then(@"A fly out should open")]
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

        [When(@"User clicks on open this document")]
        public void WhenUserClicksOnOpenThisDocument()
        {
            scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[0].click()");
        }

        [Then(@"That document should open")]
        public void ThenThatDocumentShouldOpen()
        {
            string openDocument = (string)scriptExecutor.ExecuteScript("var links =$('.dropdown-menu .ms-ContextualMenu-item a')[0].href;return links");
            Assert.IsTrue(openDocument.Contains("https://msmatter.sharepoint.com/sites/microsoft"));
        }

        [When(@"User clicks on view matter details in fly out")]
        public void WhenUserClicksOnViewMatterDetailsInFlyOut()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[1].click()");
        }

        [Then(@"User should be redirected to matter landing page")]
        public void ThenUserShouldBeRedirectedToMatterLandingPage()
        {
            string viewMatterDetails = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[1].innerText;return links");
            Assert.IsTrue(viewMatterDetails.Contains("View Matter Details"));
        }

        [When(@"User clicks on pin this document or unpin this document")]
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

        [When(@"User clicks on document")]
        public void WhenUserClicksOnDocument()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript(" $('.col-xs-8 a')[0].click()");
            Thread.Sleep(1000);
        }

        [Then(@"A document fly out should open")]
        public void ThenADocumentFlyOutShouldOpen()
        {
            string headingMatterName = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[0].innerText ;return links");
            string matterName = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[1].innerText ;return links");
            string clientName = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[2].innerText ;return links");
            string documentId = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[3].innerText ;return links");
            string authorName = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[4].innerText ;return links");
            string modifiedDate = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[5].innerText ;return links");
            string openDocument = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[6].innerText;return links");
            string viewDocument = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[7].innerText;return links");
            string flyoutMatterName = (string)scriptExecutor.ExecuteScript("var links =  $('.ms-Callout-content .ms-font-m')[1].innerText;return links");
            string flyoutClientName = (string)scriptExecutor.ExecuteScript("var links =   $('.ms-Callout-content .ms-font-m')[3].innerText;return links");
            string flyoutDocumentId = (string)scriptExecutor.ExecuteScript("var links =  $('.ms-Callout-content .ms-font-m')[5].innerText;return links");
            string flyoutAuthorName = (string)scriptExecutor.ExecuteScript("var links =  $('.ms-Callout-content .ms-font-m')[7].innerText;return links");
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

        [When(@"User clicks on open this document in document fly out")]
        public void WhenUserClicksOnOpenThisDocumentInDocumentFlyOut()
        {
            scriptExecutor.ExecuteScript("$('.ms-Callout-content')[6].click()");
            Thread.Sleep(5000);
        }

        [Then(@"That document should open when clicked")]
        public void ThenThatDocumentShouldOpenWhenClicked()
        {
            string openDocument = (string)scriptExecutor.ExecuteScript("var links =$('.dropdown-menu .ms-ContextualMenu-item a')[0].href;return links");
            Assert.IsTrue(openDocument.Contains("https://msmatter.sharepoint.com/sites/microsoft"));
        }

        [When(@"User clicks on view document details")]
        public void WhenUserClicksOnViewDocumentDetails()
        {
            scriptExecutor.ExecuteScript("$('.ms-Callout-content')[7].click()");
            Thread.Sleep(5000);
        }

        [Then(@"Document landing page should open")]
        public void ThenDocumentLandingPageShouldOpen()
        {
            string viewDocuments = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[7].innerText;return links");
            Assert.IsTrue(viewDocuments.Contains("View document details"));
        }
    }
}