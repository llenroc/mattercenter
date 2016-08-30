/// <disable>JS2074, JS3058</disable>
// Test suite
describe("MatterUsers Controller test suite", function () {
    "use strict";
    var mockapi = function (matterResource) {
        getData(matterResource, mockMatterResource);

    };
   
    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("matterResource", ["$resource", "auth", mockMatterResource]);
    }));

    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("api", ["matterResource", "documentResource", "documentDashBoardResource", "matterDashBoardResource", "homeResource", mockapi]);
    }));

    beforeEach(module("ui.router"));
    beforeEach(module("ui.bootstrap"));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller("MatterUsersController as vm", { $scope: $scope, $state: $state, $stateParams: $stateParams, matterResource: mockMatterResource, api: mockapi, $rootScope: rootScope, $location: $location });
    }));


    describe("Verification of CheckPopUp function", function () {
        it("It should check error pop up", function () {
            vm.errorStatus = false;
            vm.CheckPopUp("test");
            expect(vm.errorPopUpBlock).toBe(false);
            expect(vm.errorBorder).toBe("");
            expect(vm.errorStatus).toBe(false);
        });

        it("It should not check the error pop up", function () {
            vm.errorStatus = true;
            vm.CheckPopUp("test");
            expect(vm.errorStatus).toBe(false);
        });
    });

    describe("Verification of removeAssignPermissionsRow function", function () {
        it("It should remove the assign permission", function () {
            vm.assignPermissionTeams = "MatterCenter";
            vm.assignPermissionTeams = {
                "splice":function (index, data) { vm.assignPermissionTeams = true; }
            }
            vm.removeAssignPermissionsRow(3);
            expect(vm.assignPermissionTeams).toBeDefined();
        });
    });

    describe("Verification of addNewAssignPermissions function", function () {
        it("It should add the assign permission", function () {
            vm.assignPermissionTeams = "MatterCenter";
            vm.assignPermissionTeams = {
                "push": function (data) { vm.assignPermissionTeams = data; }
            }
            vm.assignRoles=["test"],["test"];
            vm.assignPermissions = ["test"], ["test"];
            vm.addNewAssignPermissions();
            expect(vm.assignPermissionTeams).toBeDefined();
        });

    });

    describe("Verification of confirmUser function", function () {
        it("It should confirm the user", function () {
            
            vm.confirmUser(true);
            expect(vm.notificationPopUpBlock).toBe(false);
            expect(vm.notificationBorder).toBe("");

        });
        it("It should confirm the user", function () {
            vm.textInputUser = { "assignedUser": null };
            vm.confirmUser(false);
            expect(vm.notificationPopUpBlock).toBe(false);
            expect(vm.notificationBorder).toBe("");
            expect(vm.textInputUser.assignedUser).toBe("");

        });

    });

    //describe("Verification of UpdateMatter function", function () {
    //    it("It should updates the matter details", function () {
    //        vm.matterProperties = { "matterObject": { "blockUserNames": "MAQ user" } };
    //        vm.UpdateMatter(event);
    //        expect(true).toBe(true);

    //    });
    //});

    describe("Verification of checkUserExists function", function () {
        it("It should check UserExists", function () {

            vm.checkUserExists("maquser@lcadms.onmicrosoft.com", event);
            expect(vm.notificationPopUpBlock).toBe(false);

        });
        it("It should checkUserExists", function () {
            vm.assignPermissionTeams = [{ "team": { "assignedUser": "maquser@lcadms.onmicrosoft.com" } }];
            vm.checkUserExists("maquser@test.onmicrosoft.com", event);
            expect(vm.notificationPopUpBlock).toBe(true);
        });
    });
 
    describe("Verification of onSelect function", function () {
        it("It should return the conflicted user", function () {
            var $item = {
                email: "",
                name: "No results found"
            };
            var $label = { "assignedUser": "maquser@lcadms.onmicrosoft.com" };
            vm.onSelect($item, $model, $label, "conflictcheckuser", "on-blurr", event);
            expect(vm.notificationPopUpBlock).toBe(false);
        });

        it("It should return the blocked user", function () {
            var $item = {
                email: "maquser@lcadms.onmicrosoft.com",
                name: "MAQ user"
            };
            var $label = { "assignedUser": "maquser@lcadms.onmicrosoft.com" };
            vm.oSiteUsers = { 
                "indexOf": function ($item) { return 1; }
                };
            vm.onSelect(item, $model, $label, "team", "on-blurr", event, item.name);
            expect(vm.typehead).toBe(false);
            expect(vm.notificationPopUpBlock).toBe(false);
        });
    });

    
});


//searchUsers
//CheckPopUp
//removeAssignPermissionsRow
//addNewAssignPermissions
//confirmUser
//checkUserExists
//onSelect
//UpdateMatter
