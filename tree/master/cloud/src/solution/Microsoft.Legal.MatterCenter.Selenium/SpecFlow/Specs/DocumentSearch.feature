Feature: This will open the SearchDocument and perform the verification

@E2E
Scenario: Open the browser and load the SearchDocument page
	When We will give 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then The search page will be loaded 'SearchDocument'

@E2E	
Scenario: Verify the Document Drop down menu
	When user click on My Documents item from Drop Down menu
	Then It should display My Documents in header
	When user click on Pinned Documents item from Drop Down menu
	Then It should display Pinned Documents in header
	When user click on All Documents item from Drop Down menu
	Then It should display All Documents in header

@E2E
Scenario: Verify the Column picker
	When user click on Column picker and Checked all the columns
	Then It should display all the columns in header
	When user click on Column picker and Unchecked all the columns
	Then It should not display any the columns except Document column in header

@E2E
Scenario: Verify the document search box
	When user search with keyword 'Test'
	Then It should display all the document which consist of Test keyword

@E2E
Scenario: Verify the document sort
	When user click on column name to sort the document in Ascending order
	Then It should sort the document in ascending order

@E2E
Scenario: Verify the document filter search
	When user click on column filter to filter the documents
	Then It should filter the document based on filter keyword 'Test'

@E2E
Scenario: Verify the document ECB menu
	When User clicks on ECB menu in document search page
	Then A fly out should open
	When User clicks on open this document
	Then That document should open
	When User clicks on view matter details in fly out
	Then User should be redirected to matter landing page
    When User clicks on pin this document or unpin this document
	Then document should be pinned or unpinned

@E2E
Scenario: Verify the document fly out
	When User clicks on document
	Then A document fly out should open
	When User clicks on open this document in document fly out
	Then That document should open when clicked
	When User clicks on view document details
	Then Document landing page should open
	   