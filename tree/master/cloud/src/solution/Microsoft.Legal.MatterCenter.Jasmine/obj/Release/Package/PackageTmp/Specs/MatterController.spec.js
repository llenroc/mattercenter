/// <disable>JS2074, JS3058</disable>
//// ***********************************************************************
//// Author           : MAQ USER
//// Created          : 31-08-2016
////
//// ***********************************************************************
//// <copyright file="MatterController.spec.js" company="MAQSoftware">
////  Copyright (c) . All rights reserved.
//// </copyright>
//// <summary>Test suite for Matters Controller</summary>
//// ***********************************************************************
describe("Matters Controller test suite", function () {
    "use strict";
   
    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("matterResource", ["$resource", "auth", mockMatterResource]);
    }));

    beforeEach(module("matterMain"));
    beforeEach(module("matterMain", function ($provide) {
        $provide.factory("api", ["matterResource", "documentResource", "documentDashBoardResource", "matterDashBoardResource", "homeResource", mockapi]);
    }));

    beforeEach(module("ui.router"));
    beforeEach(module("ui.bootstrap"));

    beforeEach(inject(function ($controller, $rootScope) {
        rootScope = $rootScope.$new();
        vm = $controller("mattersController as vm", { $scope: $scope, $state: $state, $stateParams: $stateParams, matterResource: mockMatterResource, api: mockapi, $rootScope: rootScope, $http: $http, $location: $location, $q: $q, $animate: $animate });
    }));


    describe("Verification of closeNotificationDialog function", function () {
        it("It should close the notification dialog box", function () {
            vm.closeNotificationDialog();
            expect(vm.IsDupliacteDocument).toBe(false);
            expect(vm.IsNonIdenticalContent).toBe(false);
            expect(vm.showLoading).toBe(false);
        });
    });

    describe("Verification of attachmentTokenCallbackEmailClient function", function () {
        it("It should attach the token along with EmailClient-Need to call createMailPopup", function () {
            var asyncResult = { status: "succeeded", value: "testtoken" };
            vm.createMailPopup = function () { return 1; };
            vm.attachmentTokenCallbackEmailClient(asyncResult, obj);
            expect(vm.attachmentToken).toBe("testtoken");
            expect(vm.mailUpLoadSuccess).toBe(false);
            expect(vm.mailUploadedFolder).toBe(null);
            expect(vm.loadingAttachments).toBe(false);
            expect(vm.mailUploadedFile).toBe(null);
        });
    });

    describe("Verification of getIconSource function", function () {
        it("It should return the source of icon", function () {
            var source = vm.getIconSource("sourceicon");
            expect(source).toBe(oTestConfiguration.sSourceIconURL);
        });
    });

    describe("Verification of closealldrops function", function () {
        it("It should close all the dropdowns", function () {
            vm.closealldrops();
            expect(vm.mattersdrop).toBe(false);
            expect(vm.mattersdropinner).toBe(true);
            expect(vm.matterheader).toBe(true);
            expect(vm.matterdateheader).toBe(true);
        });
    });

    describe("Verification of checkEmptyorWhitespace function", function () {
        it("It should check empty or whitespace in the input", function () {
            var data = vm.checkEmptyorWhitespace("test");
            expect(data).toBe("test");
        });
    });

    describe("Verification of hideBreadCrumb function", function () {
        it("It should hide the bread crumb", function () {
            vm.hideBreadCrumb();
            expect(rootScope.breadcrumb).toBe(true);
            expect(rootScope.foldercontent).toBe(false);
        });
    });

    describe("Verification of disabled function", function () {
        it("It should set the status based on current date.", function () {
            var oDate = new Date();
            var status = vm.disabled(oDate, "day");
            expect(status).toBe(true);
        });
    });

    describe("Verification of showSortExp function", function () {
        it("It should sort the elements in ascending order", function () {
            vm.sortexp = "test";
            vm.sortby = "asc";
            $scope.$apply = function () { };
            vm.showSortExp();
            expect(angular.element()).toBeDefined();
        });
    });

    describe("Verification of showmatterdrop function", function () {
        it("It should show matter dropdown if matter is pinned", function () {
            vm.mattersdropinner = true;
            vm.showmatterdrop(event);
            expect(vm.mattersdrop).toBe(true);
            expect(vm.mattersdropinner).toBe(false);
        });

        it("It should hide matter dropdown if matter is not pinned", function () {
            vm.mattersdropinner = false;
            vm.showmatterdrop(event);
            expect(vm.mattersdrop).toBe(false);
            expect(vm.mattersdropinner).toBe(true);
        });
    });

    describe("Verification of overwriteConfiguration function", function () {
        it("It should return overwrite configuration for the file", function () {
            var bAppendEnabled = vm.overwriteConfiguration(oTestConfiguration.sOverwriteConfigFileName);
            expect(bAppendEnabled).toBe(true);
        });
    });

    describe("Verification of contentCheckNotification function", function () {
        it("It should set upload file configurations to perform content check", function () {
            var file = { contentCheck: "", saveLatestVersion: "", cancel: "", append: true };
            vm.contentCheckNotification(file, true);
            expect(file.contentCheck).toBe("contentCheck");
            expect(file.saveLatestVersion).toBe("False");
            expect(file.cancel).toBe("False");
            expect(file.append).toBe(false);
        });
    });

    describe("Verification of abortContentCheck function", function () {
        it("It should set upload file configuration to abort content check", function () {
            vm.file = {};
            vm.file.value = "This";
            vm.abortContentCheck(vm.file, false);
            expect(vm.file.contentCheck).toBe(null);
            expect(vm.file.saveLatestVersion).toBe("True");
            expect(vm.file.value).toBe("This<br/><div>Content check has been aborted.</div>");
            expect(vm.file.cancel).toBe("True");
        });
    });

    describe("Verification of closeSuccessBanner function", function () {
        it("It should close the success banner", function () {
            vm.closeSuccessBanner();
            expect(vm.oUploadGlobal.successBanner).toBe(false);
        });
    });
    /*
      //----------------------------------------------------------------------------------
      describe("Verification of handleOutlookDrop function", function () {
        it("This should handle all the files that has been dragged from the outlook", function () {
           vm.handleOutlookDrop("test", "test");
           expect(vm.targetDrop).toBe("test");
           expect(vm.sourceFile).toBe("test");
           expect(vm.isLoadingFromDesktopStarted).toBe(true);
           vm.oUploadGlobal=
           {
               successBanner: null

           };
           expect(vm.oUploadGlobal.successBanner).toBe(false);
       });
   });

     describe("Verification of handleDesktopDrop function", function () {
         it("This should handle all  the files that has been dragged from the user desktop", function () {
           vm.handleDesktopDrop("test", "test","test");    
           expect(vm.isLoadingFromDesktopStarted).toBe(true);
           vm.oUploadGlobal =
           {
               successBanner: null

           };
           expect(vm.oUploadGlobal.successBanner).toBe(true);
      });
   });

   describe("Verification of overWriteDocument function", function () {
       it("This should overwrite document", function () {
           vm.overWriteDocument("overwrite");
           expect(vm.showLoading).toBe(true);
           expect(vm.IsDupliacteDocument).toBe(false);
           expect(vm.IsNonIdenticalContent).toBe(false);
       });
       it("This should perform content check", function () {
           vm.overWriteDocument("contentCheck");
           expect(vm.showLoading).toBe(true);
       });

       it("It should append the contents", function () {
           vm.overWriteDocument("append");
           expect(vm.showLoading).toBe(true);
       });
   });

   describe("Verification of closeNotificationDialog function", function () {
       it("This should close the notification dialog", function () {
           vm.closeNotificationDialog();
           expect(vm.IsDupliacteDocument).toBe(false);
           expect(vm.IsNonIdenticalContent).toBe(false);
           expect(vm.showLoading).toBe(false);  
       });
   });

   describe("Verification of Openuploadmodal function", function () {
       it("This should open upload modal", function () {
           vm.Openuploadmodal("test","test","test");
           expect(vm.isLoadingFromDesktopStarted).toBe(false);
           vm.oUploadGlobal=
           {
               successBanner: null
           };
           expect(vm.oUploadGlobal.successBanner).toBe(false);
       });

       it("This should get folder hierarchy ", function () {
           vm.getFolderHierarchy("test", "test", "test");
         
           //write for getFolderHierarcy
       });
   });

   describe("Verification of attachmentTokenCallbackEmailClientfunction", function () {
       it("This should get attachment token call back from email client", function () {
           vm.attachmentTokenCallbackEmailClient("test","test");
           vm.asyncResult = 
           {
               "status" : "succeeded",
               "value" : null
           };
           expect(vm.attachmentToken).toBe("succeeded");
           vm.createMailPopup();
           expect(vm.mailUpLoadSuccess).toBe(false);
           expect(vm.mailUpLoadSuccess).toBeNull();
           expect(vm.mailUploadedFolder).toBeNull();
           expect(vm.loadingAttachments).toBe(false);
       });
   });

   describe("Verification of initOutlook function", function () {
       it("This should initialise outlook", function () {
           vm.initOutlook();      
           expect(vm.IsDupliacteDocument).toBe(false);         
           expect(vm.loadingAttachments).toBe(true);          
       });
   });

   describe("Verification of searchMatter function ", function () {
       it("This should filter the grid when clicked on search button ", function () {
           vm.searchMatter();
           expect(vm.pagenumber).toBe(1);
       });
   });


   describe("Verification of SetMatters function", function () {
       it("This should set the mattername in dropdown ", function () {
           vm.SetMatters("test","test");
           expect(vm.mattername).toBe("test");
           vm.GetMatters("test");
           expect(vm.matterid).toBe("test");
       });
   });


   describe("Verification of FilterModifiedDate function", function () {
       it("This should filter data based on modified date", function () {
           vm.FilterModifiedDate("test");
           expect(vm.lazyloader).toBe(false);
           expect(vm.divuigrid).toBe(false);
       });

       it("This should filter data based on modified date", function () {
           vm.FilterModifiedDate("Modified Date");
           expect(vm.moddatefilter).toBe(true);
       });

       it("This should filter data based on open date", function () {
           vm.FilterModifiedDate("Open Date");
           expect(vm.opendatefilter).toBe(true);
       });
   });

   //describe("Verification of clearFilters function", function () {
      // it("This should clear all the filters", function () {
        //   vm.clearFilters("Test");
        //   expect(vm.documentheader).toBe(true);
       //    expect(vm.documentdateheader).toBe(true);
       //    expect(vm.lazyloader).toBe(false);
        //   expect(vm.nodata).toBe(false);
         //  expect(vm.pagenumber).toBe(1);
      // });

     //  it("This should clear filters based on attorney search term", function () {
       //    vm.clearFilters("Test");
       //    expect(vm.documentfilter).toBe(false);
      // });

     //  it("This should clear filters based on area search term and subareaof law", function () {
      //     vm.clearFilters("Test");
       //    expect(vm.clientfilter).toBe(false);
      // });

      // it("This should clear filters based on search term and name", function () {
       //    vm.clearFilters("Test");
         //  expect(vm.checkoutfilter).toBe(false);
      // });

      // it("This should clear filters based on client search term", function () {
        ///   vm.clearFilters("Test");
       //    expect(vm.authorfilter).toBe(false);
     //  });

      // it("This should clear filters based on modified from date and modified to date", function () {
       //    vm.clearFilters("Test");
       ///    expect(vm.moddatefilter).toBe(false);
     //  });

      // it("This should clear filters based on open from date and open to date", function () {
           vm.clearFilters("Test");
     //      expect(vm.createddatefilter).toBe(false);
    //   });
//
   //});

    describe("Verification of mattersearch function ", function () {
        it("This method gives region for searching matter by property and searchterm ", function () {
            vm.mattersearch("test", "test","test");
            expect(vm.lazyloader).toBe(false);  
            expect(vm.filternodata).toBe(false);
        });
    });

    describe("Verification of GetMatters function ", function () {
        it("This should get all matters when dropdown changes and id is 1 ", function () {
            vm.GetMatters(1);
            expect(vm.selected).toBe("");
            expect(vm.searchTerm).toBe("");
            expect(vm.searchClientTerm).toBe("");
            expect(vm.startdate).toBe("");
            expect(vm.enddate).toBe("");
            expect(vm.sortexp).toBe("");
            expect(vm.sortby).toBe("");
            expect(vm.lazyloader).toBe(false);
            expect(vm.divuigrid).toBe(false);     
        });

        it("This should get all matters when dropdown changes and id is 2", function () {
            vm.GetMatters(2);

            //check what needs to be written

        });

        it("This should get all matters when dropdown changes and id is 3 ", function () {
            vm.GetMatters(3);

            //check what needs to be written

        });
    });

    describe("Verification of openMatterHeader function ", function () {
        it("This should display and set the position of filter name wise ", function () {
            //check what needs to be done
            vm.openMatterHeader($event, "test");
            expect(vm.filternodata).toBe(false);       
        });
    });
    */
});


