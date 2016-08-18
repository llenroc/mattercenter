(function () {
    'use strict;'
    var app = angular.module("matterMain");
    app.controller('DocumentDashBoardController', ['$scope', '$state', '$interval', '$stateParams', 'api', '$timeout', 'documentDashBoardResource', '$rootScope', 'uiGridConstants', '$location', '$http', 'commonFunctions', '$window',
        function documentDashBoardController($scope, $state, $interval, $stateParams, api, $timeout, documentDashBoardResource, $rootScope, uiGridConstants, $location, $http, commonFunctions, $window) {
            var vm = this;
            vm.sum=function()
            {
                vm.z = vm.x + vm.y;
            }
        }
    ]);
}


)();