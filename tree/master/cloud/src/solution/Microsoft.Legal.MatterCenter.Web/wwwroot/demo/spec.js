describe('Calculator Controller', function () {
    var $scope = {};
    var vm;
    var environment;
    var rootScope;
    var utils;
    var state = { go: function () { } };


    beforeEach(module('app'));

    beforeEach(inject(function ($controller, $http, $rootScope) {
        rootScope = $rootScope.$new();
        
        vm = $controller('LoginController', { $scope: $scope, $http: $http});

        console.log($scope.z);

    }));



    describe('Initialize', function () {
        it('should return service name', function ($scope, $http) {
            $scope.data;
            var data = $scope.myWelcome;
            expect(data).not.toBe(null);
        });
    });

});