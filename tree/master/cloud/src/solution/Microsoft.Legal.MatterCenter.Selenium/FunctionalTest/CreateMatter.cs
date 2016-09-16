﻿using Microsoft.Legal.MatterCenter.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace Protractor_Net_Demo.FunctionalTest
{
    [Binding]
    public class CreateMatterSteps
    {
        const string URL = "https://maqmctest09080707.azurewebsites.net/#/createMatter";
        static IWebDriver webDriver = Common.getDriver();
        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)webDriver;
        Common common = new Common();

        [When(@"We will enter '(.*)' and '(.*)'")]
        public void WhenWeWillEnterAnd(string userName, string password)
        {
            common.getLogin(webDriver, URL);
            Assert.IsTrue(userName.Contains("matteradmin@msmatter.onmicrosoft.com"));
            Assert.IsTrue(password.Contains("P@$$w0rd01"));
        }

        [Then(@"The matter provision page will be loaded '(.*)'")]
        public void ThenTheMatterProvisionPageWillBeLoaded(string p0)
        {
            Assert.IsTrue(p0.Contains(p0));
        }

        [When(@"User select basic matter information")]
        public void WhenUserSelectBasicMatterInformation()
        {
            Thread.Sleep(4000);
            webDriver.FindElement(By.XPath("//main/div/div/div")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//section[@id='snOpenMatter']/div/div[2]/select")).Click();
            Thread.Sleep(3000);
            new SelectElement(webDriver.FindElement(By.XPath("//section[@id='snOpenMatter']/div/div[2]/select"))).SelectByText("Microsoft");
            Thread.Sleep(2000);
            webDriver.FindElement(By.CssSelector("option[value=\"100001\"]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.Id("txtMatterName")).Click();
            webDriver.FindElement(By.Id("txtMatterName")).Clear();
            Random randomObj = new Random();
            String randomNumber = randomObj.Next(1, 99999).ToString();
            webDriver.FindElement(By.Id("txtMatterName")).SendKeys(randomNumber);
            webDriver.FindElement(By.Id("txtMatterDesc")).Click();
            webDriver.FindElement(By.Id("txtMatterDesc")).Clear();
            Thread.Sleep(2000);
            webDriver.FindElement(By.Id("txtMatterDesc")).SendKeys("test");
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.col-xs-6 .buttonPrev').click();");
            Thread.Sleep(2000);
        }

        [Then(@"It should navigates to second step")]
        public void ThenItShouldNavigatesToSecondStep()
        {
            string nextStep = (string)scriptExecutor.ExecuteScript("var step = $('.menuTextSelected')[0].innerText;return step");
            Assert.IsTrue(nextStep.Contains("Assign Permission"));
        }

        [When(@"User select permission for matter")]
        public void WhenUserSelectPermissionForMatter()
        {
            Thread.Sleep(2000);
            webDriver.FindElement(By.XPath("//main/div/div/div[2]")).Click();
            Thread.Sleep(2000);
            webDriver.FindElement(By.Id("txtConflictCheckBy")).Click();
            webDriver.FindElement(By.Id("txtConflictCheckBy")).Clear();
            webDriver.FindElement(By.Id("txtConflictCheckBy")).SendKeys("wilson");
            webDriver.FindElement(By.LinkText("Wilson Gajarla")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.Id("txtUser1")).Click();
            webDriver.FindElement(By.Id("txtUser1")).Clear();
            webDriver.FindElement(By.Id("txtUser1")).SendKeys("saiK");
            webDriver.FindElement(By.LinkText("SaiKiran Gudala")).Click();
            Thread.Sleep(2000);
            scriptExecutor.ExecuteScript("$('.calendar').val('09/14/2016').trigger('change')");
            Thread.Sleep(2000);
            webDriver.FindElement(By.Id("roleUser1")).Click();
            new SelectElement(webDriver.FindElement(By.Id("roleUser1"))).SelectByText("Responsible Attorney");
            webDriver.FindElement(By.Id("chkConflictCheck")).Click();
            webDriver.FindElement(By.Id("txtBlockUser")).Click();
            webDriver.FindElement(By.Id("txtBlockUser")).Clear();
            webDriver.FindElement(By.Id("txtBlockUser")).SendKeys("wils");
            webDriver.FindElement(By.LinkText("Wilson Gajarla")).Click();
            Thread.Sleep(2000);
            new SelectElement(webDriver.FindElement(By.Id("permUser1"))).SelectByText("Full Control");
            Thread.Sleep(3000);
            scriptExecutor.ExecuteScript("$('.col-xs-6 .buttonPrev').click();");
            Thread.Sleep(3000);
        }

        [Then(@"It should navigates to third step")]
        public void ThenItShouldNavigatesToThirdStep()
        {
            string nextStep = (string)scriptExecutor.ExecuteScript("var step = $('.menuTextSelected')[0].innerText;return step");
            Assert.IsTrue(nextStep.Contains("Create and Notify"));
        }

        [When(@"User checked on all check box")]
        public void WhenUserCheckedOnAllCheckBox()
        {
            Thread.Sleep(4000);
            webDriver.FindElement(By.XPath("//main/div/div/div[3]")).Click();
            Thread.Sleep(2000);
            bool checkTrueOrFalse = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected2').prop('checked');return step;");
            if (checkTrueOrFalse == false)
            {
                scriptExecutor.ExecuteScript("$('#demo-checkbox-unselected2').click()");
            }
            checkTrueOrFalse = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected0').prop('checked');return step");
            if (checkTrueOrFalse == false)
            {
                scriptExecutor.ExecuteScript("$('#demo-checkbox-unselected0').click()");
            }
            checkTrueOrFalse = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected3').prop('checked');return step");
            if (checkTrueOrFalse == false)
            {
                scriptExecutor.ExecuteScript("$('#demo-checkbox-unselected3').click()");
            }
            checkTrueOrFalse = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected1').prop('checked');return step");
            if (checkTrueOrFalse == false)
            {
                scriptExecutor.ExecuteScript("$('#demo-checkbox-unselected1').click()");
            }
        }

        [Then(@"All check box should get checked")]
        public void ThenAllCheckBoxShouldGetChecked()
        {
            bool checkIncludeEmailNotification = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected2').prop('checked');return step;");
            bool checkIncludeCalendar = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected0').prop('checked');return step;");
            bool checkIncludeTasks = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected3').prop('checked');return step;");
            bool checkIncludeRssFeed = (bool)scriptExecutor.ExecuteScript("var step =$('#demo-checkbox-unselected3').prop('checked');return step;");
            Assert.IsTrue(checkIncludeEmailNotification == true);
            Assert.IsTrue(checkIncludeCalendar == true);
            Assert.IsTrue(checkIncludeTasks == true);
            Assert.IsTrue(checkIncludeRssFeed == true);
        }

        [When(@"User clicks on create and notify")]
        public void WhenUserClicksOnCreateAndNotify()
        {
            scriptExecutor.ExecuteScript("$('#btnCreateMatter').click()");
            Thread.Sleep(120000);
        }

        [Then(@"A new matter should get created")]
        public void ThenANewMatterShouldGetCreated()
        {
            String checkHereLink = (string)scriptExecutor.ExecuteScript("var step = $('.notification a')[0].innerText;return step;");
            Assert.IsTrue(checkHereLink.Contains("here"));
        }
    }
}
