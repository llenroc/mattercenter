/// <disable>JS2074, JS3058</disable>
//// ***********************************************************************
//// Author           : MAQ USER
//// Created          : 31-08-2016
////
//// ***********************************************************************
//// <copyright file="CreateMatterControllerservicecall.spec.js" company="MAQSoftware">
////  Copyright (c) . All rights reserved.
//// </copyright>
//// <summary>Test suite for createMatter controller for service call</summary>
//// ***********************************************************************
describe("CreateMatter Controller test suite for service call", function () {
    "use strict";

    var mockapi = function (matterResource, callback) {
        getData(matterResource, mockMatterResourceService);
    };

    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("matterResource", ["$resource", "auth", mockMatterResourceService]);
    }));

    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("api", ["matterResource", "documentResource", "documentDashBoardResource", "matterDashBoardResource", "homeResource", mockapi]);
    }));

    beforeEach(module("ui.router"));
    beforeEach(module("ui.bootstrap"));

    beforeEach(inject(function ($controller, $injector, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller("createMatterController as vm", { $scope: $scope, $rootScope: rootScope, $state: $state, $stateParams: $stateParams, matterResource: mockMatterResourceService, api: mockapi });
    }));

    describe("Verification of Check valid matter function", function () {
        it("It Should return true for new matter", function () {
            vm.matterName = oTestConfiguration.sValidMatterName;
            vm.clientUrl = oTestConfiguration.sValidMatterClientURL;
            vm.checkValidMatterName();
            expect(vm.errorPopUpBlock).toBe(false);
        });
    });

    ////This function is removed in latest build of Microsoft
    ////describe("Verification of selectMatterType function", function () {
    ////    it("It should successfully get Practice Group, Area of law and SubArea of law", function () {
    ////        vm.selectMatterType();
    ////        expect(vm.popupContainerBackground).toBe("Show");
    ////        expect(vm.popupContainer).toBe("Show");
    ////    });
    ////});

    describe("Verification of getSelectedClientValue function", function () {
        it("It should successfully get data from matter configuration list", function () {
            vm.getSelectedClientValue(oTestConfiguration.oClientObj);
            expect(vm.secureMatterCheck).toBe(true);
            expect(vm.includeCalendar).toBe(true);
            expect(vm.includeEmail).toBe(true);
            expect(vm.includeRssFeeds).toBe(true);
            expect(vm.defaultConfilctCheck).toBe(true);
            expect(vm.isMatterDescriptionMandatory).toBe(true);
            expect(vm.includeTasks).toBe(true);
        });
    });

    ////describe("Verification of createMatterButton function", function () {
    ////    it("It should successfully create matter button", function () {
    ////        vm.chkConfilctCheck = true;
    ////        vm.conflictDate = "8/1/2016";
    ////        vm.createMatterButton(event);
    ////        expect(localStorage.getItem("IsRestrictedAccessSelected")).toBe("true");
    ////        expect(localStorage.getItem("IsCalendarSelected")).toBe("true");
    ////        expect(localStorage.getItem("IsRSSSelected")).toBe("true");
    ////        expect(localStorage.getItem("IsEmailOptionSelected")).toBe("true");
    ////        expect(localStorage.getItem("IsConflictCheck")).toBe("true");
    ////        expect(localStorage.getItem("IsMatterDescriptionMandatory")).toBe("false");
    ////        expect(localStorage.getItem("IsTaskSelected")).toBe("true");
    ////    });
    ////});
});
