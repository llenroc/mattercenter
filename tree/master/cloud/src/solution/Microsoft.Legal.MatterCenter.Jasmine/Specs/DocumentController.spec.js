//Test suite
describe('documents Controller test suite', function () {
    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("documentResource", ['$resource', 'auth', mockdocumentResource]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        om = $controller('documentsController as om', { $scope: $scope, $state: $state, $stateParams: $stateParams, documentResource: mockdocumentResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location, $q: $q, $animate: $animate });
    }));

    describe('Verification of showdocdrop function', function () {
        it('It should show docdrop menu', function () {
            om.docdropinner = true;
            om.showdocdrop(event);
            expect(om.documentsdrop).toBe(true);
            expect(om.docdropinner).toBe(false);

        });
        it('It should show hide docdrop menu', function () {
            om.docdropinner = false;
            om.showdocdrop(event);
            expect(om.documentsdrop).toBe(false);
            expect(om.docdropinner).toBe(true);

        });
    });

    describe('Verification of closealldrops function', function () {
        it('It should close all dropdown menu', function () {
            om.closealldrops();
            expect(om.documentsdrop).toBe(false);
            expect(om.docdropinner).toBe(true);
            expect(om.documentheader).toBe(true);
            expect(om.documentdateheader).toBe(true);

        });
    });

    describe('Verification of getTableHeight function', function () {
        it('It should set dynamic height to the grid', function () {
            om.isOutlook = true;
            var heightobj = om.getTableHeight();
            expect(heightobj).toBeDefined();
            expect(heightobj).not.toBe(null);
        });
        it('It should not set dynamic height to the grid', function () {
            om.isOutlook = false;
            var heightobj = om.getTableHeight();
            expect(heightobj).toBeDefined();
            expect(heightobj).not.toBe(null);

        });
    });
    describe('Verification of isOutlookAsAttachment function', function () {
        it('It should set dynamic height to the grid', function () {
            om.isOutlookAsAttachment(true);
            expect(om.showAttachment).not.toBe(true);
            expect(om.showAttachment).toBeDefined();
            expect(om.enableAttachment).toBe(false);

        });

    });
    describe('Verification of closeNotification function', function () {
        it('It should close all the notification', function () {
            om.closeNotification();
            expect(om.showPopUpHolder).toBe(false);
            expect(om.showSuccessAttachments).toBe(false);

        });
    });

    describe('Verification of search function', function () {
        it('It should perform the search text', function () {
            om.selected = "";
            om.search();
            expect(om.pagenumber).toBe(1);
            expect(om.documentname).toBe("All Documents");
            expect(om.documentid).toBe(1);
            expect(om.lazyloader).toBe(false);
            expect(om.responseNull).toBe(false);
            expect(om.divuigrid).toBe(false);

        });
    });

    describe('Verification of disabled function', function () {
        it('It should set the status', function () {
            var d = new Date()
            var status = om.disabled(d, "day");
            expect(status).toBe(true);
        });
    });

    describe('Verification of showSortExp function', function () {
        it('It should sort as per the ascending order', function () {
            om.sortexp = "test";
            om.sortby = "asc";
            $scope.$apply = function () { };
            om.showSortExp();
            expect(angular.element()).toBeDefined();
        });
    });


    describe('Verification of toggleCheckerAll function', function () {
        it('It should check all checkboxes inside grid', function () {
            $scope.$apply = function () { };
            $scope.gridApi = {
                "selection":
                    { selectAllRows: function () { },
                    clearSelectedRows: function () { }}
            };
            om.gridOptions.data = obj;
            om.toggleCheckerAll(true);
            expect(om.gridOptions.data[0].checker).toBe(true);
            expect(om.documentsCheckedCount).toBe(2);
            expect(om.selectedRows).toBe(obj);
        });
        it('It should not check checkboxes inside grid', function () {
            $scope.$apply = function () { };
            $scope.gridApi = {
                "selection":
                    {
                        selectAllRows: function () { },
                        clearSelectedRows: function () { }
                    }
            };
            om.gridOptions.data = obj;
            om.toggleCheckerAll(false);
            expect(om.gridOptions.data[0].checker).toBe(false);
            expect(om.documentsCheckedCount).toBe(0);
            expect(om.selectedRows).not.toBe(null);
            expect(om.showErrorAttachmentInfo).toBe(false);
        });
    });


});



