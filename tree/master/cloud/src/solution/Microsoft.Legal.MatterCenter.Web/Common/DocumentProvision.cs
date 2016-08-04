﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Legal.MatterCenter.Models;
using Microsoft.Legal.MatterCenter.Utility;
using System.Net;
using Microsoft.Legal.MatterCenter.Repository;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net.Http;
using Microsoft.Exchange.WebServices.Data;

using System.Reflection;

namespace Microsoft.Legal.MatterCenter.Web.Common
{
    public class DocumentProvision : IDocumentProvision
    {
        private IDocumentRepository docRepository;
        private IUploadHelperFunctions uploadHelperFunctions;
        private IUserRepository userRepository;
        private GeneralSettings generalSettings;
        private DocumentSettings documentSettings;
        private ICustomLogger customLogger;
        private LogTables logTables;
        private ErrorSettings errorSettings;
        public DocumentProvision(IDocumentRepository docRepository, 
            IUserRepository userRepository, 
            IUploadHelperFunctions uploadHelperFunctions, 
            IOptions<GeneralSettings> generalSettings, 
            IOptions<DocumentSettings> documentSettings, 
            ICustomLogger customLogger, 
            IOptions<LogTables> logTables, IOptions<ErrorSettings> errorSettings)
        {
            this.docRepository = docRepository;
            this.uploadHelperFunctions = uploadHelperFunctions;
            this.userRepository = userRepository;
            this.generalSettings = generalSettings.Value;
            this.documentSettings = documentSettings.Value;
            this.customLogger = customLogger;
            this.logTables = logTables.Value;
            this.errorSettings = errorSettings.Value;
        }

        public async Task<int> GetAllCounts(SearchRequestVM searchRequestVM)
        {
            try
            {
                searchRequestVM.SearchObject.Filters.FilterByMe = 0;
                var searchObject = searchRequestVM.SearchObject;
                // Encode all fields which are coming from js
                SearchUtility.EncodeSearchDetails(searchObject.Filters, false);
                // Encode Search Term
                searchObject.SearchTerm = (searchObject.SearchTerm != null) ?
                    WebUtility.HtmlEncode(searchObject.SearchTerm).Replace(ServiceConstants.ENCODED_DOUBLE_QUOTES,
                    ServiceConstants.DOUBLE_QUOTE) : string.Empty;

                var searchResultsVM = await docRepository.GetDocumentsAsync(searchRequestVM);
                return searchResultsVM.TotalRows;
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }

        }

