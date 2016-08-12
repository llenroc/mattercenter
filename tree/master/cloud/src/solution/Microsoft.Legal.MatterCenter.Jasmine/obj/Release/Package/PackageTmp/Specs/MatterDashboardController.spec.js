//Test suite
describe('MatterDashBoard Controller test suite', function () {
   
    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("matterDashBoardResource", ['$resource', 'auth', mockmatterDashBoardResource]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
       $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        pm = $controller('MatterDashBoardController as pm', { $scope: $scope, $state: $state, $stateParams: $stateParams, matterDashBoardResource: mockmatterDashBoardResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location ,$q: $q });
    }));

    describe('Verification of closealldrops function', function () {
        it('It should set all drop down value as false with click on page', function () {
            pm.closealldrops();
            expect(pm.searchdrop).toBe(false);
            expect(pm.downwarddrop).toBe(true);
            expect(pm.upwarddrop).toBe(false);
            expect(pm.clientdrop).toBe(false);
            expect(pm.clientdropvisible).toBe(false);
            expect(pm.pgdrop).toBe(false);
            expect(pm.pgdropvisible).toBe(false);
            expect(pm.aoldrop).toBe(false);
            expect(pm.aoldropvisible).toBe(false);
            expect(pm.sortbydrop).toBe(false);
            expect(pm.sortbydropvisible).toBe(false);
        });
    });

    describe('Verification of hideinnerdrop function', function () {
        it('It should set innerdrop box as hidden', function () {
            pm.hideinnerdrop(event);
            expect(pm.clientdrop).toBe(false);
            expect(pm.clientdropvisible).toBe(false);
            expect(pm.pgdrop).toBe(false);
            expect(pm.pgdropvisible).toBe(false);
            expect(pm.aoldrop).toBe(false);
            expect(pm.aoldropvisible).toBe(false);
           
        });
    });

    describe('Verification of filterSearchOK function', function () {
        it('It should return selected client', function () {
            pm.selectedClients = '';
            pm.clients = clientobj;
            pm.filterSearchOK("client");
            expect(pm.clientdrop).toBe(false);
            expect(pm.clientdropvisible).toBe(false);
            expect(pm.selectedClientsForCancel).toBe("A. Datum Corporation");
            expect(pm.selectedClients).toBe("A. Datum Corporation");
        });

        it('It should return selected practice group', function () {
            pm.selectedPGs = '';
            pm.practiceGroups = practicegroup;
            pm.filterSearchOK("pg");
            expect(pm.pgdropvisible).toBe(false);
            expect(pm.pgdrop).toBe(false);
            expect(pm.selectedPGsForCancel).toBe("Advertising, Marketing ＆ Promotions");
            expect(pm.selectedPGs).toBe("Advertising, Marketing ＆ Promotions");

        });
        it('It should return selected AOL', function () {
            pm.selectedAOLs = '';
            pm.aolTerms = practicegroup;
            pm.filterSearchOK("aol");
            expect(pm.aoldropvisible).toBe(false);
            expect(pm.aoldrop).toBe(false);
            expect(pm.selectedAOLsForCancel).toBe("Advertising, Marketing ＆ Promotions");
            expect(pm.selectedAOLs).toBe("Advertising, Marketing ＆ Promotions");
        });
    });

    describe('Verification of filterSearchCancel function', function () {
        it('It should trigger when the user clicks on "Cancel" button in the filter panel', function () {
            pm.filterSearchCancel("client");
            expect(pm.clientdrop).toBe(false);
            expect(pm.clientdropvisible).toBe(false);
            expect(pm.pgdrop).toBe(false);
            expect(pm.pgdropvisible).toBe(false);
            expect(pm.aoldropvisible).toBe(false);
            expect(pm.aoldrop).toBe(false);
        });

    });

    describe('Verification of showupward function', function () {
        it('It should show upward dropdown box', function () {
            pm.showupward(event);
            expect(pm.searchdrop).toBe(true);
            expect(pm.downwarddrop).toBe(false);
            expect(pm.upwarddrop).toBe(true);

        });
    });

    describe('Verification of showdownward function', function () {
        it('It should show downward dropdown box', function () {
            pm.showdownward(event);
            expect(pm.searchdrop).toBe(false);
            expect(pm.upwarddrop).toBe(false);
            expect(pm.downwarddrop).toBe(true);

        });
    });

    describe('Verification of showsortby function', function () {
        it('It should show sortby box', function () {
            pm.sortbydropvisible = false
             pm.showsortby(event);
            
             expect(pm.sortbydrop).toBe(true);
             expect(pm.sortbydropvisible).toBe(true);
         });
         it('It should not show sortby box', function () {
             pm.sortbydropvisible = true
             pm.showsortby(event);
             
             expect(pm.sortbydrop).toBe(false);
             expect(pm.sortbydropvisible).toBe(false);
         });
     });

    describe('Verification of pagination function', function () {
        it('It should not display pagination on page', function () {
            $scope.$apply = function () { };
            pm.totalrecords = 0;
            pm.pagination();
            expect(pm.fromtopage).toBe("1 - 0");
            expect(pm.displaypagination).toBe(false);
        });
        it('It should display pagination on page', function () {
            $scope.$apply = function () { };
            pm.totalrecords = 16;
            pm.pagination();
            expect(pm.fromtopage).toBe("1 - 16");
            expect(pm.displaypagination).toBe(true);

        });
    });

    describe('Verification of overwriteConfiguration function', function () {
        it('It should overwrite the Configuration of file', function () {
            var bAppendEnabled = pm.overwriteConfiguration("TestEmail.msg");
            expect(bAppendEnabled).toBe(true);
        });

    });

    describe('Verification of contentCheckNotification function', function () {
        it('It should set file parameter', function () {
            pm.file = {};
            pm.contentCheckNotification(pm.file,true);
            expect(pm.file.contentCheck).toBe("contentCheck");
            expect(pm.file.saveLatestVersion).toBe("False");
            expect(pm.file.cancel).toBe("False");
        });
        
    });

    describe('Verification of abortContentCheck function', function () {
        it('It should set file parameter while aborting the content check', function () {
            pm.file = {};
            pm.file.value="This";
            pm.abortContentCheck(pm.file, false);
            expect(pm.file.contentCheck).toBe(null);
            expect(pm.file.saveLatestVersion).toBe("True");
            expect(pm.file.value).toBe("This<br/><div>Content check has been aborted.</div>");
            expect(pm.file.cancel).toBe("True");
        });

    });

    describe('Verification of closeSuccessBanner function', function () {
        it('It should close success banner', function () {
            pm.closeSuccessBanner();
            expect(pm.oUploadGlobal.successBanner).toBe(false);
        });

    });



   
});

//closealldrops
//hideinnerdrop
//filterSearchOK
//filterSearchCancel
//showupward
//showdownward
//showsortby
//pagination
//overwriteConfiguration
//contentCheckNotification
//abortContentCheck
//closeSuccessBanner
