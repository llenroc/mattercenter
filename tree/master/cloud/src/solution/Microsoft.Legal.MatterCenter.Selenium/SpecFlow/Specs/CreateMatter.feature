Feature: This will open the create matter page and perform the verification feature

@E2E
Scenario: 01. Open the browser and load the Create Matter page
	When We will enter 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then The matter provision page will be loaded 'CreateMatter'

@E2E
Scenario: 02. Verify Open Matter tab
	When User select basic matter information
	Then It should navigates to second step

@E2E
Scenario: 03. Verify Assign Permission tab
	When User select permission for matter
	Then It should navigates to third step

@E2E
Scenario: 04. Verify create and notify tab
	When User checked on all check box
	Then All check box should get checked
	When User clicks on create and notify 
	Then A new matter should get created



