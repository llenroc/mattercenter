/// <disable>JS2074, JS3058</disable>
// Test suite
describe("SettingsController test suite", function () {
    var mockapi = function (settingsResource) {
        getData(settingsResource, mockSettingsResource);

    };

    var mockapi = function (settingsResource, callback) {
        getData(settingsResource, mockSettingsResource);
    };
    
    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("settingsResource", ["$resource", "auth", mockSettingsResource]);
    }));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("api", ["matterResource", "documentResource", "documentDashBoardResource", "matterDashBoardResource", "homeResource", "settingsResource", mockapi]);
    }));

    beforeEach(module("ui.router"));
    beforeEach(module("ui.bootstrap"));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller("SettingsController as vm", { $scope: $scope, $state: $state, $stateParams: $stateParams, settingsResource: mockSettingsResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location, $q: $q });
    }));

    describe("Verification of canCreateMatter function", function () {
        it("It should display the navigation content", function () {
            vm.getTaxonomyData();
            expect(vm.taxonomydata).toBeDefined();
            expect(vm.clientlist).toBe(true);
            expect(vm.nodata).toBe(false);
            expect(vm.lazyloader).toBe(true);

        });
    });
    
    describe("Verification of getRolesData function", function () {
        it("It should display the navigation content", function () {
            vm.getTaxonomyData();
            expect(vm.assignRoles).toBeDefined();
            expect(vm.clientlist).toBe(true);
            expect(vm.nodata).toBe(false);
            expect(vm.lazyloader).toBe(true);

        });
    });

    describe("Verification of getPermissionsData function", function () {
        it("It should display the navigation content", function () {
            vm.getPermissionsData();
            expect(vm.assignPermissions).toBeDefined();
            expect(vm.clientlist).toBe(true);
            expect(vm.nodata).toBe(false);
            expect(vm.lazyloader).toBe(true);

        });
    });

    describe("Verification of getPermissionsData function", function () {
        it("It should display the navigation content", function () {
            vm.getPermissionsData();
            expect(vm.assignPermissions).toBeDefined();
            expect(vm.clientlist).toBe(true);
            expect(vm.nodata).toBe(false);
            expect(vm.lazyloader).toBe(true);

        });
    });

    describe("Verification of removeAssignPermissionsRow function", function () {
        it("It should remove the assign permission", function () {
            vm.assignPermissionTeams = "MatterCenter";
            vm.assignPermissionTeams = {
                "splice": function (index, data) { vm.assignPermissionTeams = true; }
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
            vm.assignRoles = ["test"], ["test"];
            vm.assignPermissions = ["test"], ["test"];
            vm.addNewAssignPermissions();
            expect(vm.assignPermissionTeams).toBeDefined();
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
            expect(vm.notificationPopUpBlock).toBeUndefined();
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
            expect(vm.notificationPopUpBlock).toBeUndefined()
        });
    });


    describe("Verification of saveSettings function", function () {
        it("It should add the assign permission", function () {
            vm.saveSettings();
            expect(vm.lazyloader).toBe(false);
            expect(vm.clientlist).toBe(true);
            expect(vm.showClientDetails).toBe(false);
            expect(vm.successmessage).toBe(false);
        });

    });
    describe("Verification of getBoolValues function", function () {
        it("It should add the assign permission", function () {
        
            var data = vm.getBoolValues("Yes");
            expect(data).toBe(true);
        });

    });

    describe("Verification of showSettings function", function () {
        it("It should add the assign permission", function () {

            vm.showSetting();
            expect(vm.clientlist).toBe(true);
            expect(vm.showClientDetails).toBe(false);
            expect(vm.successmessage).toBe(false);
        });
    });

    describe("Verification of showSettings function", function () {
        it("It should add the assign permission", function () {
            var data = {
                "DefaultMatterName": "Test",
                "DefaultMatterId": 1122,
                "IsRestrictedAccessSelected":true,
                "IsCalendarSelected": true,
                "IsRSSSelected": true,
                "IsEmailOptionSelected": true,
                "IsTaskSelected": true,
                "IsMatterDescriptionMandatory": true,
                "IsConflictCheck": true
            };
            vm.setClientData(data);
            expect(vm.assignteam).toBe("Yes");
            expect(vm.calendar).toBe("Yes");
            expect(vm.rss).toBe("Yes");
            expect(vm.email).toBe("Yes");
            expect(vm.tasks).toBe("Yes");
            expect(vm.matterdesc).toBe("Yes");
            expect(vm.conflict).toBe("Yes");
            expect(vm.showmatterconfiguration).toBe("DateTime");
        });
   
        it("It should add the assign permission", function () {
            var data = {
                "DefaultMatterName": "Test",
                "DefaultMatterId": 1122,
                "IsRestrictedAccessSelected": false,
                "IsCalendarSelected": false,
                "IsRSSSelected": false,
                "IsEmailOptionSelected": false,
                "IsTaskSelected": false,
                "IsMatterDescriptionMandatory": false,
                "IsConflictCheck": false
            };
            vm.setClientData(data);
            expect(vm.assignteam).toBe("No");
            expect(vm.calendar).toBe("No");
            expect(vm.rss).toBe("No");
            expect(vm.email).toBe("No");
            expect(vm.tasks).toBe("No");
            expect(vm.matterdesc).toBe("No");
            expect(vm.conflict).toBe("No");
            expect(vm.showmatterconfiguration).toBe("DateTime");
        });
    });

    describe("Verification of showSelectedClient function", function () {
        it("It should add the assign permission", function () {

            vm.showSelectedClient("Test","http://lcadms.sharepoint.com");
            expect(vm.lazyloader).toBe(false);
            expect(vm.selected).toBe("Test");
            expect(vm.clienturl).toBe("http://lcadms.sharepoint.com");
            expect(vm.nodata).toBe(false);
            expect(vm.lazyloader).toBe(false);
            expect(vm.clientlist).toBe(true);
            expect(vm.showClientDetails).toBe(false);
        });
    });

});


//getTaxonomyData
//getRolesData
//getPermissionsData
//searchUsers
//showSelectedClient
//setClientData
//addNewAssignPermissions
//removeAssignPermissionsRow
//onSelect
//saveSettings
//showSettings
