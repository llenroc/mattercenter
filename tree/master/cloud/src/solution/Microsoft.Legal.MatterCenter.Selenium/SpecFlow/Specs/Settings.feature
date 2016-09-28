Feature: Settings Page 

Scenario: 01. Open the browser and load settings page
	When we will enter value as 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then settings page should be loaded with element 'settingName'

Scenario: 02. Set the value on settings page 
	When settings page is configured and save button is clicked
	Then settings should be saved and confirmation message should be displayed
	        
    
