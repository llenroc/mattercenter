Feature: DocumentLanding

@E2E
Scenario:01. Open the browser and load document landing page
	When user will provide 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then document landing page should be loaded with element 'documentName'
	   