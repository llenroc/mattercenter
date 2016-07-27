//Test suite
describe('MatterDashBoard Controller test suite', function () {
    var $scope = {};
    var pm;
    var api;
    var matterResource;
    var rootScope = {};
    var $state = { go: function () { } };
    var $stateParams;
    var $interval = { go: function () { } };
    var $watch;
    var $http;
    var $location;
    var $q = {};
    $q.defer = function () { };

    var mockmatterDashBoardResource = function ($resource, auth) {
        return $resource(null, null,
            {
                'get': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/get',
                    isArray: true
                }),
                'getPinnepmatters': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/getpinned',
                    isArray: true
                }),
                'getMyMatters': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/getpinned',
                    isArray: true
                }),
                'getTaxonomyDetails': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/taxonomy/gettaxonomy'
                }),
                'UnpinMatter': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/unpin'
                }),
                'PinMatter': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/pin'
                }),
                'getFolderHierarchy': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/getfolderhierarchy'

                }),
                'getMatterCounts': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/getmattercounts'

                }),
            });
    };

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
                  // $scope.$parent.pm.Error = true;
                  //pm.Error = true;
                  var message = 'oops something went wrong. ';
                  if (e.data != null)
                      if (e.data.Message != undefined)
                          message = e.data.Message;
                      else
                          message = e.data;
                  else
                      message = e.message + e.description + e.stack + e.number;

                  // alert(message);
                  //$scope.$parent.pm.guid = "kdsjfl";
                  // pm.guid = "kdsjfl";

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

    };

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("matterDashBoardResource", ['$resource', 'auth', mockmatterDashBoardResource]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
       $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        pm = $controller('MatterDashBoardController as pm', { $scope: $scope, $state: $state, $stateParams: $stateParams, matterDashBoardResource: mockmatterDashBoardResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location ,$q: $q });
    }));

    describe('Verification of closealldrops function', function () {
        it('It should set all drop down value as false with click on page', function () {
            pm.closealldrops();
            expect(pm.searchdrop).toBe(false);
            expect(pm.downwarddrop).toBe(true);
            expect(pm.upwarddrop).toBe(false);
            expect(pm.clientdrop).toBe(false);
            expect(pm.clientdropvisible).toBe(false);
            expect(pm.pgdrop).toBe(false);
            expect(pm.pgdropvisible).toBe(false);
            expect(pm.aoldrop).toBe(false);
            expect(pm.aoldropvisible).toBe(false);
            expect(pm.sortbydrop).toBe(false);
            expect(pm.sortbydropvisible).toBe(false);
        });
    });

    describe('Verification of hideinnerdrop function', function () {
        it('It should set innerdrop box as hidden', function () {
            pm.hideinnerdrop(event);
            expect(pm.clientdrop).toBe(false);
            expect(pm.clientdropvisible).toBe(false);
            expect(pm.pgdrop).toBe(false);
            expect(pm.pgdropvisible).toBe(false);
            expect(pm.aoldrop).toBe(false);
            expect(pm.aoldropvisible).toBe(false);
           
        });
    });

    describe('Verification of filterSearchOK function', function () {
        it('It should return selected client', function () {
            pm.selectedClients = '';
            pm.clients = clientobj;
            pm.filterSearchOK("client");
            expect(pm.clientdrop).toBe(false);
            expect(pm.clientdropvisible).toBe(false);
            expect(pm.selectedClients).toBe("A. Datum Corporation,");
        });

        it('It should return selected practice group', function () {
            pm.selectedPGs = '';
            pm.practiceGroups = practicegroup;
            pm.filterSearchOK("pg");
            expect(pm.pgdropvisible).toBe(false);
            expect(pm.pgdrop).toBe(false);
            expect(pm.selectedPGs).toBe("Advertising, Marketing ＆ Promotions,");

        });
        it('It should return selected AOL', function () {
            pm.selectedAOLs = '';
            pm.aolTerms = practicegroup;
            pm.filterSearchOK("aol");
            expect(pm.aoldropvisible).toBe(false);
            expect(pm.aoldrop).toBe(false);
            expect(pm.selectedAOLs).toBe("Advertising, Marketing ＆ Promotions,");

        });
    });
   
});

//closealldrops
//hideinnerdrop
//filterSearchOK
//filterSearchCancel
//showupward
//showdownward
//showsortby
//pagination
//overwriteConfiguration
//contentCheckNotification
//abortContentCheck
//closeSuccessBanner
