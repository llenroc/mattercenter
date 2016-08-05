(function () {
    'use strict';

    angular.module("matterMain")
        .controller('navigationController', ['$state', '$stateParams', 'api', '$rootScope',
        function ($state, $stateParams, api, $rootScope) {

            var vm = this;
            vm.sum = function () {
                vm.z = vm.x + vm.y;
            }

            //#endregion
        }]);
})();
