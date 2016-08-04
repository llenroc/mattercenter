﻿(function () {
    'use strict';
    var app = angular.module("matterMain");
    app.controller('MatterUsersController', ['$scope', '$state', '$stateParams', 'api', 'matterResource', '$filter', '$window', '$rootScope', '$location',
    function ($scope, $state, $stateParams, api, matterResource, $filter, $window, $rootScope, $location) {
        var cm = this;
        cm.arrAssignedUserName = [],
        cm.arrAssignedUserEmails = [],
        cm.userIDs = [];
        cm.matterProperties = undefined;
        cm.assignPermissionTeams = [];
        $rootScope.profileClass = "hide";
        cm.notificationPopUpBlock = false;
        cm.sConflictScenario = "";
        cm.isEdit = "false";
        cm.oMandatoryRoleNames = [];
        cm.popupContainerBackground = "Show";

        function getParameterByName(name) {
            "use strict";
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(decodeURIComponent($location.absUrl()));
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        cm.clientUrl = getParameterByName("clientUrl");
        cm.matterName = getParameterByName("matterName");
        cm.isEdit = getParameterByName("IsEdit");

        if (cm.clientUrl === "" && cm.matterName === "") {
            cm.matterName = "E2ETesting2";
            cm.clientUrl = "https://lcadms.sharepoint.com/sites/microsoft";
            cm.isEdit = "true";
        }

        //#region Service API Call
        //API call to get roles that are configured in the system
        function getRoles(options, callback) {
            api({
                resource: 'matterResource',
                method: 'getRoles',
                data: options,
                success: callback
            });
        }
        //API call to get permission levels that are configured in the system
        function getPermissionLevels(options, callback) {
            api({
                resource: 'matterResource',
                method: 'getPermissionLevels',
                data: options,
                success: callback
            });
        }

        //API call to users from matter stamped properties api
        function getStampedProperties(options, callback) {
            api({
                resource: 'matterResource',
                method: 'getStampedProperties',
                data: options,
                success: callback
            });
        }
        //API call to update matter permissions
        function updateMatterPermissions(optionsForUsers, callback) {
            api({
                resource: 'matterResource',
                method: 'updateMatter',
                data: optionsForUsers,
                success: callback
            });
        }
        //API call to check whether a user exists or not?
        function userexists(options, callback) {
            api({
                resource: 'matterResource',
                method: 'userexists',
                data: options,
                success: callback
            });
        }
        //#endregion

        //#region
        cm.searchUsers = function (val) {
            var searchUserRequest = {
                Client: {
                    //Need to get the matter url from query string
                    Url: cm.clientUrl
                },
                SearchObject: {
                    SearchTerm: val
                }
            };
            return matterResource.getUsers(searchUserRequest).$promise;
        }

        var optionsForRoles = new Object;
        optionsForRoles = {
            Url: configs.global.repositoryUrl
        }

        var optionsForPermissionLevels = new Object;
        optionsForPermissionLevels = {
            Url: configs.global.repositoryUrl
        }

        var optionsForStampedProperties = new Object;
        optionsForStampedProperties = {
            Client: {
                Url: cm.clientUrl
            },
            Matter: {
                // Name:'Microsoft Matter'
                Name: cm.matterName
            }
        }
        //endregion

        //#region Main function calss
        function getMatterUsers() {
            if (cm.clientUrl !== "" && cm.matterName !== "") {
                getStampedProperties(optionsForStampedProperties, function (response) {
                    cm.matterProperties = response
                    console.log(response);
                    //Get all roles from catalog site collection
                    getRoles(optionsForRoles, function (response) {
                        cm.assignRoles = response;
                        //Get all permissions from catalog site collection
                        getPermissionLevels(optionsForPermissionLevels, function (response) {
                            cm.assignPermissions = response;
                            getUsersRolesAndPermissions();
                            cm.popupContainerBackground = "hide";
                        });
                    });
                });
            }
        }

        getMatterUsers();
        cm.CheckPopUp = function (e) {
            //  e.stopPropagation();
            if (!cm.errorStatus) {
                cm.errorPopUpBlock = false;
                cm.errorBorder = "";
            }
            cm.errorStatus = false;

        }

        function getUsersRolesAndPermissions() {
            var tempMatterProp = cm.matterProperties;
            var userEmails = tempMatterProp.matterObject.assignUserEmails;
            var userNames = tempMatterProp.matterObject.assignUserNames;
            var permissions = tempMatterProp.matterObject.permissions;
            var roles = tempMatterProp.matterObject.roles;
            cm.sConflictScenario = 0 < tempMatterProp.matterObject.blockUserNames.length ? "True" : "False";
            var assigendTeams = [];
            if (userEmails && userNames && permissions && roles && userEmails.length === userNames.length && userNames.length === permissions.length && permissions.length === roles.length) {
                for (var i = 0; i < userEmails.length; i++) {
                    var assignedTeam = {};
                    assignedTeam.assignedUser = userNames[i][0] + "(" + userEmails[i][0] + ")";
                    // assignedTeam.assignedRole = roles[i];
                    angular.forEach(cm.assignRoles, function (role) {
                        if (role.name == roles[i]) {
                            assignedTeam.assignedRole = role;
                        }
                    });
                    angular.forEach(cm.assignPermissions, function (permission) {
                        if (permission.name == permissions[i]) {
                            assignedTeam.assignedPermission = permission;
                        }
                    });
                    cm.assignPermissionTeams = (cm.assignPermissionTeams.length == 1 && cm.assignPermissionTeams[0].assignedUser == "") ? [] : cm.assignPermissionTeams;
                    assignedTeam.assigneTeamRowNumber = (cm.assignPermissionTeams.length == 1 && cm.assignPermissionTeams[0].assignedUser == "") ? 1 : cm.assignPermissionTeams.length + 1;
                    cm.assignPermissionTeams.push(assignedTeam);

                }
            }

        }

        cm.removeAssignPermissionsRow = function (index) {
            var remainingRows = cm.assignPermissionTeams.length;
            if (1 < remainingRows) {
                cm.assignPermissionTeams.splice(index, 1);

            }
        };
        cm.addNewAssignPermissions = function () {
            var newItemNo = cm.assignPermissionTeams.length + 1;
            cm.assignPermissionTeams.push({ 'assigneTeamRowNumber': newItemNo, 'assignedRole': cm.assignRoles[0], 'assignedPermission': cm.assignPermissions[0] });
        };

        //getPermissionsAndRoles();

        //var arrRoles = [];
        //arrRoles = getAssignedUserRoles();

        //var arrPermissions = [];
        //arrPermissions = getAssignedUserPermissions();
        //#endregion


        cm.checkUserExists = function (userMailId, $event) {
            if ($event) {
                $event.stopPropagation();
            }
            function validateEmail(email) {
                var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(email);
            }
            function validate(email) {
                if (validateEmail(email)) {
                    var optionsForUserExsists = new Object;
                    optionsForUserExsists = {
                        Url: cm.clientUrl,
                        Name: email
                    }
                    userexists(optionsForUserExsists, function (response) {
                        if (!response.isUserExistsInSite) {
                            angular.forEach(cm.assignPermissionTeams, function (team) {
                                var userEmail = getUserName(team.assignedUser + ";", false)
                                if (userEmail[0] == email) {
                                    cm.textInputUser = team;
                                    // cm.errTextMsg = "Please enter a valid email address.";
                                    //  cm.errorBorder = "";
                                    // cm.errorStatus = true;
                                    //  cm.errorPopUpBlock = true;
                                    //   showErrorNotificationAssignTeams(cm.errTextMsg, team.assigneTeamRowNumber, "user")
                                    // cm.errorBorder = "txtUser" + team.assigneTeamRowNumber; 
                                    showNotificatoinMessages(team.assigneTeamRowNumber);
                                    return false;
                                }

                            });
                            cm.notificationPopUpBlock = true;
                        }
                        else {
                            cm.notificationPopUpBlock = false;

                        }
                    });

                }
                else {
                    angular.forEach(cm.assignPermissionTeams, function (team) {
                        var userEmail = getUserName(team.assignedUser + ";", false)
                        if (userEmail[0] == email) {
                            cm.errTextMsg = "Please enter a valid email address.";
                            cm.errorBorder = "";
                            cm.errorStatus = true;
                            cm.errorPopUpBlock = true;
                            showErrorNotificationAssignTeams(cm.errTextMsg, team.assigneTeamRowNumber, "user")
                            cm.errorBorder = "txtUser" + team.assigneTeamRowNumber;
                            return false;
                        }

                    });

                }
            }
            if (userMailId && userMailId != "") {
                var pattern = /\(([^)]+)\)/, matches = userMailId.match(pattern);
                if (matches && matches.length > 0) {
                    userMailId = matches[1];
                }
                validate(userMailId);

            }
        }




        function showErrorNotificationAssignTeams(errorMsg, teamRowNumber, type) {
            var fieldType = "";

            if (type == "user") {
                fieldType = "txtUser";
            }
            else if (type == "role") { fieldType = "roleUser" }
            else if (type == "perm") {
                fieldType = "permUser";
            }
            else if (type == "btnCreateMatter") {
                fieldType = "btnCreateMatter";
                teamRowNumber = "";
            }

            var temp = angular.element('#' + fieldType + teamRowNumber).parent().position();


            var matterErrorEle = document.getElementById("errorBlock");
            var matterErrorTrinageleBlockEle = document.getElementById("errTrinagleBlock");
            var matterErrorTrinagleBorderEle = document.getElementById("errTrinagleBroderBlock");
            var matterErrorTextEle = document.getElementById("errText");
            matterErrorEle.className = ""; matterErrorTrinageleBlockEle.className = ""; matterErrorTrinagleBorderEle.className = ""; matterErrorTextEle.className = "";
            matterErrorEle.classList.add("errorPopUp");
            matterErrorTrinageleBlockEle.classList.add("errTriangle");
            matterErrorTrinageleBlockEle.classList.add("popUpFloatLeft");
            matterErrorTrinagleBorderEle.classList.add("errTriangleBorder");
            matterErrorTrinagleBorderEle.classList.add("popUpFloatLeft");
            matterErrorTextEle.classList.add("errText");
            matterErrorTextEle.classList.add("popUpFloatRight");
            var errPopUpCAttorny = document.createElement('style'),
                errTringleBlockCAttorny = document.createElement('style'),
                errTringleBorderCAttorny = document.createElement('style'),
                errTextMatterCAttorny = document.createElement('style');
            errPopUpCAttorny.type = 'text/css';
            errTringleBlockCAttorny.type = 'text/css';
            errTringleBorderCAttorny.type = 'text/css';
            errTextMatterCAttorny.type = 'text/css';

            var width = GetWidth();
            var x = 0, y = 0;
            if (width > 734) {
                y = temp.top - 85, x = temp.left - 25;
            }
            else {
                y = temp.offsetTop, x = temp.offsetLeft;
            }
            //if (width > 734) {
            //    console.log(posEle.x);
            //    console.log(posEle.y);
            //    y = temp.offsetTop-9 , x = temp.offsetLeft +405;
            //}
            //else {
            //    y = temp.offsetTop + 57, x = temp.offsetLeft + 10;
            //}


            errPopUpCAttorny.innerHTML = ".errPopUpCAttorny{top:" + y + "px;left:" + x + "px;}";
            errTringleBlockCAttorny.innerHTML = "{min-height: 40px;top: 17px !important;left: 24px;width:100%}";
            errTringleBorderCAttorny.innerHTML = "{min-height: 40px,top: 17px !important;left: 24px;width:100%}";
            errTextMatterCAttorny.innerHTML = "{min-height:40px;top:21px !important;left: 24px;width:100%}";
            document.getElementsByTagName('head')[0].appendChild(errPopUpCAttorny);
            document.getElementsByTagName('head')[0].appendChild(errTringleBlockCAttorny);
            document.getElementsByTagName('head')[0].appendChild(errTringleBorderCAttorny);
            document.getElementsByTagName('head')[0].appendChild(errTextMatterCAttorny);


            cm.errTextMsg = errorMsg;

            cm.errorPopUpBlock = true;
            matterErrorEle.classList.add("errPopUpCAttorny");
            matterErrorTrinageleBlockEle.classList.add("errTringleBlockCAttorny");
            matterErrorTrinagleBorderEle.classList.add("errTringleBorderCAttorny");
            matterErrorTextEle.classList.add("errTextMatterCAttorny");
        }


        //#region Utilty functions

        var getUserName = function (sUserEmails, bIsName) {
            "use strict";
            var arrUserNames = [], sEmail = "", oEmailRegex = new RegExp("^[\\s]*\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*[\\s]*$");
            if (sUserEmails && null !== sUserEmails && "" !== sUserEmails) {
                arrUserNames = sUserEmails.split(";");
                for (var iIterator = 0; iIterator < arrUserNames.length - 1; iIterator++) {
                    if (arrUserNames[iIterator] && null !== arrUserNames[iIterator] && "" !== arrUserNames[iIterator]) {
                        if (-1 !== arrUserNames[iIterator].lastIndexOf("(")) {
                            sEmail = $.trim(arrUserNames[iIterator].substring(arrUserNames[iIterator].lastIndexOf("(") + 1, arrUserNames[iIterator].lastIndexOf(")")));
                            if (oEmailRegex.test(sEmail)) {
                                arrUserNames[iIterator] = bIsName ? $.trim(arrUserNames[iIterator].substring(0, arrUserNames[iIterator].lastIndexOf("("))) : sEmail;
                            }
                        }
                    }
                }
            }
            return arrUserNames;
        }
        cm.oMandatoryRoleNames = [];
        function validateTeamAssigmentRole() {
            var oAssignList = cm.assignPermissionTeams
            , iExpectedCount = 0, iActualCount = 0, iIterator = 0, iLength = cm.assignRoles.length;
            for (iIterator = 0; iIterator < iLength; iIterator++) {
                if (cm.assignRoles[iIterator].mandatory) {
                    iExpectedCount++;
                    cm.oMandatoryRoleNames.push(cm.assignRoles[iIterator].name);
                }
            }
            angular.forEach(oAssignList, function (oItem) {
                if (true == oItem.assignedRole.mandatory) {
                    iActualCount++;
                }
            });
            if (iExpectedCount <= iActualCount) {
                return true;
            }
            return false;
        }

        /* Function to validate Permission */
        function validatePermission() {
            "use strict";
            var oPermissionList = cm.assignPermissionTeams, bIsFullControl = false;
            var Edit_Matter_Mandatory_Permission_Level = "Full Control";
            angular.forEach(oPermissionList, function (oPermissionList, oPermissionListItem) {
                if (oPermissionListItem) {
                    if (Edit_Matter_Mandatory_Permission_Level === oPermissionListItem.assignedPermission.name) {
                        bIsFullControl = true;
                    }
                }
            });
            return bIsFullControl;
        }

        function getUserEmail(arrUsersEmails) {
            var sEmail = "";
            if (arrUsersEmails && 0 < arrUsersEmails.length) {
                for (var nCount = 0; nCount < arrUsersEmails.length; nCount++) {
                    if ("" !== arrUsersEmails[nCount]) {
                        sEmail = arrUsersEmails[nCount];
                    }
                }
            }
            return sEmail;
        }

        function getArrAssignedUserNamesAndEmails() {
            cm.arrAssignedUserName = [], cm.arrAssignedUserEmails = [], cm.userIDs = [];
            var count = 1;
            angular.forEach(cm.assignPermissionTeams, function (team) { //For loop
                cm.arrAssignedUserName.push(getUserName(team.assignedUser + ";", true));
                cm.arrAssignedUserEmails.push(getUserName(team.assignedUser + ";", false));
                cm.userIDs.push("txtAssign" + count++);
            });
        }

        function getAssignedUserRoles() {
            "use strict";
            var arrAssigneTeams = cm.assignPermissionTeams, nCount = 0, nlength, arrRoles = [];
            if (arrAssigneTeams) {
                nlength = arrAssigneTeams.length;
                for (nCount = 0; nCount < nlength; nCount++) {
                    if (arrAssigneTeams[nCount] && arrAssigneTeams[nCount].assignedRole) {
                        if (arrAssigneTeams[nCount].assignedRole && arrAssigneTeams[nCount].assignedRole.name) {
                            if ("" !== arrAssigneTeams[nCount].assignedRole.name) {
                                arrRoles.push(arrAssigneTeams[nCount].assignedRole.name);
                            }
                        }
                    }
                }
            }
            return arrRoles;
        }

        function getAssignedUserPermissions() {
            "use strict";
            var arrAssigneTeams = cm.assignPermissionTeams, nCount = 0, nlength, arrAssignRoles, arrPermissions = [];
            if (arrAssigneTeams) {
                nlength = arrAssigneTeams.length;
                for (nCount = 0; nCount < nlength; nCount++) {
                    if (arrAssigneTeams[nCount] && arrAssigneTeams[nCount].assignedPermission) {
                        if (arrAssigneTeams[nCount].assignedPermission && arrAssigneTeams[nCount].assignedPermission.name) {
                            if ("" !== arrAssigneTeams[nCount].assignedPermission.name) {
                                arrPermissions.push(arrAssigneTeams[nCount].assignedPermission.name);
                            }
                        }
                    }
                }
            }
            return arrPermissions;
        }
        var validateAttornyUserRolesAndPermissins = function () {
            var responsibleAttorny = 0, fullControl = 0;
            for (var iCount = 0; iCount < cm.assignPermissionTeams.length; iCount++) {

                if ("" !== cm.assignPermissionTeams[iCount].assignedUser) {

                    if (cm.assignPermissionTeams[iCount].assignedRole && "" !== cm.assignPermissionTeams[iCount].assignedRole.name) {
                        if (cm.assignPermissionTeams[iCount].assignedPermission && "" != cm.assignPermissionTeams[iCount].assignedPermission.name) {
                            if (cm.assignPermissionTeams[iCount].assignedRole.mandatory) {
                                responsibleAttorny++;
                            }
                            if (cm.assignPermissionTeams[iCount].assignedPermission.name == "Full Control") {
                                fullControl++;
                            }

                        }
                        else {
                            cm.errTextMsg = "Please provide at least one permission on this  matter. ";
                            cm.errorBorder = "";
                            cm.errorPopUpBlock = true;
                            return false;
                        }
                    }
                    else {
                        cm.errorPopUpBlock = true;
                        cm.errTextMsg = "Enter at least one role for this matter.";
                        cm.errorBorder = "";
                        return false;
                    }
                }
                else {
                    cm.errTextMsg = cm.assignPermissionTeams[iCount].assignedRole.name + " cannot be empty.";
                    cm.errorBorder = "";
                    showErrorNotificationAssignTeams(cm.errTextMsg, cm.assignPermissionTeams[iCount].assigneTeamRowNumber, "user");
                    cm.errorPopUpBlock = true;
                    return false;
                }
            }

            if (responsibleAttorny >= 1) {
                if (fullControl >= 1) {
                    return true;
                }
                else {
                    cm.errTextMsg = "Please provide at least one user who has Full Control permission on this  matter.";
                    cm.errorBorder = "permUser1";
                    showErrorNotificationAssignTeams(cm.errTextMsg, 1, "perm");
                    cm.errorPopUpBlock = true;
                    return false;
                }
            }
            else {
                cm.errTextMsg = "Enter at least one Responsible Attorney for this matter.";
                cm.errorBorder = "roleUser1";
                showErrorNotificationAssignTeams(cm.errTextMsg, 1, "role");
                cm.errorPopUpBlock = true;
                return false;
            }
        }


        cm.onSelect = function ($item, $model, $label, value, fucnValue, $event) {
            console.log(cm.typehead);
            if ($item && $item.name !== "No results found") {

                if (value == "team") {
                    $label.assignedUser = $item.name + '(' + $item.email + ')';
                    cm.typehead = false;
                    cm.notificationPopUpBlock = false;
                }
                console.log(cm.typehead);

                if (-1 == cm.oSiteUsers.indexOf($item.email)) {
                    cm.oSiteUsers.push($item.email);
                }
                cm.checkUserExists($label.assignedUser);

            }
            else {
                if (fucnValue == "on-blurr") {
                    cm.checkUserExists($label.assignedUser, $event);
                }
            }
        }

        cm.confirmUser = function (confirmUser) {
            if (confirmUser) {
                cm.notificationPopUpBlock = false;
                cm.notificationBorder = "";
            } else {
                cm.notificationPopUpBlock = false;
                cm.textInputUser.assignedUser = "";
                cm.notificationBorder = "";
            }
        }

        function showNotificatoinMessages(teamRowNumber) {
            var temp = document.getElementById('txtUser' + teamRowNumber);
            var notificationEle = document.getElementById("notificationBlock");
            var notificationTrinageleBlockEle = document.getElementById("notificatoinTrinagleBlock");
            var notificationTrinagleBorderEle = document.getElementById("notificationTrinagleBroderBlock");
            var notificationTextEle = document.getElementById("notificationText");
            notificationEle.className = ""; notificationTrinageleBlockEle.className = ""; notificationTrinagleBorderEle.className = ""; notificationTextEle.className = "";
            notificationEle.classList.add("notificationPopUp");
            notificationTrinageleBlockEle.classList.add("notificatonTriangle");
            notificationTrinageleBlockEle.classList.add("popUpFloatLeft");
            notificationTrinagleBorderEle.classList.add("notificationTriangleBorder");
            notificationTrinagleBorderEle.classList.add("popUpFloatLeft");
            notificationTextEle.classList.add("notificatonText");
            notificationTextEle.classList.add("popUpFloatRight");
            var notifcationPopUpCAttorny = document.createElement('style'),
                notifcationTringleBlockCAttorny = document.createElement('style'),
                notifcationTringleBorderCAttorny = document.createElement('style'),
                notifcationTextMatterCAttorny = document.createElement('style');
            notifcationPopUpCAttorny.type = 'text/css';
            notifcationTringleBlockCAttorny.type = 'text/css';
            notifcationTringleBorderCAttorny.type = 'text/css';
            notifcationTextMatterCAttorny.type = 'text/css';

            var width = GetWidth();
            var x = 0, y = 0;
            if (width > 734) {
                y = temp.offsetTop + 53, x = temp.offsetLeft + 70;
            }
            else {
                y = temp.offsetTop, x = temp.offsetLeft;
            }
            cm.notificationBorder = "txtUser" + teamRowNumber;

            notifcationPopUpCAttorny.innerHTML = ".notifcationPopUpCAttorny{top:" + y + "px;left:" + x + "px;}";
            notifcationTringleBlockCAttorny.innerHTML = "{min-height: 40px;top: 17px !important;left: 24px;width:100%}";
            notifcationTringleBorderCAttorny.innerHTML = "{min-height: 40px,top: 17px !important;left: 24px;width:100%}";
            notifcationTextMatterCAttorny.innerHTML = "{min-height:40px;top:21px !important;left: 24px;width:100%}";
            document.getElementsByTagName('head')[0].appendChild(notifcationPopUpCAttorny);
            document.getElementsByTagName('head')[0].appendChild(notifcationTringleBlockCAttorny);
            document.getElementsByTagName('head')[0].appendChild(notifcationTringleBorderCAttorny);
            document.getElementsByTagName('head')[0].appendChild(notifcationTextMatterCAttorny);

            notificationEle.classList.add("notifcationPopUpCAttorny");
            notificationTrinageleBlockEle.classList.add("notifcationTringleBlockCAttorny");
            notificationTrinagleBorderEle.classList.add("notifcationTringleBorderCAttorny");
            notificationTextEle.classList.add("notifcationTextCAttorny");
        }

        function GetWidth() {
            "use strict";
            var x = 0;
            if (self.innerHeight) {
                x = self.innerWidth;
            } else if (document.documentElement && document.documentElement.clientHeight) {
                x = document.documentElement.clientWidth;
            } else if (document.body) {
                x = document.body.clientWidth;
            }
            return x;
        }


        function validateUsers() {
            var keepGoing = true;
            var blockedUserEmail = cm.matterProperties.matterObject.blockUserNames[0];
           
            angular.forEach(cm.assignPermissionTeams, function (team) {
                if (keepGoing) {
                    if (team.assignedUser && team.assignedUser != "") {//For loop                                             

                        if (blockedUserEmail && blockedUserEmail != "") {
                         var teamUserEmail= getUserName(team.assignedUser.trim() + ";", false)
                         if (teamUserEmail[0] == blockedUserEmail) {
                                cm.errTextMsg = "Please enter individual who is not conflicted.";
                                cm.errorBorder = "";
                                cm.errorPopUpBlock = true;
                                showErrorNotificationAssignTeams(cm.errTextMsg, team.assigneTeamRowNumber, "user")
                                cm.errorBorder = "txtUser" + team.assigneTeamRowNumber; keepGoing = false;
                                return false;
                            }
                        }
                    }
                    else {
                        showErrorNotificationAssignTeams(team.assignedRole.name + " cannot be empty", team.assigneTeamRowNumber, "user")
                        cm.errorBorder = "txtUser" + team.assigneTeamRowNumber;
                        keepGoing = false;
                        return false;
                    }
                }
            });

            if (keepGoing) {
                return true;
            }
        }




        cm.UpdateMatter = function ($event) {
            cm.popupContainerBackground = "Show";
            if ($event) {
                $event.stopPropagation();
            }
            var arrUserNames = [],
                arrUserEmails = [],
                arrTeamMembers = [],
                roleInformation = {},
                arrReadOnlyUsers = [],
                sResponsibleAttorney = [],
                sResponsibleAttorneyEmail = [];
            var arrBlockUserNames = cm.matterProperties.matterObject.blockUserNames ? cm.matterProperties.matterObject.blockUserNames : ""

            var attornyCheck = validateAttornyUserRolesAndPermissins($event);
            var validUsersCheck=validateUsers();       
            if (attornyCheck && validUsersCheck) {
                angular.forEach(cm.assignPermissionTeams, function (item) {
                    if ("" !== item.assignedRole && "" !== item.assignedPermission) {
                        if (roleInformation.hasOwnProperty(item.assignedRole.name)) {
                            roleInformation[item.assignedRole.name] = roleInformation[item.assignedRole.name] + ";" + item.assignedUser;
                        }
                        else {
                            roleInformation[item.assignedRole.name] = item.assignedUser;
                        }
                    }
                });

                angular.forEach(cm.assignPermissionTeams, function (item) {
                    arrUserNames.push(getUserName(item.assignedUser.trim() + ";", true));
                    arrUserEmails.push(getUserName(item.assignedUser.trim() + ";", false));
                    arrTeamMembers.push(getUserName(item.assignedUser.trim() + ";", true).join(";"));
                    var User_Upload_Permissions = "Read";
                    angular.forEach(cm.assignPermissionTeams, function (item) {
                        if (item.assignedPermission.name.toLowerCase() === User_Upload_Permissions.toLowerCase()) {
                            arrReadOnlyUsers.push(getUserName(item.assignedRole.name.trim() + ";", false).join(";"), ";");
                        }
                    });

                });

                validateTeamAssigmentRole();
                getArrAssignedUserNamesAndEmails();
                var arrRoles = getAssignedUserRoles();
                var arrPermissions = getAssignedUserPermissions();
                angular.forEach(cm.assignPermissionTeams, function (item) {
                    if (1 <= cm.assignPermissionTeams.length) {
                        if ("" !== item.assignedRole && "" !== item.assignedPermission) {
                            if (-1 !== cm.oMandatoryRoleNames.indexOf(item.assignedRole.name)) {
                                sResponsibleAttorney.push(getUserName(item.assignedUser + ";", true).join(";"));
                                sResponsibleAttorneyEmail.push(getUserName(item.assignedUser + ";", false).join(";"));
                            }
                        }
                    }
                });

                var updatedMatterUsers = {
                    Client: {
                        Url: cm.clientUrl,
                        Id: "",
                        Name: ""
                    },
                    Matter: {
                        Name: cm.matterName,
                        BlockUserNames: arrBlockUserNames,
                        AssignUserNames: arrUserNames,
                        AssignUserEmails: arrUserEmails,
                        Permissions: arrPermissions,
                        Roles: arrRoles,
                        Conflict: {
                            Identified: cm.sConflictScenario
                        },
                        FolderNames: [],
                        DefaultContentType: "",
                        ContentTypes: [],
                        Description: "",
                        Id: "",
                        MatterGuid: cm.matterProperties.matterObject.matterGuid

                    },
                    MatterDetails: {
                        PracticeGroup: "",
                        AreaOfLaw: "",
                        SubareaOfLaw: "",
                        ResponsibleAttorney: sResponsibleAttorney.join(";").replace(/;;/g, ";"),
                        ResponsibleAttorneyEmail: sResponsibleAttorneyEmail.join(";").replace(/;;/g, ";"),
                        UploadBlockedUsers: arrReadOnlyUsers,
                        TeamMembers: arrTeamMembers.join(";"),
                        RoleInformation: JSON.stringify(roleInformation)
                    },
                    EditMode: cm.isEdit,
                    UserIds: cm.userIDs,
                    SerializeMatter: "",
                    Status: ""
                }

                updateMatterPermissions(updatedMatterUsers, function (response) {
                    console.log(response);


                    cm.popupContainerBackground = "hide";
                    //                           cm.errTextMsg = "Error in updating  matter: Incorrect inputs.";
                    //                           showErrorNotificationAssignTeams(cm.errTextMsg, "", "btnCreateMatter");
                    //                           cm.errorBorder = "";
                    //                           cm.errorPopUpBlock = true;
                    //                           cm.popupContainerBackground = "hide";
                });

            }
            else {
                cm.popupContainerBackground = "hide";
            }
        }

        //#endregion
    }]);


})();