        public async Task<int> GetMyCounts(SearchRequestVM searchRequestVM)
        {
            try
            {
                searchRequestVM.SearchObject.Filters.FilterByMe = 1;
                var searchObject = searchRequestVM.SearchObject;
                // Encode all fields which are coming from js
                SearchUtility.EncodeSearchDetails(searchObject.Filters, false);
                // Encode Search Term
                searchObject.SearchTerm = (searchObject.SearchTerm != null) ?
                    WebUtility.HtmlEncode(searchObject.SearchTerm).Replace(ServiceConstants.ENCODED_DOUBLE_QUOTES,
                    ServiceConstants.DOUBLE_QUOTE) : string.Empty;

                var searchResultsVM = await docRepository.GetDocumentsAsync(searchRequestVM);
                return searchResultsVM.TotalRows;
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        public async Task<int> GetPinnedCounts(Client client)
        {
            try
            {
                var pinResponseVM = await docRepository.GetPinnedRecordsAsync(client);
                return pinResponseVM.TotalRows;
            }
            catch (Exception ex)
            {
                customLogger.LogError(ex, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, logTables.SPOLogTable);
                throw;
            }
        }

        public Stream DownloadAttachments(MailAttachmentDetails mailAttachmentDetails)
        {
            
            ///// filename, stream
            Dictionary<string, Stream> collectionOfAttachments = new Dictionary<string, Stream>();
            ///// full URL, relative URL
            string[] allAttachmentUrl = mailAttachmentDetails.FullUrl.Split(';');

            bool attachmentFlag = mailAttachmentDetails.IsAttachmentCall;
            foreach (string attachmentUrl in allAttachmentUrl)
            {
                if (!string.IsNullOrWhiteSpace(attachmentUrl))
                {
                    string uniqueKeyWithDate = attachmentUrl.Split(Convert.ToChar(ServiceConstants.DOLLAR, CultureInfo.InvariantCulture))[1].Substring(attachmentUrl.Split(Convert.ToChar(ServiceConstants.DOLLAR, 
                        CultureInfo.InvariantCulture))[1].LastIndexOf(Convert.ToChar(ServiceConstants.BACKWARD_SLASH, CultureInfo.InvariantCulture)) + 1) + ServiceConstants.DOLLAR + Guid.NewGuid();
                    Stream fileStream = docRepository.DownloadAttachments(attachmentUrl);
                    collectionOfAttachments.Add(uniqueKeyWithDate, fileStream);
                }
            }
            return GenerateEmail(collectionOfAttachments, allAttachmentUrl, attachmentFlag);            
        }       


        private Stream GenerateEmail(Dictionary<string, Stream> collectionOfAttachments, string[] documentUrls, bool attachmentFlag)
        {
            Stream result = null;
            try
            {
                MemoryStream mailFile = GetMailAsStream(collectionOfAttachments, documentUrls, attachmentFlag);
                mailFile.Position = 0;
                var fileContentResponse = new HttpResponseMessage(HttpStatusCode.OK);
                fileContentResponse.Headers.Clear();

                fileContentResponse.Content = new StreamContent(mailFile);

                fileContentResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ReturnExtension(string.Empty));
                //fileContentResponse.Headers.Add("Content-Type", ReturnExtension(string.Empty));
                fileContentResponse.Content.Headers.Add("Content-Length", mailFile.Length.ToString());
                fileContentResponse.Content.Headers.Add("Content-Description","File Transfer");
                fileContentResponse.Content.Headers.Add("Content-Disposition", "inline; filename=" + documentSettings.TempEmailName + new Guid().ToString() + ServiceConstants.EMAIL_FILE_EXTENSION);
                fileContentResponse.Content.Headers.Add("Content-Transfer-Encoding","binary");
                fileContentResponse.Content.Headers.Expires = DateTimeOffset.Now.AddDays(-1); ;
                fileContentResponse.Headers.Add("Cache-Control", "must-revalidate, post-check=0, pre-check=0");                
                fileContentResponse.Headers.Add("Pragma", "public");
                result = mailFile;
            }
            catch(Exception ex)
            {

            }
            return result;
        }


        /// <summary>
        /// Forms the memory stream of the mail with attachments.
        /// </summary>
        /// <param name="collectionOfAttachments">Collection of attachments as dictionary</param>
        /// <returns>Memory stream of the created mail object</returns>
        internal MemoryStream GetMailAsStream(Dictionary<string, Stream> collectionOfAttachments, string[] documentUrls, bool attachmentFlag)
        {
            MemoryStream result = null;
            string documentUrl = string.Empty;
            try
            {
                // need to be able to update/configure or get current version of server
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013);
                //// can use on premise exchange server credentials with service.UseDefaultCredentials = true, or explicitly specify the admin account (set default to false)
                service.Credentials = new WebCredentials(generalSettings.MailCartMailUserName, generalSettings.MailCartMailPassword);
                service.Url = new Uri(generalSettings.ExchangeServiceURL);
                Microsoft.Exchange.WebServices.Data.EmailMessage email = new Microsoft.Exchange.WebServices.Data.EmailMessage(service);
                email.Subject = documentSettings.MailCartMailSubject;

                if (attachmentFlag)
                {
                    email.Body = new MessageBody(documentSettings.MailCartMailBody);
                    foreach (KeyValuePair<string, Stream> mailAttachment in collectionOfAttachments)
                    {
                        if (null != mailAttachment.Value)
                        {
                            // Remove the date time string before adding the file as an attachment
                            email.Attachments.AddFileAttachment(mailAttachment.Key.Split('$')[0], mailAttachment.Value);
                        }
                    }
                }
                else
                {
                    int index = 0;
                    foreach (string currentURL in documentUrls)
                    {
                        if (null != currentURL && 0 < currentURL.Length)
                        {
                            string[] currentAssets = currentURL.Split('$');
                            string documentURL = generalSettings.SiteURL + currentAssets[1];
                            string documentName = currentAssets[2];

                            documentUrl = string.Concat(documentUrl, string.Format(CultureInfo.InvariantCulture, "'{0} ) {1} : <a href='{2}'>{2} </a><br/>" , ++index, documentName, documentURL));
                        }
                    }
                    documentUrl = string.Format(CultureInfo.InvariantCulture, "<div style='font-family:Calibri;font-size:12pt'>{0}</div>", documentUrl);
                    email.Body = new MessageBody(documentUrl);
                }
                //// This header allows us to open the .eml in compose mode in outlook
                email.SetExtendedProperty(new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Unsent", MapiPropertyType.String), "1");
                email.Save(WellKnownFolderName.Drafts); // must save draft in order to get MimeContent
                email.Load(new PropertySet(EmailMessageSchema.MimeContent));
                MimeContent mimcon = email.MimeContent;
                //// Do not make the StylCop fixes for MemoryStream here
                MemoryStream fileContents = new MemoryStream();
                fileContents.Write(mimcon.Content, 0, mimcon.Content.Length);
                fileContents.Position = 0;
                result = fileContents;
            }
            catch (Exception exception)
            {
                //Logger.LogError(exception, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ServiceConstantStrings.LogTableName);
                MemoryStream fileContents = new MemoryStream();
                result = fileContents;
            }
            return result;
        }

        /// <summary>
        /// Gets the file content type based on specified extensions.
        /// </summary>
        /// <param name="fileExtension">Extension of the file</param>
        /// <returns>File content type</returns>
        internal static string ReturnExtension(string fileExtension)
        {
            string result = string.Empty;
            switch (fileExtension)
            {
                case ".txt":
                    result = "text/plain";
                    break;
                case ".doc":
                    result = "application/ms-word";
                    break;
                case ".xls":
                    result = "application/vnd.ms-excel";
                    break;
                case ".gif":
                    result = "image/gif";
                    break;
                case ".jpg":
                case "jpeg":
                    result = "image/jpeg";
                    break;
                case ".bmp":
                    result = "image/bmp";
                    break;
                case ".wav":
                    result = "audio/wav";
                    break;
                case ".ppt":
                    result = "application/mspowerpoint";
                    break;
                case ".dwg":
                    result = "image/vnd.dwg";
                    break;
                default:
                    result = "application/octet-stream";
                    break;
            }
            return result;
        }

        public GenericResponseVM UploadAttachments(AttachmentRequestVM attachmentRequestVM)
        {
            int attachmentCount = 0;
            string message = string.Empty;
            var client = attachmentRequestVM.Client;
            var serviceRequest = attachmentRequestVM.ServiceRequest;            
            GenericResponseVM genericResponse = null;            
            foreach (AttachmentDetails attachment in serviceRequest.Attachments)
            {
                genericResponse = uploadHelperFunctions.Upload(client, serviceRequest, ServiceConstants.ATTACHMENT_SOAP_REQUEST, attachment.id, false,
                    attachment.name, serviceRequest.FolderPath[attachmentCount], false, ref message,
                    attachment.originalName);
                if (genericResponse!=null && genericResponse.IsError==true)
                {
                    //result = false;
                    break;
                }
                attachmentCount++;
            }            
            return genericResponse;
        }

        public GenericResponseVM UploadEmails(AttachmentRequestVM attachmentRequestVM)
        {
            string message = string.Empty;
            var client = attachmentRequestVM.Client;
            var serviceRequest = attachmentRequestVM.ServiceRequest;            
            GenericResponseVM genericResponse = uploadHelperFunctions.Upload(client, serviceRequest, ServiceConstants.MAIL_SOAP_REQUEST, serviceRequest.MailId, true,
                        serviceRequest.Subject, serviceRequest.FolderPath[0], true, ref message, string.Empty);            
            return genericResponse;
        }

        public GenericResponseVM CheckDuplicateDocument(string clientUrl, string folderName, string documentLibraryName, 
            string fileName, ContentCheckDetails contentCheck, bool allowContentCheck)
        {
            GenericResponseVM genericResponse = null;
            DuplicateDocument duplicateDocument = uploadHelperFunctions.DocumentExists(clientUrl, contentCheck, documentLibraryName, folderName, false);
            if (duplicateDocument != null && duplicateDocument.DocumentExists)
            {
                string documentPath = string.Concat(generalSettings.SiteURL, folderName, ServiceConstants.FORWARD_SLASH, fileName);
                string duplicateMessage = (allowContentCheck && duplicateDocument.HasPotentialDuplicate) ? errorSettings.FilePotentialDuplicateMessage : errorSettings.FileAlreadyExistMessage;
                duplicateMessage = $"{duplicateMessage}|{duplicateDocument.HasPotentialDuplicate}";
                genericResponse = new GenericResponseVM()
                {
                    IsError = true,
                    Code = UploadEnums.DuplicateDocument.ToString(),
                    Value = string.Format(CultureInfo.InvariantCulture, duplicateMessage, fileName, documentPath)
                };

            }
            return genericResponse;
        }

        public GenericResponseVM PerformContentCheck(string clientUrl, string folderUrl, IFormFile uploadedFile, string fileName)
        {
            GenericResponseVM genericResponse = null;
            genericResponse = uploadHelperFunctions.PerformContentCheck(clientUrl, folderUrl, uploadedFile, fileName);            
            return genericResponse;
        }

        public async Task<SearchResponseVM> GetDocumentsAsync(SearchRequestVM searchRequestVM)
        {
            var searchObject = searchRequestVM.SearchObject;
            // Encode all fields which are coming from js
            SearchUtility.EncodeSearchDetails(searchObject.Filters, false);
            // Encode Search Term
            searchObject.SearchTerm = (searchObject.SearchTerm != null) ?
                WebUtility.HtmlEncode(searchObject.SearchTerm).Replace(ServiceConstants.ENCODED_DOUBLE_QUOTES, ServiceConstants.DOUBLE_QUOTE) : string.Empty;

            var searchResultsVM = await docRepository.GetDocumentsAsync(searchRequestVM);

            if (searchResultsVM.TotalRows > 0)
            {
                IList<DocumentData> documentDataList = new List<DocumentData>();
                IEnumerable<IDictionary<string, object>> searchResults = searchResultsVM.SearchResults;
                foreach (var searchResult in searchResults)
                {
                    DocumentData documentData = new DocumentData();
                    foreach (var key in searchResult.Keys)
                    {
                        documentData.Checker = false;
                        switch (key.ToLower())
                        {
                            
                            case "mcdocumentclientname":
                                documentData.DocumentClient = searchResult[key].ToString();
                                break;
                            case "mcdocumentclientid":
                                documentData.DocumentClientId = searchResult[key].ToString();
                                break;
                            case "sitename":
                                documentData.DocumentClientUrl = searchResult[key].ToString();
                                break;
                            case "mcversionnumber":
                                documentData.DocumentVersion = searchResult[key].ToString();
                                break;
                            case "refinablestring13":
                                documentData.DocumentMatter = searchResult[key].ToString();
                                break;
                            case "refinablestring12":
                                documentData.DocumentMatterId = searchResult[key].ToString();
                                break;
                            case "filename":
                                documentData.DocumentName = searchResult[key].ToString();
                                break;
                            case "mccheckoutuser":
                                documentData.DocumentCheckoutUser = searchResult[key].ToString();
                                break;
                            case "created":
                                documentData.DocumentCreatedDate = searchResult[key].ToString();
                                break;
                            case "fileextension":
                                documentData.DocumentExtension = searchResult[key].ToString();
                                if (documentData.DocumentExtension.ToLower() != "pdf")
                                {
                                    documentData.DocumentIconUrl = $"{generalSettings.SiteURL}/_layouts/15/images/ic{documentData.DocumentExtension}.gif";
                                }
                                else
                                {
                                    documentData.DocumentIconUrl = $"{generalSettings.SiteURL}/_layouts/15/images/ic{documentData.DocumentExtension}.png";
                                }
                                break;
                            case "docid":
                                documentData.DocumentID = searchResult[key].ToString();
                                break;
                            case "path":
                                documentData.DocumentOWAUrl = searchResult[key].ToString();
                                documentData.DocumentUrl = searchResult[key].ToString();
                                break;
                            case "serverredirectedurl":
                                if (searchResult[key] != null)
                                {
                                    documentData.DocumentOWAUrl = searchResult[key].ToString();
                                    documentData.DocumentUrl = searchResult[key].ToString();
                                }
                                break;
                            case "lastmodifiedtime":
                                documentData.DocumentModifiedDate= searchResult[key].ToString();
                                break;
                            
                            case "msitofficeauthor":
                                documentData.DocumentOwner = searchResult[key].ToString();
                                break;
                            case "parentlink":
                                documentData.DocumentParentUrl = searchResult[key].ToString();
                                documentData.DocumentMatterUrl = documentData.DocumentParentUrl.Substring(0, documentData.DocumentParentUrl.LastIndexOf("/"));
                                break;
                                
                        }
                        documentData.PinType = "Pin";
                        
                                               
                    }
                    documentDataList.Add(documentData);
                }
                searchResultsVM.DocumentDataList = documentDataList;
                
            }
            searchResultsVM.SearchResults = null;
            return searchResultsVM;
        }

        public GenericResponseVM UploadFiles(IFormFile uploadedFile, string fileExtension, string originalName, 
            string folderName, string fileName, string clientUrl, string folder, string documentLibraryName)
        {
            GenericResponseVM genericResponse = null;
            try
            {                
                Dictionary<string, string> mailProperties = ContinueUpload(uploadedFile, fileExtension);
                //setting original name property for attachment
                if (string.IsNullOrWhiteSpace(mailProperties[ServiceConstants.MailOriginalName]))
                {
                    mailProperties[ServiceConstants.MAIL_ORIGINAL_NAME] = originalName;
                }

                genericResponse = docRepository.UploadDocument(folderName, uploadedFile, fileName, mailProperties, clientUrl, folder, documentLibraryName);
            }
            catch(Exception ex)
            {
                genericResponse = new GenericResponseVM()
                {
                    Code = UploadEnums.UploadFailure.ToString(),
                    Value = folderName,
                    IsError = true
                };
            }
            return genericResponse;

        }

        private Dictionary<string, string> ContinueUpload(IFormFile uploadedFile, string fileExtension)
        {
            Dictionary<string, string> mailProperties = new Dictionary<string, string>
                            {
                                { ServiceConstants.MAIL_SENDER_KEY, string.Empty },
                                { ServiceConstants.MAIL_RECEIVER_KEY, string.Empty },
                                { ServiceConstants.MAIL_RECEIVED_DATEKEY, string.Empty },
                                { ServiceConstants.MAIL_CC_ADDRESS_KEY, string.Empty },
                                { ServiceConstants.MAIL_ATTACHMENT_KEY, string.Empty },
                                { ServiceConstants.MAIL_SEARCH_EMAIL_SUBJECT, string.Empty },
                                { ServiceConstants.MAIL_SEARCH_EMAIL_FROM_MAILBOX_KEY, string.Empty },
                                { ServiceConstants.MAIL_FILE_EXTENSION_KEY, fileExtension },
                                { ServiceConstants.MAIL_IMPORTANCE_KEY, string.Empty},
                                { ServiceConstants.MAIL_CONVERSATIONID_KEY, string.Empty},
                                { ServiceConstants.MAIL_CONVERSATION_TOPIC_KEY, string.Empty},
                                { ServiceConstants.MAIL_SENT_DATE_KEY, string.Empty},
                                { ServiceConstants.MAIL_HAS_ATTACHMENTS_KEY, string.Empty},
                                { ServiceConstants.MAIL_SENSITIVITY_KEY, string.Empty },
                                { ServiceConstants.MAIL_CATEGORIES_KEY, string.Empty },
                                { ServiceConstants.MailOriginalName, string.Empty}
                            };
            if (string.Equals(fileExtension, ServiceConstants.EMAIL_FILE_EXTENSION, StringComparison.OrdinalIgnoreCase))
            {
                var client = new Client()
                {
                    Url = generalSettings.CentralRepositoryUrl
                };
                
                Users currentUserDetail = userRepository.GetLoggedInUserDetails(client);
                mailProperties[ServiceConstants.MAIL_SEARCH_EMAIL_FROM_MAILBOX_KEY] = currentUserDetail.Name;
                Stream fileStream = uploadedFile.OpenReadStream();
                mailProperties = MailMessageParser.GetMailFileProperties(fileStream, mailProperties);       // Reading properties only for .eml file 
               
            }
            return mailProperties;
        }
    }
}
