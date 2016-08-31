/// <disable>JS2074, JS3058</disable>
//// ***********************************************************************
//// Author           : MAQ USER
//// Created          : 31-08-2016
////
//// ***********************************************************************
//// <copyright file="DocumentController.spec.js" company="MAQSoftware">
////  Copyright (c) . All rights reserved.
//// </copyright>
//// <summary>Test suite for document controller</summary>
//// ***********************************************************************
describe("documents Controller test suite", function () {
    "use strict";

    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("documentResource", ["$resource", "auth", mockDocumentResource]);
    }));

    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("api", ["matterResource", "documentResource", "documentDashBoardResource", "matterDashBoardResource", "homeResource", mockapi]);
    }));

    beforeEach(module("ui.router"));
    beforeEach(module("ui.bootstrap"));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller("documentsController as vm", { $scope: $scope, $state: $state, $stateParams: $stateParams, documentResource: mockDocumentResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location, $q: $q, $animate: $animate });
    }));

    describe("Verification of showdocdrop function", function () {
        it("It should display the matters dropdown in resposive ", function () {
            vm.docdropinner = true;
            vm.showdocdrop(event);
            expect(vm.documentsdrop).toBe(true);
            expect(vm.docdropinner).toBe(false);
        });

        it("It should hide the matters dropdown in resposive ", function () {
            vm.docdropinner = false;
            vm.showdocdrop(event);
            expect(vm.documentsdrop).toBe(false);
            expect(vm.docdropinner).toBe(true);
        });
    });

    describe("Verification of closealldrops function", function () {
        it("It should close all dropdown menu", function () {
            vm.closealldrops();
            expect(vm.documentsdrop).toBe(false);
            expect(vm.docdropinner).toBe(true);
            expect(vm.documentheader).toBe(true);
            expect(vm.documentdateheader).toBe(true);
        });
    });

    describe("Verification of getTableHeight function", function () {
        it("It should set dynamic height of the grid", function () {
            vm.isOutlook = true;
            var oTableHeight = vm.getTableHeight();
            expect(oTableHeight).toBeDefined();
            expect(oTableHeight).not.toBe(null);
        });

        it("It should not set dynamic height of the grid", function () {
            vm.isOutlook = false;
            var oTableHeight = vm.getTableHeight();
            expect(oTableHeight).toBeDefined();
            expect(oTableHeight).not.toBe(null);
        });
    });

    describe("Verification of isOutlookAsAttachment function", function () {
        it("It should show outlook as an attachment", function () {
            vm.isOutlookAsAttachment(true);
            expect(vm.showAttachment).not.toBe(true);
            expect(vm.showAttachment).toBeDefined();
            expect(vm.enableAttachment).toBe(false);
        });
    });

    describe("Verification of closeNotification function", function () {
        it("It should close all the notifications", function () {
            vm.closeNotification();
            expect(vm.showPopUpHolder).toBe(false);
            expect(vm.showSuccessAttachments).toBe(false);
        });
    });

    describe("Verification of search function", function () {
        it("It should perform the text search in all documents", function () {
            vm.selected = "";
            vm.search();
            expect(vm.pagenumber).toBe(1);
            expect(vm.documentname).toBe("All Documents");
            expect(vm.documentid).toBe(1);
            expect(vm.lazyloader).toBe(false);
            expect(vm.responseNull).toBe(false);
            expect(vm.divuigrid).toBe(false);
        });
    });

    describe("Verification of disabled function", function () {
        it("It should set the status", function () {
            var oDate = new Date();
            var bStatus = vm.disabled(oDate, "day");
            expect(bStatus).toBe(true);
        });
    });

    describe("Verification of showSortExp function", function () {
        it("It should sort the data in ascending order", function () {
            vm.sortexp = "test";
            vm.sortby = "asc";
            vm.showSortExp();
            expect(angular.element()).toBeDefined();
        });
    });

    // Test suite to execute test cases.
    describe("Verification of toggleCheckerAll function", function () {
        // Test case to validate function.
        it("It should check all the checkboxes inside grid", function () {
            vm.gridOptions.data = obj;
            vm.toggleCheckerAll(true);
            expect(vm.gridOptions.data[0].checker).toBe(true);
            expect(vm.documentsCheckedCount).toBe(oTestConfiguration.nDocumentCheckCount);
            expect(vm.selectedRows).toBe(obj);
        });

        it("It should uncheck all the checkboxes inside grid", function () {
            vm.gridOptions.data = obj;
            vm.toggleCheckerAll(false);
            expect(vm.gridOptions.data[0].checker).toBe(false);
            expect(vm.documentsCheckedCount).toBe(0);
            expect(vm.selectedRows).not.toBe(null);
            expect(vm.showErrorAttachmentInfo).toBe(false);
        });
    });

    /*
    //-------------------------------------------------------------------------
    describe("Verification of sendDocumentAsAttachment function", function () {
        // Test case to validate function.
        it("It should send document as attachment", function () {
            vm.sendDocumentAsAttachment();
           //vm.selectedRows
            //{
             //   length: 5

           // };
        expect(vm.enableAttachment).toBe(true);
        expect(vm.errorAttachDocument).toBe(false);
        expect(vm.asyncCallCompleted).toBe(1);
        expect(vm.showFailedAtachments).toBe(false);
        expect(vm.showPopUpHolder).toBe(true);
        expect(vm.attachedProgressPopUp).toBe(true);
        });

        it("It should not send document as attachment", function () {
            vm.sendDocumentAsAttachment();
          //vm.selectedRows
                // {
                //    length: 10
    //             };
            expect(vm.enableAttachment).toBe(false);
            expect(vm.errorAttachDocument).toBe(true);
        });
    });
     
    describe("Verification of searchDocument function", function () {
        it("Inside searchDocument function", function () {
        vm.searchDocument("test");
        expect(vm.pagenumber).toBe(1);
    });
 });

    describe("Verification of FilterModifiedDate function", function () {
        it("It should filter the modified date ", function () {
         vm.FilterModifiedDate("test");
         expect(vm.lazyloader).toBe(false);
        expect(vm.divuigrid).toBe(false);
    });

    it("It should filter based on modified date ", function () {
        vm.FilterModifiedDate("Modified Date");
        expect(vm.moddatefilter).toBe(true);      
    });

    it("It should filter based on created date", function () {
        vm.FilterModifiedDate("Modified Date");
        expect(vm.createddatefilter).toBe(true);

    });
 });

    describe("Verification of clearFilters function", function () {
        it("It should clear all the filters", function () {
        vm.clearFilters("Test");
        expect(vm.documentheader).toBe(true);
        expect(vm.documentdateheader).toBe(true);
        expect(vm.lazyloader).toBe(false);
        expect(vm.nodata).toBe(false);
        expect(vm.pagenumber).toBe(1);
    });

    it("It should clearfilters based on LastModifiedTime and search term", function () {
        vm.clearFilters("Test");
        expect(vm.documentfilter).toBe(false);         
    });

    it("It should clear filters based on LastModified time and client name", function () {
        vm.clearFilters("Test");
        expect(vm.clientfilter).toBe(false);
    });
    it("It should clear filters based on search term", function () {
        vm.clearFilters("Test");
        expect(vm.checkoutfilter).toBe(false);
    });

    it("It should clear filters based on search term and document author", function () {
        vm.clearFilters("Test");
        expect(vm.authorfilter).toBe(false);
    });

    it("It should clear filters based on LastModified date", function () {
        vm.clearFilters("Test");
        expect(vm.moddatefilter).toBe(false);
    });

    it("It should clear filters based on CreatedDate ", function () {
        vm.clearFilters("Test");
        expect(vm.createddatefilter).toBe(false);
    });
});

    describe("Verification of getDocumentAssets function", function () {
        it("It should get all document assests", function () {
        vm.searchDocument("test");
        expect(vm.assetsuccess).toBe(false);
    });
});

    describe("Verification of openDocumentHeader function", function () {
        it("It should open the document author", function () {
        //vm.openDocumentHeader("test");
        expect(vm.filternodata).toBe(false);
    });
});
    */
});