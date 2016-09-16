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
    public class SearchMatter
    {
        const string URL = "https://matterwebapp.azurewebsites.net/#/matters";
        string initialState;
        static IWebDriver webDriver = Common.getDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        Common common = new Common();

        [When(@"We pass '(.*)' and '(.*)'")]
        public void WhenWePassAnd(string userName, string password)
        {
            common.getLogin(webDriver, URL);
            Assert.IsTrue(userName.Contains("matteradmin@msmatter.onmicrosoft.com"));
            Assert.IsTrue(password.Contains("P@$$w0rd01"));
        }

        [Then(@"The Matter search page will be loaded as '(.*)'")]
        public void ThenTheMatterSearchPageWillBeLoadedAs(string pageName)
        {
            Assert.IsTrue(pageName.Contains(pageName));
        }

        [When(@"User opens the search matter page")]
        public void WhenUserOpensTheSearchMatterPage()
        {
            Thread.Sleep(4000);
        }

        [Then(@"My Matter tab should be loaded")]
        public void ThenMyMatterTabShouldBeLoaded()
        {
            string matters = (string)scriptExecutor.ExecuteScript("var links = $('#gridViewPageHeaderContainer span')[0].innerHTML;return links");
            Assert.IsTrue(matters.Contains("My Matters"));
        }

        [When(@"User clicks on All Matters")]
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

        [When(@"User clicks on Pinned Matters")]
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

        [When(@"User types '(.*)' in search box")]
        public void WhenUserTypesInSearchBox(string searchText)
        {
            scriptExecutor.ExecuteScript(" $('#searchmatter .form-control')[0].value = '" + searchText + "'");
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('#basic-addon1').click()");
            Thread.Sleep(4000);
        }
        [Then(@"All Matters with the name test will be displayed")]
        public void ThenAllMattersWithTheNameTestWillBeDisplayed()
        {
            long linkLength = (long)scriptExecutor.ExecuteScript("var links = $('.col-xs-7').length;return links;");
            int linkCounter, tempCounter = 0;
            for (linkCounter = 0; linkCounter < linkLength; linkCounter++)
            {
                Thread.Sleep(1000);
                string test = (string)scriptExecutor.ExecuteScript("var links =$('.col-xs-7')[" + linkCounter + "].innerText;return links;");
                if (test.ToLower().Contains("test"))
                    tempCounter++;
            }
            //Console.Write(counter);
            if (tempCounter > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [When(@"User clicks on column picker icon")]
        public void WhenUserClicksOnColumnPickerIcon()
        {
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-button .ui-grid-icon-container').click()");
        }

        [Then(@"A column picker should be shown\.")]
        public void ThenAColumnPickerShouldBeShown_()
        {
            string mattersList = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-menu-items li button')[1].innerText;return links");
            string matterName = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-wrapper span')[0].innerText;return links");
            string clientMatterId = (string)scriptExecutor.ExecuteScript("var links =$('.ui-grid-cell-contents span.ui-grid-header-cell-label')[1].innerText;return links");
            string clientName = (string)scriptExecutor.ExecuteScript("var links =$('.ui-grid-cell-contents span.ui-grid-header-cell-label')[2].innerText;return links");
            string modifiedDate = (string)scriptExecutor.ExecuteScript("var links =$('.ui-grid-cell-contents span.ui-grid-header-cell-label')[3].innerText;return links");
            Assert.IsTrue(mattersList.Contains("Columns:"));
            Assert.IsTrue(matterName.Contains("Matter"));
            Assert.IsTrue(clientMatterId.Contains("Client.MatterID"));
            Assert.IsTrue(clientName.Contains("Client"));
            Assert.IsTrue(modifiedDate.Contains("Modified Date"));
        }
        [When(@"User checks all columns")]
        public void WhenUserChecksAllColumns()
        {
            Thread.Sleep(4000);
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[9].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[10].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[12].click()");
            Thread.Sleep(4000);
        }

        [Then(@"All columns should be shown in column header")]
        public void ThenAllColumnsShouldBeShownInColumnHeader()
        {
            string responsibleAttorney = (string)scriptExecutor.ExecuteScript("var links =$('.ui-grid-cell-contents span.ui-grid-header-cell-label')[4].innerText;return links");
            string subAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links =$('.ui-grid-cell-contents span.ui-grid-header-cell-label')[5].innerText;return links");
            string openDate = (string)scriptExecutor.ExecuteScript("var links =$('.ui-grid-cell-contents span.ui-grid-header-cell-label')[6].innerText;return links");
            Assert.IsTrue(responsibleAttorney.Contains("Responsible Attorney"));
            Assert.IsTrue(subAreaOfLaw.Contains("Sub Area of Law"));
            Assert.IsTrue(openDate.Contains("Open Date"));
        }

        [When(@"User clicks on column picker and unchecked all the columns")]
        public void WhenUserClicksOnColumnPickerAndUncheckedAllTheColumns()
        {
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[2].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[4].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[6].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[9].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[10].click()");
            scriptExecutor.ExecuteScript("$('.ui-grid-menu-inner ul li button')[12].click()");
        }

        [Then(@"All columns should be hidden in column header except Matter column")]
        public void ThenAllColumnsShouldBeHiddenInColumnHeaderExceptMatterColumn()
        {
            string columnLength = (string)scriptExecutor.ExecuteScript("var links = $('.ui-grid-header-cell-row .ui-grid-header-cell').length;var num=links.toString();return num");
            Assert.IsTrue(columnLength.Contains("1"));
        }

        [When(@"User clicks on ECB menu")]
        public void WhenUserClicksOnECBMenu()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
        }

        [Then(@"A fly out should be shown")]
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

        [When(@"User clicks on upload to matter")]
        public void WhenUserClicksOnUploadToMatter()
        {
            scriptExecutor.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[0].click()");
        }

        [Then(@"An upload to matter pop up should be shown")]
        public void ThenAnUploadToMatterPopUpShouldBeShown()
        {
            string uploadToMatter = (string)scriptExecutor.ExecuteScript("var links = $('.attachmentHeader')[0].innerText;return links");
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('.modalClose img')[0].click()");
            Assert.IsTrue(uploadToMatter.Contains("Upload to a matter"));
        }

        [When(@"User clicks on view matter details")]
        public void WhenUserClicksOnViewMatterDetails()
        {
            //driver.Navigate().GoToUrl(URL);
            //Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
            Thread.Sleep(1000);
            // js.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[1].click()");
        }

        [Then(@"Matter landing page should load")]
        public void ThenMatterLandingPageShouldLoad()
        {
            string matterLibrary = (string)scriptExecutor.ExecuteScript("var links = $('.ms-ContextualMenu-item > a')[1].href;return links");
            Assert.IsTrue(matterLibrary.Contains("https://matterwebapp.azurewebsites.net/"));
        }

        [When(@"User clicks on go to matter one note")]
        public void WhenUserClicksOnGoToMatterOneNote()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript("$('.dropdown a')[0].click()");
            //js.ExecuteScript("$('.dropdown-menu .ms-ContextualMenu-item a')[2].click()");
        }

        [Then(@"User should be redirected to one Note")]
        public void ThenUserShouldBeRedirectedToOneNote()
        {
            string URL = (string)scriptExecutor.ExecuteScript("var links =$('.ms-ContextualMenu-item > a')[2].href;return links");
            Assert.IsTrue(URL.Contains("https://msmatter.sharepoint.com/sites/microsoft/_layouts/WopiFrame.aspx?"));
        }

        [When(@"User clicks on pin this matter or unpin this matter")]
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

        [Then(@"Matter should be pinned or unpinned")]
        public void ThenMatterShouldBePinnedOrUnpinned()
        {
            string finalState = (string)scriptExecutor.ExecuteScript("var links = $('.dropdown-menu .ms-ContextualMenu-item a')[3].innerText;return links");
            Console.Write(initialState);
            Console.Write(finalState);
            if ((initialState.Contains("Pin") && finalState.Contains("Unpin")) || (initialState.Contains("Unpin") && finalState.Contains("Pin")))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [When(@"User clicks on matter")]
        public void WhenUserClicksOnMatter()
        {
            Thread.Sleep(5000);
            scriptExecutor.ExecuteScript(" $('.col-xs-7 a')[0].click()");
            Thread.Sleep(1000);
        }

        [Then(@"A matter fly out should open")]
        public void ThenAMatterFlyOutShouldOpen()
        {
            string matterName = (string)scriptExecutor.ExecuteScript("var links =$('.col-xs-7 a')[0].innerText;return links");
            string matterHeaderName = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[0].innerText ;return links");
            string clientName = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[1].innerText ;return links");
            string clientMatterId = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[2].innerText ;return links");
            string subAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[3].innerText ;return links");
            string responsibleAttorney = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[4].innerText ;return links");
            string viewMatter = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[5].innerText ;return links");
            string uploadToMatter = (string)scriptExecutor.ExecuteScript("var links = $('.ms-Callout-content')[6].innerText;return links");
            string flyoutClientName = (string)scriptExecutor.ExecuteScript("var links =  $('.ms-Callout-content .ms-font-m')[1].innerText;return links");
            string flyoutClientMatterId = (string)scriptExecutor.ExecuteScript("var links =   $('.ms-Callout-content .ms-font-m')[3].innerText;return links");
            string flyoutSubAreaOfLaw = (string)scriptExecutor.ExecuteScript("var links =  $('.ms-Callout-content .ms-font-m')[5].innerText;return links");
            string flyoutResonsibleAttorney = (string)scriptExecutor.ExecuteScript("var links =  $('.ms-Callout-content .ms-font-m')[7].innerText;return links");
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

        [When(@"User clicks on View Matter details in matter fly out")]
        public void WhenUserClicksOnViewMatterDetailsInMatterFlyOut()
        {
            //js.ExecuteScript("$('.ms-Callout-content')[5].click()");
        }

        [Then(@"Matter Landing Page should open")]
        public void ThenMatterLandingPageShouldOpen()
        {
            string viewMatterDetails = (string)scriptExecutor.ExecuteScript("var links =$('.ms-Callout-content')[5].href ;return links");
            Assert.IsTrue(viewMatterDetails.Contains("https://matterwebapp.azurewebsites.net/"));
        }

        [When(@"User clicks on Upload to matter in matter fly out")]
        public void WhenUserClicksOnUploadToMatterInMatterFlyOut()
        {
            scriptExecutor.ExecuteScript("$('.ms-Callout-content')[6].click()");
        }

        [Then(@"An upload to matter pop up will open")]
        public void ThenAnUploadToMatterPopUpWillOpen()
        {
            string uploadMatter = (string)scriptExecutor.ExecuteScript("var links = $('.attachmentHeader')[0].innerText;return links");
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("$('.modalClose img')[0].click()");
            Assert.IsTrue(uploadMatter.Contains("Upload to a matter"));
        }


        [When(@"user click on column name to sort the Matter in Ascending order")]
        public void WhenUserClickOnColumnNameToSortTheMatterInAscendingOrder()
        {
            Thread.Sleep(4000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(2000);
        }

        [Then(@"It should sort the Matter in ascending order")]
        public void ThenItShouldSortTheMatterInAscendingOrder()
        {
            int totalDocument = 0,  documentCount = 0;
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
            tempDocumentList.Sort();
            var sortedDocumentList = scriptExecutor.ExecuteScript("var oDocumentList = " + sortedDocument + ".sort();return oDocumentList");
            foreach (string element in (IEnumerable)sortedDocumentList)
            {
                if (element.Trim().ToLower().CompareTo(tempDocumentList[documentCount].Trim().ToLower()) == 0)
                {
                    totalDocument++;
                }
                documentCount++;
            }
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(2000);
            Console.Write(totalDocument);
            Assert.IsTrue(totalDocument >= 1);
        }

        [When(@"user click on column filter to filter the Matter")]
        public void WhenUserClickOnColumnFilterToFilterTheMatter()
        {
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("div.ui-grid-cell-contents.ui-grid-header-cell-primary-focus")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.CssSelector("#acombo > img")).Click();
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.form-control')[2].value = 'Test'");
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("(//button[@type='button'])[4]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//div[@id='filterResultsContainer']/div")).Click();
            Thread.Sleep(2000);
        }

        [Then(@"It should filter the Matter based on filter keyword '(.*)'")]
        public void ThenItShouldFilterTheMatterBasedOnFilterKeyword(string filterText)
        {
            int filterDocument = 0;
            long length = (long)scriptExecutor.ExecuteScript("var links = $('#matterPopup a.btn-link').length;return links");
            for (int count = 0; count > length; count++)
            {
                string datachunk = (string)scriptExecutor.ExecuteScript("var links = $('#matterPopup a.btn-link')[" + count + "].innerText;return links");
                if (datachunk.ToLower().Contains(filterText.ToLower()))
                    filterDocument++;
            }
            Assert.IsTrue(filterDocument >= 0);
        }

    }
}
