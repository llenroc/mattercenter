Feature: Matter Dashboard Page

@E2E
Scenario: 01. Open the browser and load Matter Center home page
	When 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01' will be given
	Then Matter Center home page should be loaded with element 'mcIcon'	   

Scenario: 02. Verify the hamburger menu
	When user clicks on hamburger menu on Matter Center home page
	Then hamburger menu should be loaded

Scenario: 05. Verify the matter fly out on Matter Center home page
	When user clicks on matter fly out
	Then a matter fly out should be seen

Scenario: 06. Verify the search feature on matter center home page
	When user types 'test' in search box on Matter Center Home page
	Then all results having the searched keyword should be displayed

Scenario: 04. Verify the upload button functionality
	When user clicks on upload button
	Then an upload pop up should be seen

Scenario: 03. Verify the pin/unpin functionality
	When user clicks on pin or unpin
	Then matter should get pinned or unpinned

Scenario: 07. Verify the advance filter functionality
	When user clicks on advance filter
	Then filter results should be shown to user

Scenario: 08. Verify the sort functionality in matter center home
	When user sorts data for All matters in ascending order
	Then all records should be sorted in ascending order
	When user sorts data for All matters in ascending order of created date
	Then all records should be sorted in ascending order of created date

	When user sorts data for Pinned matters in ascending order
	Then all records should be sorted in ascending order
	When user sorts data for Pinned matters in ascending order of created date
	Then all records should be sorted in ascending order of created date

	When user sorts data for My matters in ascending order
	Then all records should be sorted in ascending order
	When user sorts data for My matters in ascending order of created date
	Then all records should be sorted in ascending order of created date

Scenario: 09. Verify the footer on matter center home
	When user navigates to the footer
	Then footer should have all the links


