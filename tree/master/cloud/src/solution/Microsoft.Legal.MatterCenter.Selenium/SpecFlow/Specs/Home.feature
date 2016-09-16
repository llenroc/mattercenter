Feature: This will open the Home page and perform the verification

@E2E
Scenario: Open the browser and load the home page
	When We will provide 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then The home page will be loaded

@E2E
Scenario: Open the HamberGer menu and verify the elements
	When user click on HamberGer Menu
	Then HamberGer Menu should display 'Home','Matters','Documents' and 'Create New Matter' menu

@E2E	
Scenario: Verify the links on Home page
	When user click on Learn more and dismiss link
	Then It should dismiss the link

@E2E	
Scenario: Verify all the component of the page
	When user click on Matters link
	Then It should open the Matter Search page
	When user click on Documents link
	Then It should open the Document search page
	When user click on Upload attachments link 
	Then It should open the Matter Search page on click
	When user click on Create a new matter
	Then It should open the Matter Provision page
	When user click on Go to Matter Center Home page
	Then It should open the Matter page

@E2E
Scenario: Verify the Matter Center Support link
	When user click on Matter Center Support link
	Then It should open draft mail with recipient 'lcaweb2@microsoft.com' and subject as 'CELA Project Center Feedback and Support request'

@E2E
Scenario: Verify the Contextual help section
	When user click on contextual help icon(?)
	Then It should open the contextual help menu'

@E2E
Scenario: Verify the User Profile icon
	When user click on User Profile icon
	Then It should open User Profile details




