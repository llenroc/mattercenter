//Test suite
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
    var data = { "name": "nikunj" };

    var mockmatterResourceService = {
        'get': '/api/v1/matter/get',

        'getPinnedMatters': '/api/v1/matter/getpinned',

        'UnpinMatters': '/api/v1/matter/unpin',

        'PinMatters': '/api/v1/matter/pin',
        'getTaxonomyDetails': '/api/v1/taxonomy/gettaxonomy',
        'checkMatterExists': '/api/v1/matter/checkmatterexists',

        'getDefaultMatterConfigurations': '/api/v1/matter/getconfigurations',

        'getUsers': '/api/v1/user/getusers',

        'getRoles': '/api/v1/user/getroles',
        'getPermissionLevels': '/api/v1/user/getpermissionlevels',

        'checkSecurityGroupExists': '/api/v1/matter/checksecuritygroupexists',
        'getFolderHierarchy': '/api/v1/matter/getfolderhierarchy',


        'createMatter': '/api/v1/matter/create',

        'assignUserPermissions': '/api/v1/matter/assignuserpermissions',


        'assignContentType': '/api/v1/matter/assigncontenttype',


        'createLandingPage': '/api/v1/matter/createlandingpage',

        'updateMatterMetadata': '/api/v1/matter/UpdateMetadata',

        'getStampedProperties': '/api/v1/matter/getstampedproperties',

        'uploadEmail': '/api/v1/document/UploadMail',

        'uploadAttachment': '/api/v1/document/UploadAttachments',

        'uploadfiles': '/api/v1/document/UploadAttachments',

        'getHelp': '/api/v1/shared/help'
    };

    var mockapi = function (matterResource, callback) {

        var mockmatterResourceService = {
            'get': '/api/v1/matter/get',

            'getPinnedMatters': '/api/v1/matter/getpinned',

            'UnpinMatters': '/api/v1/matter/unpin',

            'PinMatters': '/api/v1/matter/pin',
            'getTaxonomyDetails': '/api/v1/taxonomy/gettaxonomy',
            'checkMatterExists': '/api/v1/matter/checkmatterexists',

            'getDefaultMatterConfigurations': '/api/v1/matter/getconfigurations',

            'getUsers': '/api/v1/user/getusers',

            'getRoles': '/api/v1/user/getroles',
            'getPermissionLevels': '/api/v1/user/getpermissionlevels',

            'checkSecurityGroupExists': '/api/v1/matter/checksecuritygroupexists',
            'getFolderHierarchy': '/api/v1/matter/getfolderhierarchy',


            'createMatter': '/api/v1/matter/create',

            'assignUserPermissions': '/api/v1/matter/assignuserpermissions',


            'assignContentType': '/api/v1/matter/assigncontenttype',


            'createLandingPage': '/api/v1/matter/createlandingpage',

            'updateMatterMetadata': '/api/v1/matter/UpdateMetadata',

            'getStampedProperties': '/api/v1/matter/getstampedproperties',

            'uploadEmail': '/api/v1/document/UploadMail',

            'uploadAttachment': '/api/v1/document/UploadAttachments',

            'uploadfiles': '/api/v1/document/UploadAttachments',

            'getHelp': '/api/v1/shared/help'
        };

        var url = "http://mattermaqdevsite.azurewebsites.net" + mockmatterResourceService[matterResource.method];
        callAPI(matterResource.success);
        function callAPI(callback) {
            
            var http = new XMLHttpRequest();
            var postdata = JSON.stringify(matterResource.data);

            http.open("POST", url, false);

            //Send the proper header information along with the request
            http.setRequestHeader("Content-type", "application/json");
            http.setRequestHeader("Accept", "application/json");
            http.setRequestHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJhdWQiOiI2MTM5NGFiYS0wOWJhLTRlMjUtYWUzMi1lMTA4MDVjNjg0MWIiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85YTY4OTE5Mi1iZmM5LTRkMDgtOTRiOS02NGIzMWJjNjA1NDAvIiwiaWF0IjoxNDY5Nzc0NjY1LCJuYmYiOjE0Njk3NzQ2NjUsImV4cCI6MTQ2OTc3ODU2NSwiYW1yIjpbInB3ZCJdLCJmYW1pbHlfbmFtZSI6IlVzZXIiLCJnaXZlbl9uYW1lIjoiTUFRIiwiaGFzZ3JvdXBzIjoidHJ1ZSIsImlwYWRkciI6IjQwLjExOC4yNDUuMTYyIiwibmFtZSI6Ik1BUSBVc2VyIiwibm9uY2UiOiJkNDBjYzhlOC0yZmRlLTRlYWUtODM5YS1iY2ZjOTYwMWY4MzkiLCJvaWQiOiIwZTNhNjkyNy0yZGE1LTRjNjQtOTU2Yy05ZWZjMjQ3N2YwZGYiLCJzdWIiOiJ0VkRTVmRHZC1TTWZ1RFNvV015TDdXR29ERmdmbDAwZF9oTHdOQTB1ODZBIiwidGlkIjoiOWE2ODkxOTItYmZjOS00ZDA4LTk0YjktNjRiMzFiYzYwNTQwIiwidW5pcXVlX25hbWUiOiJNQVFVc2VyQExDQURNUy5vbm1pY3Jvc29mdC5jb20iLCJ1cG4iOiJNQVFVc2VyQExDQURNUy5vbm1pY3Jvc29mdC5jb20iLCJ2ZXIiOiIxLjAifQ.RA_mxTix2-HzxRc3WnL5QMhcTaRFheg6_zRwdZ-g8hqx3tqkXMYxFsi9arMChwKfkMA3fhjdaloFVSBm_ElQ_mGWcXlWwxsESDiSKM4MBVrzBZTlgSUMQ-QwCMyZMz7eLwTYzwymdADM536lM0gxkqDeQUm3ngsWBsX0GbG86JlfKVFNmug1F6zwDcy-8eO9fgRltA4POdjy0HtyeaZxF7llFCc0HGWpwnz2qrvLJdZQjcTVSV5RjJtJE5CkIT5FDBJJsid8GH5PFNdduUGxBj7DIqOa_R99bpp9GfYBVxCzLEll8QmScEIb7hAy3VXOkbQZOUOuEXmvWrRvLrIaEg");
            http.send(postdata);

            if (http.status === 200) {// That's HTTP for 'ok'
                console.log(http.responseText);
                callback(JSON.parse(http.responseText));
            }

        }
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
            vm.matterName = "nikunj78956123";
            vm.clientUrl = "https://lcadms.sharepoint.com/sites/microsoft";
            vm.checkValidMatterName();
            expect(vm.checkValidMatterName).toBe(true);
        });

    });



});
