/// <disable>JS2074, JS3058</disable>
// Test suite
describe("Navigation Controller test suite", function () {
    "use strict";
    
    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("api", ["matterResource", "documentResource", "documentDashBoardResource", "matterDashBoardResource", "homeResource", mockapi]);
    }));

    beforeEach(module("ui.router"));
    beforeEach(module("ui.bootstrap"));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller("navigationController as vm", { $scope: $scope, $state: $state, $stateParams: $stateParams, api: mockapi, $rootScope: rootScope });
    }));

    describe("Verification of menuClick function", function () {
        it("It should display the navigation content", function () {
            vm.menuClick();
            expect(vm.welcomeheader).toBe(false);
            expect(vm.emailsubject).toBe(oTestConfiguration.sEmailSubject);
            expect(vm.navigationContent).toBeDefined();
            expect(vm.learnmore).toBe(oTestConfiguration.sLearnMoreLink);
        });
    });
});


