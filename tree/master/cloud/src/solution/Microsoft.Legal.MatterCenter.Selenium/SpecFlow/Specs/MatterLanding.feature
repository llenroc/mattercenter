Feature: MatterLanding

@E2E
Scenario:01. Open the browser and load matter landing page
	When user pass 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then matter landing page should be loaded with element 'matterInfo'

@E2E
Scenario:02. Verify the matter components
    When user checks all the components
	Then all components should be present

@E2E
Scenario:03. Verify the matter profile and matter description
    When user clicks on matter profile tab
	Then all the matter details should be seen 
	When user clicks on matter description tab
	Then matter description should be seen

@E2E
Scenario:04. Verify the footer
    When user navigates to footer 
	Then all links should be present in the footer

@E2E
Scenario:05. Verify the hamburger menu
    When user clicks on hamburger menu
	Then hamburger menu should be seen 

	    
