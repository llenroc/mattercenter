﻿// ***********************************************************************
// Assembly         : Microsoft.Legal.MatterCenter.ProviderService
// Author           : v-lapedd
// Created          : 04-09-2016
//
// ***********************************************************************
// <copyright file="DocumentController.cs" company="Microsoft">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>This file defines service for Taxonomy</summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Legal.MatterCenter.Models;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;

#region Matter Namespaces
using Microsoft.Legal.MatterCenter.Utility;
using Microsoft.Legal.MatterCenter.Repository;
using Microsoft.Legal.MatterCenter.Web.Common;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
#endregion


namespace Microsoft.Legal.MatterCenter.Web
{
    /// <summary>
    /// Document Controller class deals with finding document, pinning document, unpinning the document etc.
    /// </summary>
    [Authorize]
    [Route("api/v1/document")]
    public class DocumentController : Controller
    {
        private ErrorSettings errorSettings;
        private ISPOAuthorization spoAuthorization;
        private IMatterCenterServiceFunctions matterCenterServiceFunctions;
        private DocumentSettings documentSettings;
        private IDocumentRepository documentRepositoy;
        private ICustomLogger customLogger;
        private LogTables logTables;
        private IDocumentProvision documentProvision;
        private GeneralSettings generalSettings;
        /// <summary>
        /// Constructor where all the required dependencies are injected
        /// </summary>
        /// <param name="errorSettings"></param>
        /// <param name="documentSettings"></param>
        /// <param name="spoAuthorization"></param>
        /// <param name="matterCenterServiceFunctions"></param>
        /// <param name="documentRepositoy"></param>
        public DocumentController(IOptionsMonitor<ErrorSettings> errorSettings,
            IOptionsMonitor<DocumentSettings> documentSettings,
            ISPOAuthorization spoAuthorization,
            IMatterCenterServiceFunctions matterCenterServiceFunctions,
            IDocumentRepository documentRepositoy,
            ICustomLogger customLogger, IOptionsMonitor<LogTables> logTables, IDocumentProvision documentProvision,
            IOptionsMonitor<GeneralSettings> generalSettings

            )
        {
            this.errorSettings = errorSettings.CurrentValue;
            this.documentSettings = documentSettings.CurrentValue;
            this.spoAuthorization = spoAuthorization;
            this.matterCenterServiceFunctions = matterCenterServiceFunctions;
            this.documentRepositoy = documentRepositoy;
            this.customLogger = customLogger;
            this.logTables = logTables.CurrentValue;
            this.documentProvision = documentProvision;
            this.generalSettings = generalSettings.CurrentValue;
        }

