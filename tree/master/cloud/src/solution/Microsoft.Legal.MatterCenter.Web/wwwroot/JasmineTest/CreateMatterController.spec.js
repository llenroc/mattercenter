var selectedPracticeGroup={
    "termName": "Advertising, Marketing ＆ Promotions",
    "parentTermName": "Practice Groups",
    "folderNames": "Email;Lorem;Ipsum",
    "areaTerms": [
    {
        "termName": "Advertising, Marketing ＆ Promotions",
        "parentTermName": "Advertising, Marketing ＆ Promotions",
        "folderNames": "Email;Lorem;Ipsum",
        "subareaTerms": [
        {
            "termName": "Advertising, Marketing ＆Promotions",
            "parentTermName": "Advertising, Marketing ＆ Promotions",
            "folderNames": "Email;Lorem;Ipsum",
            "isNoFolderStructurePresent": "false",
            "documentTemplates": "Advertising, Marketing ＆ Promotions",
            "documentTemplateNames": "Agribusiness;Aircraft;California Public Utilities Commission (CPUC);Class Action Defense",
            "id": "683ec070-7ed0-4e82-b07c-13a1b4485b7b",
            "wssId": 0,
            "subareaTerms": null,
            "$$hashKey": "object:153"
        }
        ],
        "id": "16827aa4-a8b3-4275-920b-184a04bc60ea",
        "wssId": 0,
        "$$hashKey": "object:149"
    }
    ],
    "id": "a42ab615-0d27-4de2-9f55-144e71219770",
    "wssId": 0,
    "$$hashKey": "object:122"
}



//Test suite
describe('Matter Center Test suite', function () {
    var $scope = {};
    var vm;
    var api;
    var matterResource;
    var rootScope = {};
    var $filter;
    var $state = { go: function () { } };
    var $stateParams;
    var $window;

    var mockmatterResourceService = function ($resource, auth) {
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
    };

    var mockapi = function api(matterResource, documentResource, documentDashBoardResource, matterDashBoardResource, homeResource) {
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

    };

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("matterResource", mockmatterResourceService);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("api", mockapi);
    }));

    beforeEach(module('ui.router'));
    beforeEach(module('ui.bootstrap'));

    beforeEach(inject(function ($controller, $injector, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller('createMatterController as vm', { $scope: $scope, $rootScope: rootScope, $state: $state, $stateParams: $stateParams, matterResource: mockmatterResourceService, api: mockapi });
    }));

    describe('Verification of clearpopup function', function () {
        it('Test wether errorPopUpBlock is enable or disable', function () {
            vm.clearPopUp();
            var val = vm.errorPopUpBlock;
            expect(val).toBe(false);
        });
    });

    describe('Verification of getMatterGUID function', function () {
        it('Test whether matterGUID length is 32 bit or not', function () {

            var val = vm.matterGUID;
            expect(val.length).toBe(32);
        });
    });

    describe('Verification of searchUsers function', function () {
        it('Test whether function is getting defined or not', function () {

            var val = vm.searchUsers;
            expect(val).not.toBe(null);
        });
    });

    describe('Verification of selectMatterTypePopUpClose function', function () {
        it('Test whether popupContainerBackground and popupContainer is hide or not', function () {
            vm.popupContainer = "Show";
            vm.selectMatterTypePopUpClose();
            var val = vm.popupContainerBackground;
            var data = vm.popupContainer;
            expect(val).toBe("hide");
            expect(data).toBe("hide");
        });
    });

    describe('Verification of getSelectedPracticeGroupValue function', function () {
        it('Test whether hierarchy value is set or not', function () {
            vm.getSelectedPracticeGroupValue();
            vm.selectedPracticeGroup = selectedPracticeGroup;
            if (vm.selectedPracticeGroup!= null)
            {
                areaOfLawTerms = vm.selectedPracticeGroup.areaTerms;
                subAreaOfLawTerms = vm.selectedPracticeGroup.areaTerms[0].subareaTerms;
                activeSubAOLTerm = vm.selectedPracticeGroup.areaTerms[0].subareaTerms[0];
                activeAOLTerm = vm.selectedPracticeGroup.areaTerms[0];

                expect(areaOfLawTerms).not.toBe(null);
                expect(subAreaOfLawTerms).not.toBe(null);
                expect(activeSubAOLTerm.termName).toBe("Advertising, Marketing ＆Promotions");
                expect(activeAOLTerm.folderNames).toBe("Email;Lorem;Ipsum");
                expect(vm.errorPopUp).toBe(false);
                
            }
            else
            {
                expect(areaOfLawTerms).toBe(null);
                expect(subAreaOfLawTerms).toBe(null);
            }

            
        });
    });

    describe('Verification of selectAreaOfLawTerm function', function () {
        it('Test whether selectAreaOfLawTerm has valid value or not', function () {
            vm.selectAreaOfLawTerm(selectedPracticeGroup.areaTerms[0]);
            var subArea = selectedPracticeGroup.areaTerms[0].subareaTerms;
            var activeSub = selectedPracticeGroup.areaTerms[0].subareaTerms[0];

            expect(subArea).not.toBe(null);
            expect(activeSub.termName).toBe("Advertising, Marketing ＆Promotions");
            expect(vm.errorPopUp).toBe(false);

        });
    });

    describe('Verification of selectSubAreaOfLawTerm function', function () {
        it('Test whether selectSubAreaOfLawTerm has valid value or not', function () {
      
            vm.selectSubAreaOfLawTerm(selectedPracticeGroup.areaTerms[0].subareaTerms[0]);
            subArea = selectedPracticeGroup.areaTerms[0].subareaTerms;
            activeSub = selectedPracticeGroup.areaTerms[0].subareaTerms[0];

            expect(subArea).not.toBe(null);
            expect(activeSub.termName).toBe("Advertising, Marketing ＆Promotions");
            expect(vm.errorPopUp).toBe(false);

        });
    });


    // Pending methods to verify
    //getSelectedPracticeGroupValue
    //selectAreaOfLawTerm
    //selectSubAreaOfLawTerm
    //selectDocumentTemplateTypeLawTerm
    //addToDocumentTemplate
    //removeFromDocumentTemplate
    //onSelect
    //saveDocumentTemplates
    //open1
    //conflictRadioChange
    //addNewAssignPermissions
    //removeAssignPermissionsRow
    //createAndNotify
    //NextClick
    //PreviousClick
    //CheckPopUp
    //closesuccessbanner

    //describe('Verification of CheckMatterName function', function () {
    //    it('Test whether Matter name is already  present or not', function () {

           
    //        vm.matterName = "abcrtawc";
    //        vm.clientUrl = "https://lcadms.sharepoint.com/sites/AdventureWorksCycles";
    //        vm.checkValidMatterName();
    //        setTimeout(function () {
    //            debugger;
    //            console.log(vm.errTextMsg);
    //        }, 50000);
    //        debugger;
    //       // {
                
    //        //    //expect(response).toBe("s");
    //        //});
    //        //var val = vm.checkValidMatterName();
    //        //console.log(val);
    //        //expect(val).toBe(true);
    //    });
    //});



});
