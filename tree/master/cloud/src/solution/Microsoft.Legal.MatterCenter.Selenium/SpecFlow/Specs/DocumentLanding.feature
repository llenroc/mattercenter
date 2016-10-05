Feature: DocumentLanding

@E2E
Scenario:01. Open the browser and load document landing page
	When user will provide 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then document landing page should be loaded with element 'documentName'
	
@E2E
Scenario:02. Verify page components
	When user clicks on various components on the page
	Then all components should work as per design  
	
@E2E
Scenario:03. Verify file properties
	When user navigates to file properties section
	Then all file properties should be present  

@E2E
Scenario:04. Verify version details
	When user navigates to version section
	Then all versions of the document should be seen
      
@E2E
Scenario:05. Verify the footer links
	When user navigates to footer on document landing page
	Then all links should be present on footer on document landing page  

@E2E
Scenario:06. Verify the pin/unpin functionality
	When user clicks on pin/unpin button
	Then document should get pinned/unpinned