        /// <summary>
        /// Get all counts for all documentCounts, my documentCounts and pinned documentCounts
        /// </summary>
        /// <param name="searchRequestVM">The search request object that has been find by the user to get results back for search criteria</param>
        /// <returns>IActionResult with proper http status code</returns>
        [HttpPost("getdocumentcounts")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IActionResult> GetDocumentCounts([FromBody]SearchRequestVM searchRequestVM)
        {
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                GenericResponseVM genericResponse = null;
                #region Error Checking    
                //Input validation
                if (searchRequestVM == null && searchRequestVM.Client == null && searchRequestVM.SearchObject == null)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion                
                //For a given search request entered by the user, this api will get all documents that has been 
                //uploaded by him, all documents that are assigned to him and all the documents which are pinned by him
                int allDocumentCounts = await documentProvision.GetAllCounts(searchRequestVM);
                int myDocumentCounts = await documentProvision.GetMyCounts(searchRequestVM);
                int pinnedDocumentCounts = await documentProvision.GetPinnedCounts(searchRequestVM.Client);
                //The object count information that will be sent to the user
                var documentCounts = new
                {
                    AllDocumentCounts = allDocumentCounts,
                    MyDocumentCounts = myDocumentCounts,
                    PinnedDocumentCounts = pinnedDocumentCounts,
                };
                //If the input validation is failed, send GenericResponseVM which contains the error information
                return matterCenterServiceFunctions.ServiceResponse(documentCounts, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        /// <summary>
        /// Gets the documents based on search criteria.
        /// </summary>
        /// <param name="searchRequestVM">The search request object that has been find by the user to get results back for search criteria</param>
        /// <returns>IActionResult with proper http status code</returns>
        [HttpPost("getdocuments")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromBody]SearchRequestVM searchRequestVM)
        {
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                #region Error Checking                
                GenericResponseVM genericResponse = null;
                //Input validation
                if (searchRequestVM == null && searchRequestVM.Client == null && searchRequestVM.SearchObject == null)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    //If the input validation is failed, send GenericResponseVM which contains the error information
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion 
                var searchResultsVM = await documentProvision.GetDocumentsAsync(searchRequestVM);
                return matterCenterServiceFunctions.ServiceResponse(searchResultsVM.DocumentDataList, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        /// <summary>
        /// Get all the documents which are pinned by the logged in user
        /// </summary>
        /// <param name="client">The SPO client url from which to retrieve all the documents which are pinnned by the requested user</param>
        /// <returns>IActionResult with proper http status code</returns>
        [HttpPost("getpinneddocuments")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IActionResult> GetPin([FromBody]Client client)
        {
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                #region Error Checking    
                //Input validation            
                GenericResponseVM genericResponse = null;                                
                if (client == null)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    //If the input validation is failed, send GenericResponseVM which contains the error information
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion
                //Get the documents which are pinned by the user
                var pinResponseVM = await documentRepositoy.GetPinnedRecordsAsync(client);  
                //Return the response with proper http status code              
                return matterCenterServiceFunctions.ServiceResponse(pinResponseVM.DocumentDataList, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        /// <summary>
        /// This api will store the metadata of the document in a sharepoint list as a JSON object which is getting pinned
        /// </summary>
        /// <param name="pinRequestDocumentVM"></param>
        /// <returns></returns>
        [HttpPost("pindocument")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IActionResult> Pin([FromBody]PinRequestDocumentVM pinRequestDocumentVM)
        {
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                #region Error Checking       
                //Input validation         
                GenericResponseVM genericResponse = null;
                if (pinRequestDocumentVM == null && pinRequestDocumentVM.Client == null && pinRequestDocumentVM.DocumentData == null)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    //If the input validation is failed, send GenericResponseVM which contains the error information
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion
                var isDocumentPinned = await documentRepositoy.PinRecordAsync<PinRequestDocumentVM>(pinRequestDocumentVM);
                var documentPinned = new
                {
                    IsDocumentPinned = isDocumentPinned
                };
                //Return the response with proper http status code and proper response object
                return matterCenterServiceFunctions.ServiceResponse(documentPinned, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        /// <summary>
        /// This api will unpin the document which is already pinned and the unpinned document will be removed from the sharepoint 
        /// list object
        /// </summary>
        /// <param name="pinRequestMatterVM"></param>
        /// <returns></returns>
        [HttpPost("unpindocument")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IActionResult> UnPin([FromBody]PinRequestDocumentVM pinRequestDocumentVM)
        {
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                #region Error Checking                
                GenericResponseVM genericResponse = null;
                if (pinRequestDocumentVM == null && pinRequestDocumentVM.Client == null && pinRequestDocumentVM.DocumentData == null)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    //If the input validation is failed, send GenericResponseVM which contains the error information
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion
                var isDocumentUnPinned = await documentRepositoy.UnPinRecordAsync<PinRequestDocumentVM>(pinRequestDocumentVM);
                var documentUnPinned = new
                {
                    IsDocumentUnPinned = isDocumentUnPinned
                };
                //Return the response with proper http status code and proper response object
                return matterCenterServiceFunctions.ServiceResponse(documentUnPinned, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        /// <summary>
        /// Returns document and list GUID
        /// </summary>        
        /// <param name="client">Client object containing list data</param>        
        /// <returns>Document and list GUID</returns>
        [HttpPost("getassets")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IActionResult> GetDocumentAssets(Client client)
        {
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                #region Error Checking                
                GenericResponseVM genericResponse = null;
                if (client == null)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    //If the input validation is failed, send GenericResponseVM which contains the error information
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion
                var documentAsset = await documentRepositoy.GetDocumentAndClientGUIDAsync(client);
                //Return the response with proper http status code and proper response object
                return matterCenterServiceFunctions.ServiceResponse(documentAsset, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        /// <summary>
        /// Uploads attachment which are there in the current mail item to SharePoint library.
        /// </summary>
        /// <param name="attachmentRequestVM">This object contains information </param>
        /// <returns></returns>
        [HttpPost("uploadattachments")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IActionResult UploadAttachments([FromBody] AttachmentRequestVM attachmentRequestVM)
        {            
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                var client = attachmentRequestVM.Client;
                var serviceRequest = attachmentRequestVM.ServiceRequest;
                GenericResponseVM genericResponse = null;
                #region Error Checking                
                ErrorResponse errorResponse = null;
                if (client == null && serviceRequest==null)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    //If the input validation is failed, send GenericResponseVM which contains the error information
                    return matterCenterServiceFunctions.ServiceResponse(errorResponse, (int)HttpStatusCode.OK);
                }
                #endregion
                if (serviceRequest.FolderPath.Count != serviceRequest.Attachments.Count)
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = "Folder path count and attachment count are not same",
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    //If the input validation is failed, send GenericResponseVM which contains the error information
                    return matterCenterServiceFunctions.ServiceResponse(errorResponse, (int)HttpStatusCode.OK);
                }
                //Upload attachments to the sharepoint document library the user has choosen
                genericResponse = documentProvision.UploadAttachments(attachmentRequestVM);
                //If there is any error in uploading the attachment, send that error information to the UI
                if(genericResponse!=null && genericResponse.IsError==true)
                {
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                //
                genericResponse = new GenericResponseVM()
                {
                    Code = HttpStatusCode.OK.ToString(),
                    Value = "Attachment upload success"
                };
                //Return the response with proper http status code and proper response object
                return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }



        /// <summary>
        /// Uploads attachments from the user desktop to sharepoint library
        /// </summary>
        /// <param name="attachmentRequestVM"></param>
        /// <returns></returns>
        [HttpPost("uploadfiles")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IActionResult UploadFiles()
        {
            try
            {
                IFormFileCollection fileCollection = Request.Form.Files;
                Regex regEx = new Regex("[*?|\\\t/:\"\"'<>#{}%~&]");
                string clientUrl = Request.Form["clientUrl"];
                string folderUrl = Request.Form["folderUrl"];
                string folderName = folderUrl.Substring(folderUrl.LastIndexOf(ServiceConstants.FORWARD_SLASH, StringComparison.OrdinalIgnoreCase) + 1);
                string documentLibraryName = Request.Form["documentLibraryName"];
                bool isDeployedOnAzure = Convert.ToBoolean(generalSettings.IsTenantDeployment, CultureInfo.InvariantCulture);
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                string originalName = string.Empty;
                bool allowContentCheck = Convert.ToBoolean(Request.Form["AllowContentCheck"], CultureInfo.InvariantCulture);
                Int16 isOverwrite = 3;     
                //Input validation           
                #region Error Checking                
                GenericResponseVM genericResponse = null;
                IList<object> listResponse = new List<object>();
                bool continueUpload = true;
                if (isDeployedOnAzure == false && string.IsNullOrWhiteSpace(clientUrl) && string.IsNullOrWhiteSpace(folderUrl))
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion
                //Get all the files which are uploaded by the user
                for (int fileCounter = 0; fileCounter < fileCollection.Count; fileCounter++)
                {
                    IFormFile uploadedFile = fileCollection[fileCounter];
                    if (!Int16.TryParse(Request.Form["Overwrite" + fileCounter], out isOverwrite))
                    {
                        isOverwrite = 3;
                    }
                    continueUpload = true;
                    ContentDispositionHeaderValue fileMetadata = ContentDispositionHeaderValue.Parse(uploadedFile.ContentDisposition);
                    string fileName = originalName = fileMetadata.FileName.Trim('"');
                    fileName = System.IO.Path.GetFileName(fileName);
                    ContentCheckDetails contentCheckDetails = new ContentCheckDetails(fileName, uploadedFile.Length);
                    string fileExtension = System.IO.Path.GetExtension(fileName).Trim();
                    if (-1 < fileName.IndexOf('\\'))
                    {
                        fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                    }
                    else if (-1 < fileName.IndexOf('/'))
                    {
                        fileName = fileName.Substring(fileName.LastIndexOf('/') + 1);
                    }
                    if (null != uploadedFile.OpenReadStream() && 0 == uploadedFile.OpenReadStream().Length)
                    {
                        listResponse.Add(new GenericResponseVM() { Code = fileName, Value = errorSettings.ErrorEmptyFile, IsError = true });
                    }
                    else if (regEx.IsMatch(fileName))
                    {
                        listResponse.Add(new GenericResponseVM() { Code = fileName, Value = errorSettings.ErrorInvalidCharacter, IsError = true });
                    }
                    else
                    {
                        string folder = folderUrl.Substring(folderUrl.LastIndexOf(ServiceConstants.FORWARD_SLASH, StringComparison.OrdinalIgnoreCase) + 1);
                        //If User presses "Perform content check" option in overwrite Popup
                        if (2 == isOverwrite)   
                        {                            
                            genericResponse = documentProvision.PerformContentCheck(clientUrl, folderUrl, uploadedFile, fileName);
                        }
                        //If user presses "Cancel upload" option in overwrite popup or file is being uploaded for the first time
                        else if (3 == isOverwrite)  
                        {
                            genericResponse = documentProvision.CheckDuplicateDocument(clientUrl, folderUrl, documentLibraryName, fileName, contentCheckDetails, allowContentCheck);
                        }
                        //If User presses "Append date to file name and save" option in overwrite Popup
                        else if (1 == isOverwrite)  
                        {
                            string fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                            string timeStampSuffix = DateTime.Now.ToString(documentSettings.TimeStampFormat, CultureInfo.InvariantCulture).Replace(":", "_");
                            fileName = fileNameWithoutExt + "_" + timeStampSuffix + fileExtension;
                        }
                        if(genericResponse==null)
                        {
                            genericResponse = documentProvision.UploadFiles(uploadedFile, fileExtension, originalName, folderUrl, fileName,
                                clientUrl, folder, documentLibraryName);
                        }
                        if (genericResponse == null)
                        {
                            string documentIconUrl = string.Empty;
                            fileExtension = fileExtension.Replace(".", "");
                            if (fileExtension.ToLower() != "pdf")
                            {
                                documentIconUrl = $"{generalSettings.SiteURL}/_layouts/15/images/ic{fileExtension}.gif";
                            }
                            else
                            {
                                documentIconUrl = $"{generalSettings.SiteURL}/_layouts/15/images/ic{fileExtension}.png";
                            }
                            //Create a json object with file upload success
                            var successFile = new
                            {
                                IsError = false,
                                Code = HttpStatusCode.OK.ToString(),
                                Value = UploadEnums.UploadSuccess.ToString(),
                                FileName = fileName,
                                DropFolder = folderName,
                                DocumentIconUrl = documentIconUrl
                            };
                            listResponse.Add(successFile);
                        }
                        else
                        {
                            //Create a json object with file upload failure
                            var errorFile = new
                            {
                                IsError = true,
                                Code = genericResponse.Code.ToString(),
                                Value = genericResponse.Value.ToString(),
                                FileName = fileName,
                                DropFolder = folderName
                            };
                            listResponse.Add(errorFile);                           
                        }                   
                    }
                }
                //Return the response with proper http status code and proper response object     
                return matterCenterServiceFunctions.ServiceResponse(listResponse, (int)HttpStatusCode.OK);
                
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        /// <summary>
        /// Uploads user selected email from outlook to SharePoint library with all the attachments
        /// </summary>
        /// <param name="attachmentRequestVM"></param>
        /// <returns></returns>
        [HttpPost("uploadmail")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IActionResult UploadMail([FromBody] AttachmentRequestVM attachmentRequestVM)
        {
            try
            {
                //Get the authorization token from the Request header, which is used by sharepoint to authorize the user
                spoAuthorization.AccessToken = HttpContext.Request.Headers["Authorization"];
                var client = attachmentRequestVM.Client;
                var serviceRequest = attachmentRequestVM.ServiceRequest;
                GenericResponseVM genericResponse = null;

                #region Error Checking   
                //Input validation             
                ErrorResponse errorResponse = null;
                if (client == null && serviceRequest==null && string.IsNullOrWhiteSpace(serviceRequest.MailId))
                {
                    genericResponse = new GenericResponseVM()
                    {
                        Value = errorSettings.MessageNoInputs,
                        Code = HttpStatusCode.BadRequest.ToString(),
                        IsError = true
                    };
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                #endregion
                //Upload email to the share point library
                genericResponse = documentProvision.UploadEmails(attachmentRequestVM);
                //If there is any error in uploading the email attachment, send that error information to the UI
                if (genericResponse != null && genericResponse.IsError == true)
                {                                
                    return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);
                }
                //If the email attachment is success, send the success response to the user
                genericResponse = new GenericResponseVM()
                {
                    Code = HttpStatusCode.OK.ToString(),
                    Value = "Attachment upload success"
                };
                //Return the response with proper http status code and proper response object
                return matterCenterServiceFunctions.ServiceResponse(genericResponse, (int)HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }
    }
}