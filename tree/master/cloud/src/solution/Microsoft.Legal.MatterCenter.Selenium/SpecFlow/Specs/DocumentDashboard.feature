﻿Feature: Document Dashboard Page

Scenario: 01. Open the browser and load document dashboard page
	When we will enter 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01' to login
	Then document dashboard page should be loaded with element 'allDocuments'

Scenario: 02. Verify the document fly out on document dashboard 
	When user clicks on document
	Then a document fly out should be seen

Scenario: 03. Verify the pin/unpin functionality
	When user clicks on pin or unpin icon
	Then document should get pinned or unpinned

Scenario: 06. Verify the search feature on document dashboard
	When user types 'test' in search box on document dashboard
	Then all documents having the searched keyword should be displayed

Scenario: 07. Verify the advance filter functionality
	When user clicks on advance filter on document dashboard
	Then filtered results should be shown to user

Scenario: 05. Verify the sort functionality on document dashboard
	When user sorts data in ascending order on document dashboard
	Then all records should be sorted in ascending order on document dashboard

Scenario: 04. Verify the mail cart functionality
	When user selects document and clicks on mail cart
	Then selected documents should be saved as a draft when clicked on email as attachment or email as link
	    
		  