//Test suite
describe('CreateMatter Controller test suite', function () {
    var $scope = {};
    var vm;
    var api;
    var matter;
    var rootScope = {};
    var $filter;
    var $state = { go: function () { } };
    var $stateParams;
    var $window;
    var deffer;

    var mockmatterResourceService = angular.module('matterMain').factory('matterResource', ['$resource', 'auth',
      function matterResource($resource, auth) {

          return $resource(null, null,
              {
                  'get': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/get',
                      isArray: true
                  }),
                  'getPinnedMatters': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/getpinned',
                      isArray: true
                  }),
                  'UnpinMatters': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/unpin'
                  }),
                  'PinMatters': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/pin'
                  }),
                  'getTaxonomyDetails': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/taxonomy/gettaxonomy'
                  }),
                  'checkMatterExists': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/checkmatterexists'
                  }),
                  'getDefaultMatterConfigurations': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/getconfigurations'
                  }),
                  'getUsers': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/user/getusers',
                      isArray: true
                  }),
                  'getRoles': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/user/getroles',
                      isArray: true
                  }),
                  'getPermissionLevels': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/user/getpermissionlevels',
                      isArray: true
                  }),
                  'checkSecurityGroupExists': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/checksecuritygroupexists'

                  }),
                  'getFolderHierarchy': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/getfolderhierarchy'

                  }),
                  'createMatter': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/create'

                  }),
                  'assignUserPermissions': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/assignuserpermissions'

                  }),
                  'assignContentType': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/assigncontenttype'

                  }),
                  'createLandingPage': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/createlandingpage'
                  }),
                  'updateMatterMetadata': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/UpdateMetadata'
                  }),
                  'getStampedProperties': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/matter/getstampedproperties'
                  }),
                  'uploadEmail': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/document/UploadMail'
                  }),
                  'uploadAttachment': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/document/UploadAttachments'
                  }),
                  'uploadfiles': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/document/UploadAttachments'
                  }),
                  'getHelp': auth.attachCSRF({
                      method: 'POST',
                      url: '/api/v1/shared/help',
                      isArray: true
                  })
              });
      }]);

    var mockapi = function (matterResource, documentResource, documentDashBoardResource, matterDashBoardResource, homeResource) {
        var resources = {
            'matterResource': matterResource,
            'documentResource': documentResource,
            'documentDashBoardResource': documentDashBoardResource,
            'matterDashBoardResource': matterDashBoardResource,
            'homeResource': homeResource
        };

        function callAPI(options) {

            var resource = resources[options.resource];



            resource[options.method](options.data)
              .$promise.then(function (response) {

                  options.success(response);
              }).catch(function (e) {
                  // $scope.$parent.vm.Error = true;
                  //vm.Error = true;
                  var message = 'oops something went wrong. ';
                  if (e.data != null)
                      if (e.data.Message != undefined)
                          message = e.data.Message;
                      else
                          message = e.data;
                  else
                      message = e.message + e.description + e.stack + e.number;

                  // alert(message);
                  //$scope.$parent.vm.guid = "kdsjfl";
                  // vm.guid = "kdsjfl";

                  if (options.error) {
                      options.error(e);
                  } else {
                      ////if (e.status === 500) {
                      ////    window.location.href = "/Error/Index";
                      ////}
                      ////else {
                      ////    //ErrorNotification.notifyError(e.status);
                      ////}
                  }
              }).finally(function () {

              });

        }

        return function (api_options) {
            var apiOptions = api_options || {};
            if (!api_options.resource || !resources.hasOwnProperty(api_options.resource) ||
                !api_options.method || !api_options.success) {
                throw new Error('Invalid request. API, method and success are required.');
            }
            api_options.data = api_options.data || {};
            callAPI(apiOptions);
        };

    }

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("matterResource",['$resource', 'auth', mockmatterResourceService]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));;
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $injector, $rootScope) {

        rootScope = $rootScope.$new();
        vm = $controller('createMatterController as vm', { $scope: $scope, $rootScope: rootScope, $state: $state, $stateParams: $stateParams, matterResource: mockmatterResourceService, api: mockapi });
    }));

    describe('Verification of clearpopup function', function () {
        it('errorPopUpBlock should be set to false', function () {
            vm.clearPopUp();
            var val = vm.errorPopUpBlock;
            expect(val).toBe(false);
        });
    });


    describe('Verification of clearpopup function', function () {
        it('errorPopUpBlock should be set to false', function () {
            vm.matterName = "new";
            vm.clientUrl = "https://lcadms.sharepoint.com/sites/AdventureWorksCycles";
            vm.checkValidMatterName();

            expect(val).toBe(false);
        });
    });

});

