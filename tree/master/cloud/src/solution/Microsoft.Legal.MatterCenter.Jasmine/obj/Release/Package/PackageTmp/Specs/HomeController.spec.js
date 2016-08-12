//Test suite
describe('Home Controller test suite', function () {
    var mockapi = function () {
    };

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("homeResource", ['$resource', 'auth', mockhomeResource]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        dm = $controller('homeController as dm', { $scope: $scope, $state: $state, $stateParams: $stateParams, homeResource: mockhomeResource, api: mockapi, $rootScope: rootScope, $location: $location, adalAuthenticationService: adalService });
    }));

    //describe('Verification of setWidth function', function () {
    //    it('It should set width of screen', function () {

    //        dm.x = 5;
    //        dm.y = 2;
    //        dm.sum();
    //        expect(dm.z).toBe(7);
    //    });
    //});


   

});


