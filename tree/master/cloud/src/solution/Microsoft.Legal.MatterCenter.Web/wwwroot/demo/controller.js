(function () {
    angular.module('app', []);

    // Registers a controller to our module 'calculatorApp'.
    angular.module('app').controller('CalculatorController', function CalculatorController($scope) {
        var vm = $scope;

        vm.z = 5;
        vm.sum = function () {
            vm.z = vm.x + vm.y;
        };
    });


    // load the app
    angular.element(document).ready(function () {
        angular.bootstrap(document, ['app']);
    });
}
)();