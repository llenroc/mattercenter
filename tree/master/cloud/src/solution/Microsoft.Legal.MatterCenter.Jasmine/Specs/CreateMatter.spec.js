﻿//Test suite
describe('CreateMatter Controller test suite', function () {
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

    };

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("matterResource", ['$resource', 'auth', mockmatterResourceService]);
    }));

    beforeEach(module('matterMain'));
    beforeEach(module('matterMain', function ($provide) {
        $provide.factory("api", ['matterResource', 'documentResource', 'documentDashBoardResource', 'matterDashBoardResource', 'homeResource', mockapi]);
    }));

    beforeEach(module('ui.router'));
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

        it('errorPopUpBlock should not be set', function () {
            var val = vm.errorPopUpBlock;
            expect(val).toBeUndefined();
        });
    });

    describe('Verification of getMatterGUID function', function () {
        it('Should return the GUID for matter with length of 32 bit', function () {

            var val = vm.matterGUID;
            expect(val.length).toBe(32);
        });

        it('Should return the GUID for matter instead of null value', function () {

            var val = vm.matterGUID;
            expect(val).not.toBe(null);
        });

        it('Should return the GUID for matter with defined value', function () {

            var val = vm.matterGUID;
            expect(val).toBeDefined();
        });
    });

    describe('Verification of selectMatterTypePopUpClose function', function () {
        it('Should return the status of popupContainerBackground and popupContainer as hide', function () {
            vm.popupContainer = "Show";
            vm.selectMatterTypePopUpClose();
            var value = vm.popupContainerBackground;
            var data = vm.popupContainer;
            expect(value).toBe("hide");
            expect(data).toBe("hide");
        });

        it('Should not return the status of popupContainerBackground and popupContainer as hide', function () {
            vm.popupContainer = "hide";
            vm.selectMatterTypePopUpClose();
            var value = vm.popupContainerBackground;
            var data = vm.popupContainer;
            expect(value).toBe("Show");
            expect(data).toBe("hide");
        });
    });

    describe('Verification of getSelectedPracticeGroupValue function', function () {
        it('Should return the selected PracticeGroup Value (all AOL and SAOL terms)', function () {
            vm.selectedPracticeGroup = selectedPracticeGroup;
            vm.getSelectedPracticeGroupValue();

            expect(vm.areaOfLawTerms).not.toBe(null);
            expect(vm.subAreaOfLawTerms).not.toBe(null);
            expect(vm.activeSubAOLTerm.termName).toBe("Advertising, Marketing ＆Promotions");
            expect(vm.activeAOLTerm.folderNames).toBe("Email;Lorem;Ipsum");
            expect(vm.errorPopUp).toBe(false);

        });

        it('Should return the null value for selected PracticeGroup Value (all AOL and SAOL terms)', function () {
            vm.selectedPracticeGroup = null;
            vm.getSelectedPracticeGroupValue();

            expect(vm.areaOfLawTerms).toBe(null);
            expect(vm.subAreaOfLawTerms).toBe(null);
        });

    });

    describe('Verification of selectAreaOfLawTerm function', function () {
        it('Should return the subAOL items on selection of AOLTerm', function () {
            vm.selectAreaOfLawTerm(selectedPracticeGroup.areaTerms[0]);

            expect(vm.subAreaOfLawTerms).not.toBe(null);
            expect(vm.activeSubAOLTerm.termName).toBe("Advertising, Marketing ＆Promotions");
            expect(vm.errorPopUp).toBe(false);
            expect(vm.activeAOLTerm).toBe(selectedPracticeGroup.areaTerms[0]);
        });

        it('Should return defined subAOL items on selection of AOLTerm', function () {
            vm.selectAreaOfLawTerm(selectedPracticeGroup.areaTerms[0]);

            expect(vm.subAreaOfLawTerms).not.toBeUndefined();
            expect(vm.activeSubAOLTerm).not.toBeUndefined();
            expect(vm.errorPopUp).not.toBeUndefined();
            expect(vm.activeAOLTerm).not.toBeUndefined();
        });
    });

    describe('Verification of selectSubAreaOfLawTerm function', function () {
        it('Should return the subAOL items', function () {

            vm.selectSubAreaOfLawTerm(selectedPracticeGroup.areaTerms[0].subareaTerms[0]);
            expect(vm.activeSubAOLTerm).toBe(selectedPracticeGroup.areaTerms[0].subareaTerms[0]);
            expect(vm.errorPopUp).toBe(false);

        });

        it('Should return defined subAOL items', function () {

            vm.selectSubAreaOfLawTerm(selectedPracticeGroup.areaTerms[0].subareaTerms[0]);
            expect(vm.activeSubAOLTerm).not.toBeUndefined();
            expect(vm.errorPopUp).not.toBeUndefined();

        });
    });

    describe('Verification of selectDocumentTemplateTypeLawTerm function', function () {
        it('Should return the document template type term', function () {

            vm.selectDocumentTemplateTypeLawTerm(documentTemplateTypeLawTerm);

            expect(vm.removeDTItem).toBe(true);
            expect(vm.errorPopUp).toBe(false);
            expect(vm.activeDocumentTypeLawTerm).toBe(documentTemplateTypeLawTerm);
            expect(vm.primaryMatterType).toBe(true);

        });

        it('Should return expected document template type term', function () {

            vm.selectDocumentTemplateTypeLawTerm(documentTemplateTypeLawTerm);

            expect(vm.removeDTItem).not.toBe(false);
            expect(vm.errorPopUp).not.toBe(true);
            expect(vm.activeDocumentTypeLawTerm).not.toBe(null);
            expect(vm.primaryMatterType).not.toBe(false);

        });
    });

    describe('Verification of addToDocumentTemplate function', function () {
        it('Should return the document template type law terms', function () {
            vm.activeSubAOLTerm = documentTemplateTypeLawTerm;
            vm.documentTypeLawTerms = subareaTerms;
            vm.addToDocumentTemplate();
            expect(vm.documentTypeLawTerms).not.toBeUndefined();
        });

        it('Should return activeDocumentTypeLawTerm as null', function () {
            vm.activeSubAOLTerm = documentTemplateTypeLawTerm;
            vm.documentTypeLawTerms = subareaTerms;
            vm.addToDocumentTemplate();
            expect(vm.activeDocumentTypeLawTerm).toBe(null);
        });

    });

    describe('Verification of removeFromDocumentTemplate function', function () {
        it('Should not return document template type law terms while removing', function () {
            vm.removeDTItem = false;
            vm.removeFromDocumentTemplate();
            expect(vm.removeDTItem).not.toBe(true);
            expect(vm.primaryMatterType).not.toBe(true);
        });

        it('Should return the document template type law terms while removing', function () {
            vm.removeDTItem = true;
            vm.removeFromDocumentTemplate();
            expect(vm.removeDTItem).toBe(false);
            expect(vm.primaryMatterType).toBe(false);
        });
    });

    describe('Verification of onSelect function', function () {

        it('Should return the conflicted ensured user', function () {
            vm.removeDTItem = false;
            vm.onSelect(item, "MAQ User", "MAQ Use", "conflictcheckuser");
            expect(vm.selectedConflictCheckUser).toBe("MAQ User(MAQUser@LCADMS.onmicrosoft.com)");

        });

        it('Should return the blocked ensured user', function () {
            vm.removeDTItem = false;
            vm.onSelect(item, "MAQ User", "MAQ User", "blockuser");
            expect(vm.blockedUserName).toBe("MAQ User(MAQUser@LCADMS.onmicrosoft.com)");

        });

    });
    //-----------------------------------------------------------------------------------------
    // Methods to verify:
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
    //-------------------------------------------------------------------------------------------


    describe("Check if matter already exist (POC Spec)", function () {
        beforeEach(function () {

            vm.matterName = "abcrtawc";
            vm.clientUrl = "https://lcadms.sharepoint.com/sites/AdventureWorksCycles";

        });

        it("should return a failing Ajax Response", function () {
            var val = vm.checkValidMatterName();
            console.log(val);
            expect(val).toBe(true);
        });

        //var data;

        //beforeEach(function () {
        //    vm.matterName = "abcrtawc";
        //    vm.clientUrl = "https://lcadms.sharepoint.com/sites/AdventureWorksCycles";
        //    runs(function () {
        //        vm.checkValidMatterName(function (res) {
        //            data = res;
        //        });
        //    });

        //    waitsFor(function () { return !!data; }, 'Timed out', 1000);
        //});

        //it("test1", function () {
        //    runs(function () {
        //        expect(data).toBe(something);
        //    });
        //});
        //it("won't be detected. It's executed without waiting for the asynchronous result", function(){

        //vm.checkValidMatterName().then(function (message) {
        //    expect(message).toBeDefined();
        //}, function (error) {
        //    expect("first test received error: " + error).toFail();
        //});
        //});


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




});
