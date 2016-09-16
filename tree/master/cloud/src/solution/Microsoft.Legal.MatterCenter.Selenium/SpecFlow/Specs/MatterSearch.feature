Feature: This will open the Search Matter page and perform the verification

@E2E
Scenario: Open the browser and load the SearchMatter page
	When We pass 'matteradmin@msmatter.onmicrosoft.com' and 'P@$$w0rd01'
	Then The Matter search page will be loaded as 'SearchMatter'

@E2E
Scenario: Verify the Matter Drop down Menu
	When User opens the search matter page
	Then My Matter tab should be loaded
	When User clicks on All Matters
	Then All Matters result should be loaded
	When User clicks on Pinned Matters
	Then Pinned Matters should be loaded

@E2E	
Scenario: Verify the matter search box
	When User types 'test' in search box 
	Then All Matters with the name test will be displayed 

@E2E
Scenario: Verify the matter column picker
	When User clicks on column picker icon
	Then A column picker should be shown.
	When User checks all columns
	Then All columns should be shown in column header 
	When User clicks on column picker and unchecked all the columns  
	Then All columns should be hidden in column header except Matter column 

@E2E
Scenario: Verify the matter ECB menu
	When User clicks on ECB menu
	Then A fly out should be shown
	When User clicks on upload to matter
	Then An upload to matter pop up should be shown
	When User clicks on view matter details
	Then Matter landing page should load
	When User clicks on go to matter one note
	Then User should be redirected to one Note
    When User clicks on pin this matter or unpin this matter
	Then Matter should be pinned or unpinned

@E2E   
Scenario: Verify the matter fly out
	When User clicks on matter
	Then A matter fly out should open
	When User clicks on View Matter details in matter fly out
	Then Matter Landing Page should open
	When User clicks on Upload to matter in matter fly out
	Then An upload to matter pop up will open
 
@E2E
Scenario: Verify the Matter sort
	When user click on column name to sort the Matter in Ascending order
	Then It should sort the Matter in ascending order

@E2E
Scenario: Verify the Matter filter search
	When user click on column filter to filter the Matter
	Then It should filter the Matter based on filter keyword 'Test'
	     
     
   
