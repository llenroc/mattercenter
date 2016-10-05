Feature: ManagePermission

@E2E
Scenario:01. Open the browser and load manage permission page
	When user will give 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then manage permission page should be loaded with default permission	

@E2E
Scenario: :02. User will add attorney to the Matter
	When user will add new attroney to the matter
	Then Attroney should be added in the matter

@E2E
Scenario: :03. User will save updated attroney to the project
	When user will click on save button on manage permission page
	Then updated attroney should be added in the matter

