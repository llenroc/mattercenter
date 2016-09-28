﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Protractor_Net_Demo.SpecFlow.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Home Page")]
    public partial class HomePageFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Home.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Home Page", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("01. Open the browser and load home page")]
        [NUnit.Framework.CategoryAttribute("E2E")]
        public virtual void _01_OpenTheBrowserAndLoadHomePage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("01. Open the browser and load home page", new string[] {
                        "E2E"});
#line 4
this.ScenarioSetup(scenarioInfo);
#line 5
 testRunner.When("we will provide \'matteradmin@msmatter.onmicrosoft.com\' and \'P@$$w0rd01\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 6
 testRunner.Then("home page should be loaded with element \'HomeContainer\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("02. Open the hamburger menu and verify all the elements")]
        [NUnit.Framework.CategoryAttribute("E2E")]
        public virtual void _02_OpenTheHamburgerMenuAndVerifyAllTheElements()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("02. Open the hamburger menu and verify all the elements", new string[] {
                        "E2E"});
#line 9
this.ScenarioSetup(scenarioInfo);
#line 10
 testRunner.When("user clicks on hamburger menu on home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("hamburger menu should display \'Home\',\'Projects\',\'Documents\' and \'Create New Matte" +
                    "r\' menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("03. Verify the links on home page")]
        [NUnit.Framework.CategoryAttribute("E2E")]
        public virtual void _03_VerifyTheLinksOnHomePage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("03. Verify the links on home page", new string[] {
                        "E2E"});
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.When("user clicks on learn more and dismiss link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.Then("it should dismiss the link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("04. Verify all components of the page")]
        [NUnit.Framework.CategoryAttribute("E2E")]
        public virtual void _04_VerifyAllComponentsOfThePage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("04. Verify all components of the page", new string[] {
                        "E2E"});
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
 testRunner.When("user clicks on matters link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
 testRunner.Then("it should open the matter search page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 22
 testRunner.When("user clicks on documents link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
 testRunner.Then("it should open the document search page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
 testRunner.When("user clicks on upload attachments link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("it should redirect to matter search page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 26
 testRunner.When("user clicks on create a new matter", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
 testRunner.Then("it should open the matter provision page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 28
 testRunner.When("user clicks on go to matter center home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
 testRunner.Then("it should open the matters page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("05. Verify the Matter Center support link")]
        [NUnit.Framework.CategoryAttribute("E2E")]
        public virtual void _05_VerifyTheMatterCenterSupportLink()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("05. Verify the Matter Center support link", new string[] {
                        "E2E"});
#line 32
this.ScenarioSetup(scenarioInfo);
#line 33
 testRunner.When("user click on Matter Center support link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 34
 testRunner.Then("it should open draft mail with recipient \'lcaweb2@microsoft.com\' and subject as \'" +
                    "CELA Project Center Feedback and Support request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("06. Verify the contextual help section")]
        [NUnit.Framework.CategoryAttribute("E2E")]
        public virtual void _06_VerifyTheContextualHelpSection()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("06. Verify the contextual help section", new string[] {
                        "E2E"});
#line 37
this.ScenarioSetup(scenarioInfo);
#line 38
 testRunner.When("user clicks on contextual help icon(?)", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("it should open the contextual help menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("07. Verify the user profile icon")]
        [NUnit.Framework.CategoryAttribute("E2E")]
        public virtual void _07_VerifyTheUserProfileIcon()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("07. Verify the user profile icon", new string[] {
                        "E2E"});
#line 42
this.ScenarioSetup(scenarioInfo);
#line 43
 testRunner.When("user clicks on user profile icon", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 44
 testRunner.Then("it should open user profile details", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
