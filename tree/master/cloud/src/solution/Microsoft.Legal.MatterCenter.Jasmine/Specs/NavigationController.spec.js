//Test suite
describe('navigation Controller test suite', function () {


    beforeEach(module('matterMain'));
    

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));
    
    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        xm = $controller('navigationController as xm', {$scope:$scope , $state: $state, $stateParams: $stateParams, api: mockapi, $rootScope: rootScope});
    }));

    describe('Verification of menuClick function', function () {
        it('It should set the Navigation content', function () {
            xm.menuClick();
            expect(xm.welcomeheader).toBe(false);
            expect(xm.emailsubject).toBe("CELA Project Center Feedback and Support request");
            expect(xm.navigationContent).toBeDefined();
            expect(xm.learnmore).toBe("http://www.microsoft.com/mattercenter");

        });
    });

});


