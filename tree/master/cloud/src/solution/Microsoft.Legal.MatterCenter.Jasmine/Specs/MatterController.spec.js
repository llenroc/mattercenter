//Test suite
describe('MattersController Controller test suite', function () {
    var $scope = {};
    var cm;
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

    var mockmatterResource = function ($resource, auth) {
        return $resource(null, null,
            {
                'get': auth.attachCSRF({
                    method: 'POST',
                    url: '/api/v1/matter/get',
                    isArray: true
                }),
                'getPinnecmatters': auth.attachCSRF({
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
                  // $scope.$parent.cm.Error = true;
                  //cm.Error = true;
                  var message = 'oops something went wrong. ';
                  if (e.data != null)
                      if (e.data.Message != undefined)
                          message = e.data.Message;
                      else
                          message = e.data;
                  else
                      message = e.message + e.description + e.stack + e.number;

                  // alert(message);
                  //$scope.$parent.cm.guid = "kdsjfl";
                  // cm.guid = "kdsjfl";

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
        $provide.factory("matterResource", ['$resource', 'auth', mockmatterResource]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        cm = $controller('mattersController as cm', { $scope: $scope, $state: $state, $stateParams: $stateParams, matterResource: mockmatterResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location, $q: $q });
    }));

    describe('Verification of setWidth function', function () {
        it('It should set width of screen', function () {
            cm.setWidth();
            expect(cm.searchResultsLength).toBe(42);
        });
    });


    describe('Verification of closeNotificationDialog function', function () {
        it('It should close notification dialog box', function () {
            cm.closeNotificationDialog();
            expect(cm.IsDupliacteDocument).toBe(false);
            expect(cm.IsNonIdenticalContent).toBe(false);
            expect(cm.showLoading).toBe(false);
        });
    });

    //describe('Verification of attachmentTokenCallbackEmailClient function', function () {
    //    it('It should attach Token along with EmailClient-Need to call createMailPopup', function () {
    //        $scope.$apply = function () { };
    //        var Office = { "context": { "mailbox": { "item": { "itemId": "786" } } } }
    //        var asyncResult = { "status": "succeeded", "value": "testtoken" };
    //        cm.attachmentTokenCallbackEmailClient(asyncResult,obj);
    //        expect(cm.attachmentToken).toBe("testtoken");
    //        expect(cm.mailUpLoadSuccess).toBe(false);
    //        expect(cm.mailUploadedFolder).toBe(null);
    //        expect(cm.loadingAttachments).toBe(false);
    //        expect(cm.mailUploadedFile).toBe(null);
    //    });
        
    //});



    describe('Verification of closealldrops function', function () {
        it('It should close all the dropdowns', function () {
            cm.closealldrops();
            expect(cm.mattersdrop).toBe(false);
            expect(cm.mattersdropinner).toBe(true);
            expect(cm.matterheader).toBe(true);
            expect(cm.matterdateheader).toBe(true);
        });
    });



});
//setWidth
//editAttachment--> no value store
//saveAttachment--> no value store
//closeNotificationDialog
//attachmentTokenCallbackEmailClient
//getIconSource
//checkEmptyorWhitespace
//hideBreadCrumb
//$watch
//disabled
//showSortExp
//showmatterdrop
//closealldrops
//overwriteConfiguration
//contentCheckNotification
//abortContentCheck
//closeSuccessBanner

