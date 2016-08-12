//Test suite
describe('CreateMatter Controller test suite', function () {
   
    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("matterResource", ['$resource', 'auth', mockmatterResourceService]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
       $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $injector, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller('createMatterController as vm', { $scope: $scope, $rootScope: rootScope, $state: $state, $stateParams: $stateParams, matterResource: mockmatterResourceService, api: mockapi });
    }));

    describe('Verification of clearpopup function', function () {
        it('errorPopUpBlock should be set to false', function () {
            vm.clearPopUp();
            var val = vm.errorPopUpBlock;
            expect(val).toBe(false);
        });

        it('errorPopUpBlock should not be set', function () {
            var val = vm.errorPopUpBlock;
            expect(val).toBeUndefined();
        });
    });

    describe('Verification of getMatterGUID function', function () {
        it('Should return the GUID for matter with length of 32 bit', function () {

            var val = vm.matterGUID;
            expect(val.length).toBe(32);
        });

        it('Should return the GUID for matter instead of null value', function () {

            var val = vm.matterGUID;
            expect(val).not.toBe(null);
        });

        it('Should return the GUID for matter with defined value', function () {

            var val = vm.matterGUID;
            expect(val).toBeDefined();
        });
    });

    describe('Verification of selectMatterTypePopUpClose function', function () {
        it('Should return the status of popupContainerBackground and popupContainer as hide', function () {
            vm.popupContainer = "Show";
            vm.selectMatterTypePopUpClose();
            var value = vm.popupContainerBackground;
            var data = vm.popupContainer;
            expect(value).toBe("hide");
            expect(data).toBe("hide");
        });

        it('Should not return the status of popupContainerBackground and popupContainer as hide', function () {
            vm.popupContainer = "hide";
            vm.selectMatterTypePopUpClose();
            var value = vm.popupContainerBackground;
            var data = vm.popupContainer;
            expect(value).toBe("Show");
            expect(data).toBe("hide");
        });
    });

    describe('Verification of getSelectedPracticeGroupValue function', function () {
        it('Should return the selected PracticeGroup Value (all AOL and SAOL terms)', function () {
            vm.selectedPracticeGroup = selectedPracticeGroup;
            vm.getSelectedPracticeGroupValue();

                expect(vm.areaOfLawTerms).not.toBe(null);
                expect(vm.subAreaOfLawTerms).not.toBe(null);
                expect(vm.activeSubAOLTerm.termName).toBe("Advertising, Marketing ＆Promotions");
                expect(vm.activeAOLTerm.folderNames).toBe("Email;Lorem;Ipsum");
                expect(vm.errorPopUp).toBe(false);

        });

        it('Should return the null value for selected PracticeGroup Value (all AOL and SAOL terms)', function () {
            vm.selectedPracticeGroup = null;
            vm.getSelectedPracticeGroupValue();

                expect(vm.areaOfLawTerms).toBe(null);
                expect(vm.subAreaOfLawTerms).toBe(null);
        });

    });

    describe('Verification of selectAreaOfLawTerm function', function () {
        it('Should return the subAOL items on selection of AOLTerm', function () {
            vm.selectAreaOfLawTerm(selectedPracticeGroup.areaTerms[0]);

            expect(vm.subAreaOfLawTerms).not.toBe(null);
            expect(vm.activeSubAOLTerm.termName).toBe("Advertising, Marketing ＆Promotions");
            expect(vm.errorPopUp).toBe(false);
            expect(vm.activeAOLTerm).toBe(selectedPracticeGroup.areaTerms[0]);
        });

        it('Should return defined subAOL items on selection of AOLTerm', function () {
            vm.selectAreaOfLawTerm(selectedPracticeGroup.areaTerms[0]);

            expect(vm.subAreaOfLawTerms).not.toBeUndefined();
            expect(vm.activeSubAOLTerm).not.toBeUndefined();
            expect(vm.errorPopUp).not.toBeUndefined();
            expect(vm.activeAOLTerm).not.toBeUndefined();
        });
    });

    describe('Verification of selectSubAreaOfLawTerm function', function () {
        it('Should return the subAOL items', function () {

            vm.selectSubAreaOfLawTerm(selectedPracticeGroup.areaTerms[0].subareaTerms[0]);
            expect(vm.activeSubAOLTerm).toBe(selectedPracticeGroup.areaTerms[0].subareaTerms[0]);
            expect(vm.errorPopUp).toBe(false);

        });

        it('Should return defined subAOL items', function () {

            vm.selectSubAreaOfLawTerm(selectedPracticeGroup.areaTerms[0].subareaTerms[0]);
            expect(vm.activeSubAOLTerm).not.toBeUndefined();
            expect(vm.errorPopUp).not.toBeUndefined();

        });
    });

    describe('Verification of selectDocumentTemplateTypeLawTerm function', function () {
        it('Should return the document template type term', function () {

            vm.selectDocumentTemplateTypeLawTerm(documentTemplateTypeLawTerm);

            expect(vm.removeDTItem).toBe(true);
            expect(vm.errorPopUp).toBe(false);
            expect(vm.activeDocumentTypeLawTerm).toBe(documentTemplateTypeLawTerm);
            expect(vm.primaryMatterType).toBe(true);

        });

        it('Should return expected document template type term', function () {

            vm.selectDocumentTemplateTypeLawTerm(documentTemplateTypeLawTerm);

            expect(vm.removeDTItem).not.toBe(false);
            expect(vm.errorPopUp).not.toBe(true);
            expect(vm.activeDocumentTypeLawTerm).not.toBe(null);
            expect(vm.primaryMatterType).not.toBe(false);

        });
    });

    describe('Verification of addToDocumentTemplate function', function () {
        it('Should return the document template type law terms', function () {
            vm.activeSubAOLTerm = documentTemplateTypeLawTerm;
            vm.documentTypeLawTerms = subareaTerms;
            vm.addToDocumentTemplate();
            expect(vm.documentTypeLawTerms).not.toBeUndefined();
        });

        it('Should return activeDocumentTypeLawTerm as null', function () {
            vm.activeSubAOLTerm = documentTemplateTypeLawTerm;
            vm.documentTypeLawTerms = subareaTerms;
            vm.addToDocumentTemplate();
            expect(vm.activeDocumentTypeLawTerm).not.toBe("null");
        });

    });

    describe('Verification of removeFromDocumentTemplate function', function () {
        it('Should not return document template type law terms while removing', function () {
            vm.removeDTItem = false;
            vm.removeFromDocumentTemplate();
            expect(vm.removeDTItem).not.toBe(true);
            expect(vm.primaryMatterType).not.toBe(true);
        });

        it('Should return the document template type law terms while removing', function () {
            vm.removeDTItem = true;
            vm.removeFromDocumentTemplate();
            expect(vm.removeDTItem).toBe(false);
            expect(vm.primaryMatterType).toBe(false);
        });
    });

    describe('Verification of onSelect function', function () {

        it('Should return the conflicted user', function () {
            $model = {};
            $label = { "assignedUser": "" };
            vm.onSelect(item, $model, $label, "conflictcheckuser", "on-blurr", event, item.name);
            expect(vm.oSiteUsers).toBeDefined();
            expect(vm.selectedConflictCheckUser).toBe("MAQ User(MAQUser@LCADMS.onmicrosoft.com)");
        });

        it('Should return the blocked user', function () {
            $model = {};
            $label = { "assignedUser": "" };
            vm.onSelect(item, $model, $label, "blockuser", "on-blurr", event, item.name);
            expect(vm.oSiteUsers).toBeDefined();
            expect(vm.blockedUserName).toBe("MAQ User(MAQUser@LCADMS.onmicrosoft.com)");
        });

        it('Should return the team member', function () {
            $model = {};
            $label = { "assignedUser": "" };
            vm.onSelect(item, $model, $label, "team", "on-blurr", event, item.name);
            expect(vm.oSiteUsers).toBeDefined();
            expect(vm.typehead).toBe(false);
            expect(vm.notificationPopUpBlock).toBe(false);
            expect($label.assignedUser).toBe("MAQ User(MAQUser@LCADMS.onmicrosoft.com)");
        });

        it('Should return the assigned user', function () {
            $item = {
                "email": "",
                "name": "No results found"
            };
            $model = {};
            $label = { "assignedUser": "" };
            vm.onSelect($item, $model, $label, "team", "on-blurr", event, item.name);
            expect(vm.user).toBe("MAQ User");
        });

    });

    describe('Verification of saveDocumentTemplates function', function () {
        it('Should return the saved document Templates', function () {
            vm.primaryMatterType = true;
            vm.activeDocumentTypeLawTerm = documentTemplateTypeLawTerm;
            vm.documentTypeLawTerms = subareaTerms;
            vm.saveDocumentTemplates();
            expect(vm.selectedDocumentTypeLawTerms).toBe(vm.documentTypeLawTerms);
            expect(vm.popupContainerBackground).toBe("hide");
            expect(vm.popupContainer).toBe("hide");

        });

        it('Should not return saved document Templates and error popup should be prompt', function () {
            vm.primaryMatterType = false;
            vm.saveDocumentTemplates();
            expect(vm.errorPopUp).toBe(true);
        });;

    });

    describe('Verification of open1 function', function () {
        it('Should return the status of date picker', function () {
            vm.open1();
            expect(vm.opened).toBe(true);
        });
    });


    describe('Verification of conflictRadioChange function', function () {
        it('Should return the status of secureMatterRadioEnabled as true', function () {
            vm.conflictRadioChange(true);
            expect(vm.secureMatterRadioEnabled).toBe(true);
            expect(vm.secureMatterCheck).toBe(true);

        });

        it('Should return the status of secureMatterRadioEnabled as false', function () {
            vm.conflictRadioChange(false);
            expect(vm.secureMatterRadioEnabled).toBe(false);

        });
    });

    describe('Verification of addNewAssignPermissions function', function () {
        it('Should return the assignPermissionTeams data', function () {
            vm.addNewAssignPermissions();
            expect(vm.assignPermissionTeams).not.toBeUndefined();

        });
    });

    describe('Verification of removeAssignPermissionsRow function', function () {
        it('Should return the remaining users', function () {
            vm.assignPermissionTeams.length = 3;
            vm.removeAssignPermissionsRow(0);
            var rows = vm.assignPermissionTeams.length;
            expect(rows).toBe(2);
        });

        it('Should return return no result', function () {
            vm.assignPermissionTeams.length = 0;
            vm.removeAssignPermissionsRow(0);
            expect(vm.assignPermissionTeams.length).toBe(0);
        });

    });

    describe('Verification of createAndNotify function', function () {
        it('Should display the Create and notify', function () {
            vm.createAndNotify(true);
            var buttonname = vm.createButton;
            expect(buttonname).toBe("Create and Notify");
        });

        it('Should display the Create', function () {
            vm.createAndNotify(false);
            var buttonname = vm.createButton;
            expect(buttonname).toBe("Create");
        });

    });

    describe('Verification of CheckPopUp function', function () {
        
        it('Should display the CheckPopUp', function () {
            vm.CheckPopUp(true);
            expect(vm.errorPopUpBlock).toBe(false);
            expect(vm.errorBorder).toBe("");
        });

    });
    
    describe('Verification of closesuccessbanner function', function () {

        it('Should set the successbanner as false', function () {
            vm.closesuccessbanner();
            expect(vm.successMsg).toBe("");
            expect(vm.successBanner).toBe(false);
        });
    });

    describe('Verification of createMatterButton function', function () {
        it('Successfully create matter button', function () {
            vm.validateCurrentPage = function (id) { return true; };
            validateCurrentPage = function (id) { return true; };
            vm.chkConfilctCheck = true;
            vm.conflictDate = "8/1/2016";
            vm.createMatterButton(event);
            expect(vm.sectionName).toBe("snConflictCheck");
            expect(vm.iCurrentPage).toBe(2);
        });
    });

    describe('Verification of navigateToSecondSection function', function () {
        it('it should navigateToSecondSection', function () {
            vm.iCurrentPage = 5;
            vm.navigateToSecondSection("snOpenMatter");
            expect(vm.sectionName).toBe("snOpenMatter");
            expect(vm.iCurrentPage).toBe(1);
            expect(localStorage.getItem('iLivePage')).toBe("1");
            expect(vm.prevButtonDisabled).toBe(true);
            expect(vm.nextButtonDisabled).toBe(false);
        });
    });


    //describe('Verification of NextClick function', function () {
    //    it('it should NextClick', function () {
    //        vm.iCurrentPage = 1;
    //        vm.NextClick(event)
    //        expect(vm.NextClick(event)).not.toThrow(Error);
    //    });
    //    it('it should perform NextClick', function () {
    //        vm.iCurrentPage = 2;
    //        vm.NextClick(event)
    //        expect(vm.NextClick(event)).not.toThrow(Error);
    //    });
    //});

    describe('Verification of PreviousClick function', function () {
        it('it should PreviousClick', function () {
             vm.iCurrentPage = 2;
            vm.PreviousClick(event)
            expect(vm.iCurrentPage).toBe(1);
            expect(localStorage.getItem('iLivePage')).toBe("1");
            expect(vm.prevButtonDisabled).toBe(true);
            expect(vm.nextButtonDisabled).toBe(false);
        });
        //it('it should perform PreviousClick', function () {
        //    vm.iCurrentPage = 3;
        //    vm.PreviousClick(event)
        //    expect(vm.NextClick(event)).not.toThrow(Error);
        //});
    });

});
