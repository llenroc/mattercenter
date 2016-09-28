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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using System.Configuration;
    using TechTalk.SpecFlow;

    [Binding]
    public class DocumentLanding
    {
        string URL = ConfigurationManager.AppSettings["documentLanding"];
        static IWebDriver webDriver = CommonHelperFunction.GetDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        CommonHelperFunction common = new CommonHelperFunction();

        #region 01. Open the browser and load document landing page
        [When(@"user will provide '(.*)' and '(.*)'")]
        public void WhenUserWillProvideAnd(string userName, string password)
        {
            common.GetLogin(webDriver, URL);
            Assert.IsTrue(userName.Contains(ConfigurationManager.AppSettings["userName"]));
            Assert.IsTrue(password.Contains(ConfigurationManager.AppSettings["password"]));
        }

        [Then(@"document landing page should be loaded with element '(.*)'")]
        public void ThenDocumentLandingPageShouldBeLoadedWithElement(string checkId)
        {
            Assert.IsTrue(common.ElementPresent(webDriver, checkId, Selector.Id));
        }
        #endregion

    }
}
