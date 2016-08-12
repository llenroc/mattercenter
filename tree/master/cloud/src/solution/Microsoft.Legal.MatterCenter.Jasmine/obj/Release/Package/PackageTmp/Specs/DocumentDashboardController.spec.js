//Test suite
describe('DocumentDashboard Controller test suite', function () {

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("documentDashBoardResource", ['$resource', 'auth', mockdocumentDashBoardResource]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
       $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $injector, $rootScope) {
        rootScope = $rootScope.$new();
        dm = $controller('DocumentDashBoardController as dm', { $scope: $scope, $state: $state, $stateParams: $stateParams, documentDashBoardResource: mockdocumentDashBoardResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location });
    }));

    describe('Verification of closealldrops function', function () {
        it('It should set all drop down value as false with click on page', function () {
     
            dm.closealldrops(event);
            expect(dm.searchdrop).toBe(false);
            expect(dm.downwarddrop).toBe(true);
            expect(dm.upwarddrop).toBe(false);
            expect(dm.clientdrop).toBe(false);
            expect(dm.clientdropvisible).toBe(false);
            expect(dm.sortbydrop).toBe(false);
            expect(dm.sortbydropvisible).toBe(false);
        });
    });

    describe('Verification of hideinnerdashboarddrop function', function () {
        it('It should close and hide innerdropdowns of search box', function () {
            dm.hideinnerdashboarddrop(event);
            expect(dm.clientdrop).toBe(false);
            expect(dm.clientdropvisible).toBe(false);
        });
    });

    describe('Verification of toggleChecker function', function () {
        it('It should set all drop down value as false', function () {
            dm.documentsCheckedCount = 2;
            dm.toggleChecker(true, obj);
            expect(dm.documentsCheckedCount).toBe(3);
            expect(dm.cartelements).toBeDefined();
        });
        it('It should not set all drop down value', function () {
            dm.documentsCheckedCount = 2;
            dm.gridApi = {};
            dm.gridApi.grid = 1;
            dm.gridApi = gridrows;
            dm.toggleChecker(false, obj);
            expect(dm.documentsCheckedCount).toBe(1);
        });
    });

    describe('Verification of removeAttachment function', function () {
        it('It should return the cartelement count', function () {
            dm.cartelements = obj;
            dm.documentsCheckedCount = 3;
            dm.removeAttachment(obj[0]);
            expect(dm.cartelements.length).toBe(1);
            expect(dm.documentsCheckedCount).toBe(2);
        });
        it('It should return the cartelement count as zero', function () {
            dm.cartelements = {};
            dm.cartelements.length=0
            dm.removeAttachment(obj[0]);
            expect(dm.cartelements.length).toBe(0);
            expect(dm.documentsCheckedCount).not.toBeLessThan(0);
        });
    });


    describe('Verification of toggleCheckerAll function', function () {
        it('It should select all the document simultaneously', function () {
            dm.documentGridOptions.data=obj
            dm.documentsCheckedCount = 3;
            dm.toggleCheckerAll(true);
            expect(dm.cartelements.length).toBeDefined();
            expect(dm.documentsCheckedCount).toBe(1);
        });
        it('It should not select the document ', function () {
            dm.documentGridOptions.data = obj
            dm.documentsCheckedCount = 3;
            dm.toggleCheckerAll(false);
            expect(dm.cartelements.length).toBe(0);
            expect(dm.documentsCheckedCount).toBe(0);
        });
    });

    describe('Verification of showupward function', function () {
        it('It should show upward dropdown box', function () {
            dm.showupward(event);
            expect(dm.searchdrop).toBe(true);
            expect(dm.downwarddrop).toBe(false);
            expect(dm.upwarddrop).toBe(true);

        });
    });

    describe('Verification of showdownward function', function () {
        it('It should show downward dropdown box', function () {
             dm.showdownward(event);
             expect(dm.searchdrop).toBe(false);
             expect(dm.upwarddrop).toBe(false);
             expect(dm.downwarddrop).toBe(true);

         });
     });

    describe('Verification of showsortby function', function () {
        it('It should show sortby box', function () {
            dm.sortbydropvisible = false
             dm.showsortby(event);
            
             expect(dm.sortbydrop).toBe(true);
             expect(dm.sortbydropvisible).toBe(true);
         });
         it('It should not show sortby box', function () {
             dm.sortbydropvisible = true
             dm.showsortby(event);
             
             expect(dm.sortbydrop).toBe(false);
             expect(dm.sortbydropvisible).toBe(false);
         });
     });

    describe('Verification of pagination function', function () {
        it('It should not display pagination on page', function () {
            $scope.$apply = function () { };
            dm.totalrecords = 0;
            dm.pagination();
            expect(dm.fromtopage).toBe("1 - 0");
            expect(dm.displaypagination).toBe(false);
        });
        it('It should display pagination on page', function () {
            $scope.$apply = function () { };
            dm.totalrecords = 16;
            dm.pagination();
            expect(dm.fromtopage).toBe("1 - 16");
            expect(dm.displaypagination).toBe(true);

        });
    });

});