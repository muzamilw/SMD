using System.Collections.Generic;
using System.Globalization;
using SMD.Common;
using SMD.ExceptionHandling;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using SMD.Repository.Repositories;
using SMD.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SMD.Implementation.Identity;



namespace SMD.Implementation.Services
{
    /// <summary>
    /// Profile Question Service 
    /// </summary>
    public sealed class ProfileQuestionService : IProfileQuestionService
    {
        #region Private
        private readonly IProfileQuestionRepository _profileQuestionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IProfileQuestionGroupRepository _profileQuestionGroupRepository;
        private readonly IProfileQuestionAnswerRepository _profileQuestionAnswerRepository;
        private readonly IIndustryRepository _industoryRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IProfileQuestionTargetCriteriaRepository _profileQuestionTargetCriteriaRepository;
        private readonly IProfileQuestionTargetLocationRepository _profileQuestionTargetLocationRepository;
        private readonly IEmailManagerService _emailManagerService;
        private readonly IStripeService _stripeService;
        //private readonly WebApiUserService _webApiUserService;
        private readonly IProductRepository _productRepository;
        private readonly ITaxRepository _taxRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>

        public ProfileQuestionService(IProfileQuestionRepository profileQuestionRepository, ICountryRepository countryRepository,
            ILanguageRepository languageRepository, IProfileQuestionGroupRepository profileQuestionGroupRepository,
            IProfileQuestionAnswerRepository profileQuestionAnswerRepository, IIndustryRepository industoryRepository, IEducationRepository educationRepository, IProfileQuestionTargetCriteriaRepository profileQuestionTargetCriteriaRepository, IProfileQuestionTargetLocationRepository profileQuestionTargetLocationRepository
            , IEmailManagerService emailManagerService, IStripeService stripeService, IProductRepository productRepository, ITaxRepository taxRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository)
        {
            _profileQuestionRepository = profileQuestionRepository;
            _countryRepository = countryRepository;
            _languageRepository = languageRepository;
            _profileQuestionGroupRepository = profileQuestionGroupRepository;
            _profileQuestionAnswerRepository = profileQuestionAnswerRepository;
            _industoryRepository = industoryRepository;
            _educationRepository = educationRepository;
            _profileQuestionTargetCriteriaRepository = profileQuestionTargetCriteriaRepository;
            _profileQuestionTargetLocationRepository = profileQuestionTargetLocationRepository;
            _emailManagerService = emailManagerService;
            _stripeService = stripeService;
            //_webApiUserService = webApiUserService;
            _productRepository = productRepository;
            _taxRepository = taxRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Skip Profile Question
        /// </summary>
        public void PerformSkip(int pqId)
        {
            ProfileQuestion profileQuestion = _profileQuestionRepository.Find(pqId);
            if (profileQuestion == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.ProfileQuestionService_ProfileQuestionNotFound, pqId));
            }

            // Update Skipped Count
            if (profileQuestion.SkippedCount == null)
            {
                profileQuestion.SkippedCount = 0;
            }

            profileQuestion.SkippedCount += 1;

            // Save Changes
            _profileQuestionRepository.SaveChanges();
        }

        /// <summary>
        /// Profile Question Search request 
        /// </summary>
        public ProfileQuestionSearchRequestResponse GetProfileQuestions(ProfileQuestionSearchRequest request)
        {
            int rowCount;
           // var obj = _profileQuestionRepository.SearchProfileQuestions(request, out rowCount);
            

            //string code = Convert.ToString((int)ProductCode.);
            //var product = productRepository.GetAll().Where(g => g.ProductCode == code).FirstOrDefault();
            //var userBaseData = surveyQuestionRepository.getBaseData();
            //double? setupPrice = 0;
            //if (product != null)
            //{
            //    userBaseData.CurrencySymbol = product.Currency != null ? product.Currency.CurrencyCode : "$";
            //    setupPrice = product.SetupPrice;
            //}
            return new ProfileQuestionSearchRequestResponse
            {
                ProfileQuestions = _profileQuestionRepository.SearchProfileQuestions(request, out rowCount),
                Countries = _countryRepository.GetAllCountries(),
                Languages = _languageRepository.GetAllLanguages(),
                Professions = _industoryRepository.GetAll(),
                Education = _educationRepository.GetAll(),
                Industry = _industoryRepository.GetAll(),
                objBaseData = _profileQuestionRepository.getBaseData(),
                TotalCount = rowCount
            };

        }

        /// <summary>
        /// Delete Profile Question
        /// </summary>
        public bool DeleteProfileQuestion(ProfileQuestion profileQuestion)
        {
            var dBprofileQuestion = _profileQuestionRepository.Find(profileQuestion.PqId);
            if (dBprofileQuestion != null)
            {
                dBprofileQuestion.Status = (Int32)ObjectStatus.Archived;
                _profileQuestionRepository.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get Base Data for PQ
        /// </summary>
        public ProfileQuestionBaseResponse GetProfileQuestionBaseData()
        {
            return new ProfileQuestionBaseResponse
            {
                Countries = _countryRepository.GetAllCountries(),
                Languages = _languageRepository.GetAllLanguages(),
                ProfileQuestionGroups = _profileQuestionGroupRepository.GetAllProfileQuestionGroups(),
                ProfileQuestions = _profileQuestionRepository.GetAllProfileQuestions(),
                objBaseData = _profileQuestionRepository.getBaseData(),
            };
        }

        /// <summary>
        /// Save Profile Question
        /// </summary>
        public ProfileQuestion SaveProfileQuestion(ProfileQuestion source)
        {
            var user = UserManager.Users.Where(g => g.Id == _profileQuestionRepository.LoggedInUserIdentity).SingleOrDefault();
            
                


            var serverObj = _profileQuestionRepository.Find(source.PqId);
            //var user = UserManager.Users.Where(g => g.Id == _profileQuestionRepository.LoggedInUserIdentity).SingleOrDefault();

            #region Edit Question
            // Edit Profile Question 
            if (serverObj != null)
            {
                serverObj.Question = source.Question;

                serverObj.Priority = source.Priority;
                serverObj.HasLinkedQuestions = source.HasLinkedQuestions;
                serverObj.LanguageId = source.LanguageId;
                serverObj.CountryId = source.CountryId;
                serverObj.ProfileGroupId = source.ProfileGroupId;
                serverObj.Type = source.Type;
                serverObj.RefreshTime = source.RefreshTime;
                serverObj.SkippedCount = source.SkippedCount;
                serverObj.ModifiedDate = source.ModifiedDate;
                serverObj.PenalityForNotAnswering = source.PenalityForNotAnswering;
                serverObj.Status = source.Status;
                serverObj.ModifiedDate = DateTime.Now.Add(-(_profileQuestionRepository.UserTimezoneOffSet));

                if (source.ProfileQuestionAnswers != null)
                {
                    if (serverObj.ProfileQuestionAnswers == null)
                    {
                        serverObj.ProfileQuestionAnswers = new List<ProfileQuestionAnswer>();
                    }
                    #region Answer Add/Edit
                    // Add/Edit Answer
                    foreach (var answer in source.ProfileQuestionAnswers)
                    {
                        var serverAns = _profileQuestionAnswerRepository.Find(answer.PqAnswerId);
                        if (serverAns != null)
                        {
                            serverAns.PqId = serverObj.PqId;
                            serverAns.Type = answer.Type;
                            serverAns.AnswerString = answer.AnswerString;
                            serverAns.ImagePath = answer.ImagePath;
                            serverAns.LinkedQuestion1Id = answer.LinkedQuestion1Id;
                            serverAns.LinkedQuestion2Id = answer.LinkedQuestion2Id;
                            serverAns.LinkedQuestion3Id = answer.LinkedQuestion3Id;
                            serverAns.LinkedQuestion4Id = answer.LinkedQuestion4Id;

                            serverAns.LinkedQuestion5Id = answer.LinkedQuestion5Id;
                            serverAns.LinkedQuestion6Id = answer.LinkedQuestion6Id;
                            serverAns.PqAnswerId = answer.PqAnswerId;
                            serverAns.SortOrder = answer.SortOrder;
                            serverAns.Status = (int)ObjectStatus.Active;
                            if (serverAns.Type == 2)
                            {
                                serverAns.ImagePath = SaveAnswerImage(serverAns);
                            }
                        }
                        else
                        {
                            serverAns = new ProfileQuestionAnswer
                            {
                                PqId = serverObj.PqId,
                                Type = answer.Type,
                                AnswerString = answer.AnswerString,
                                ImagePath = answer.ImagePath,
                                Status = (int)ObjectStatus.Active,
                                LinkedQuestion1Id = answer.LinkedQuestion1Id,
                                LinkedQuestion2Id = answer.LinkedQuestion2Id,
                                LinkedQuestion3Id = answer.LinkedQuestion3Id,
                                LinkedQuestion4Id = answer.LinkedQuestion4Id,
                                LinkedQuestion5Id = answer.LinkedQuestion5Id,
                                LinkedQuestion6Id = answer.LinkedQuestion6Id,
                                PqAnswerId = answer.PqAnswerId,
                                SortOrder = answer.SortOrder
                            };
                            if (serverAns.Type == 2)
                            {
                                serverAns.ImagePath = SaveAnswerImage(serverAns);
                            }
                            _profileQuestionAnswerRepository.Add(serverAns);
                            serverObj.ProfileQuestionAnswers.Add(serverAns);
                        }
                    }

                    #endregion
                }

                #region Answer Deletion

                if (source.ProfileQuestionAnswers == null)
                {
                    source.ProfileQuestionAnswers = new Collection<ProfileQuestionAnswer>();
                }
                var serverListOfAns = _profileQuestionAnswerRepository.GetProfileQuestionAnswerByQuestionId(source.PqId);
                if (serverListOfAns != null)
                {
                    foreach (var serverAns in serverListOfAns)
                    {
                        if (!source.ProfileQuestionAnswers.Any(ans => ans.PqAnswerId == serverAns.PqAnswerId))
                        {
                            serverAns.Status = (Int32)ObjectStatus.Archived;
                        }
                    }
                }
                #endregion
            }
            #endregion
            #region Add Question
            else
            {

                IEnumerable<SmdRoleClaimValue> roleClaim = ClaimHelper.GetClaimsByType<SmdRoleClaimValue>(SmdClaimTypes.Role);
                string RoleName = roleClaim != null && roleClaim.Any() ? roleClaim.ElementAt(0).Role : "Role Not Loaded";

                int? compid = 0;

                if (RoleName.StartsWith("Franchise"))
                    compid = null;
                else
                    compid = _profileQuestionRepository.CompanyId;

                serverObj = new ProfileQuestion
                {
                  
                    Question = source.Question,
                    Priority = source.Priority,
                    HasLinkedQuestions = source.HasLinkedQuestions,
                    LanguageId = source.LanguageId,
                    CountryId = source.CountryId,
                    ProfileGroupId = source.ProfileGroupId,
                    Type = source.Type,
                    RefreshTime = source.RefreshTime,
                    SkippedCount = source.SkippedCount,
                    ModifiedDate = source.ModifiedDate,
                    PenalityForNotAnswering = source.PenalityForNotAnswering,
                    Status = source.Status,
                    CompanyId = compid,
                    AgeRangeStart = source.AgeRangeStart,
                    AgeRangeEnd = source.AgeRangeEnd,
                    Gender = source.Gender,
                    SubmissionDateTime=source.SubmissionDateTime,
                    CreatedBy=source.CreatedBy,
                    UserID=source.UserID,
                    AmountCharged = source.AmountCharged,
                    AnswerNeeded =source.AnswerNeeded
                    //    CreatedBy = user.FullName
                };
                serverObj.CompanyId = compid;
                if (serverObj.Status == 2)
                {
                    serverObj.SubmissionDateTime = DateTime.Now;
                }
                 if (user != null)
                 {
                     serverObj.CreatedBy = user.FullName;
                     serverObj.UserID = user.Id;
                 }

                _profileQuestionRepository.Add(serverObj);
                _profileQuestionRepository.SaveChanges();
                if (serverObj.ProfileQuestionAnswers == null)
                {
                    serverObj.ProfileQuestionAnswers = new List<ProfileQuestionAnswer>();
                }
                foreach (var answer in source.ProfileQuestionAnswers)
                {
                    var serverAns = new ProfileQuestionAnswer
                        {
                            PqId = serverObj.PqId,
                            Type = answer.Type,
                            AnswerString = answer.AnswerString,
                            LinkedQuestion1Id = answer.LinkedQuestion1Id,
                            LinkedQuestion2Id = answer.LinkedQuestion2Id,
                            LinkedQuestion3Id = answer.LinkedQuestion3Id,
                            LinkedQuestion4Id = answer.LinkedQuestion4Id,
                            LinkedQuestion5Id = answer.LinkedQuestion5Id,
                            LinkedQuestion6Id = answer.LinkedQuestion6Id,
                            PqAnswerId = answer.PqAnswerId,
                            SortOrder = answer.SortOrder,
                            Status = (int)ObjectStatus.Active
                        };
                    if (serverAns.Type == 2)
                    {
                        serverAns.ImagePath = SaveAnswerImage(answer);
                    }
                    _profileQuestionAnswerRepository.Add(serverAns);
                    _profileQuestionAnswerRepository.SaveChanges();
                    serverObj.ProfileQuestionAnswers.Add(serverAns);
                }
            }
            #endregion
            foreach (var loc in source.ProfileQuestionTargetLocations)
            {
                
                if (loc.ID != 0)
                {
                    _profileQuestionTargetLocationRepository.Update(loc);
                    _profileQuestionTargetLocationRepository.SaveChanges();
                }
                else
                {
                    if (loc.CityID > 0 && loc.CountryID > 0)
                    {
                        loc.PQID = serverObj.PqId;
                        _profileQuestionTargetLocationRepository.Add(loc);
                        _profileQuestionTargetLocationRepository.SaveChanges();
                    }
                }
            }
            // add or update criteria
            foreach (var criteria in source.ProfileQuestionTargetCriterias)
            {
                if (criteria.PQID > 0 && criteria.IsDeleted)
                {
                    _profileQuestionTargetCriteriaRepository.RemoveCriteria(criteria.PQID??0);
                }
                else
                {
                    if (criteria.PQID != 0)
                    {
                        if (criteria.Type != (int)ProfileQuestionTargetCriteriaType.Language && criteria.Type != (int)ProfileQuestionTargetCriteriaType.Industry)  // industry and languages are addable and deleteable
                            _profileQuestionTargetCriteriaRepository.Update(criteria);
                        _profileQuestionTargetCriteriaRepository.SaveChanges();
                    }
                    else
                    {
                        criteria.PQID = serverObj.PqId;
                        _profileQuestionTargetCriteriaRepository.Add(criteria);
                        _profileQuestionTargetCriteriaRepository.SaveChanges();
                    }
                }
            }
            // _profileQuestionRepository.SaveChanges();
            return _profileQuestionRepository.Find(serverObj.PqId);

        }

        /// <summary>
        /// Save Answer Image
        /// </summary>
        public string SaveAnswerImage(ProfileQuestionAnswer source)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["SMD_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath =
                server.MapPath(mpcContentPath + "/ProfileQuestions/" + "PQId-" + source.PqId +
                               "/PQAId-" + source.PqAnswerId);

            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }

            mapPath = ImageHelper.Save(mapPath, string.Empty, string.Empty,
                "AnswerImage_" + DateTime.Now.Second + ".png", source.ImagePath, source.ImageUrlBytes);

            return mapPath;
        }

        /// <summary>
        /// Profile Questions For Api
        /// </summary>
        public ProfileQuestionApiSearchResponse GetProfileQuestionsByGroupForApi(GetProfileQuestionApiRequest request)
        {
            //request.UserId = "02de201f-f4ea-420e-a1a8-74e7c1975277";
            var allPqGroups = _profileQuestionGroupRepository.GetAllProfileQuestionGroups();
            var unAnsweredQuestions = new List<ProfileQuestion>();
            double percentageCompleted = 0;

            foreach (var pqGroup in allPqGroups)
            {
                var unAnsweredQuestionsCount = _profileQuestionRepository.GetCountOfUnAnsweredQuestionsByGroupId(pqGroup.ProfileGroupId, request.UserId);
                int totalQuestionsCount = _profileQuestionRepository.GetTotalCountOfGroupQuestion(pqGroup.ProfileGroupId);
                if (unAnsweredQuestionsCount > 0)
                {
                    request.GroupId = pqGroup.ProfileGroupId;
                    unAnsweredQuestions = _profileQuestionRepository.GetUnansweredQuestionsByGroupId(request).ToList();
                    percentageCompleted = ((totalQuestionsCount - unAnsweredQuestionsCount) * 100) / totalQuestionsCount;
                    break;
                }
                // Reseting 
                request.TotalCount = 0;
                request.PageNo = 1;

            }
            return new ProfileQuestionApiSearchResponse
            {
                ProfileQuestions = unAnsweredQuestions,
                PercentageCompleted = percentageCompleted
            };
        }
        //private ApplicationUserManager UserManager
        //{
        //    get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        //}
        public ProfileQuestionResponseModelForApproval GetProfileQuestionForAproval(GetPagedListRequest request)
        {
            int rowCount;
            return new ProfileQuestionResponseModelForApproval
            {
                Coupons = _profileQuestionRepository.GetProfileQuestionsForApproval(request, out rowCount).ToList(),
                TotalCount = rowCount
            };
        }
        //public List<ProfileQuestionTargetLocation> GetPQlocation(long pqId)
        //{
        //    return _profileQuestionTargetLocationRepository.GetPQlocation(pqId);

        //}
        public string UpdatePQForApproval(ProfileQuestion source)
        {
            string respMesg = "True";
            var dbCo = _profileQuestionRepository.Find(source.PqId);
            // Update 
            if (dbCo != null)
            {
                // Approval
                if (source.Approved == true)
                {
                    dbCo.Approved = true;
                    dbCo.ApprovalDate = DateTime.Now;
                    dbCo.ApprovedByUserID = _profileQuestionRepository.LoggedInUserIdentity;
                    dbCo.Status = (Int32)AdCampaignStatus.Live;

                    // Stripe payment + Invoice Generation
                    // Muzi bhai said we will see it on latter stage 

                    //todo pilot: unCommenting Stripe payment code on Ads approval
                    respMesg = MakeStripePaymentandAddInvoiceForPQ(dbCo);
                    if (respMesg.Contains("Failed"))
                    {
                        return respMesg;
                    }
                }
                // Rejection 
                else
                {
                    dbCo.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbCo.Approved = false;
                    dbCo.RejectionReason = source.RejectionReason.ToString();
                    _emailManagerService.SendProfileQuestionRejectionEmailForApproval(dbCo.UserID, dbCo.RejectionReason);
                }
                dbCo.ModifiedDate = DateTime.Now;
                dbCo.ModifiedBy = _profileQuestionRepository.LoggedInUserIdentity;

                _profileQuestionRepository.SaveChanges();

            }
            return respMesg;
        }
        private string MakeStripePaymentandAddInvoiceForPQ(ProfileQuestion source)
        {
            #region Stripe Payment
            string response = null;
            Boolean isSystemUser = false;
            double amount = 0;
            // User who added Campaign for approval 
            var user = UserManager.FindById(source.UserID);
            // Get Current Product
            var product = (dynamic)null;
            product = _productRepository.GetProductByCountryId("PQID");
          
            // Tax Applied
            var tax = _taxRepository.GetTaxByCountryId(user.Company.CountryId);
            // Total includes tax
            if (product != null)
            {
                amount = source.AmountCharged ?? 0 + tax.TaxValue ?? 0;


                // If It is not System User then make transation 
                //if (user.Roles.Any(role => role.Name.ToLower().Equals("user")))
                //{
                // Make Stripe actual payment 
                response = _stripeService.ChargeCustomer((int?)amount, user.Company.StripeCustomerId);
                isSystemUser = false;

            }

            #endregion

            if (response != null && !response.Contains("Failed"))
            {
                if (source.CompanyId != null)
                {
                    TransactionManager.ProfileQuestionApproveTransaction(source.PqId, amount, source.CompanyId.Value);


                    #region Add Invoice

                    // Add invoice data
                    var invoice = new Invoice
                    {
                        Country = user.Company.CountryId.ToString(),
                        Total = (double)amount,
                        NetTotal = (double)amount,
                        InvoiceDate = DateTime.Now,
                        InvoiceDueDate = DateTime.Now.AddDays(7),
                        Address1 = user.Company.CountryId.ToString(),
                        CompanyId = user.Company.CompanyId,
                        CompanyName = "My Company",
                        CreditCardRef = response
                    };
                    _invoiceRepository.Add(invoice);

                    #endregion
                    #region Add Invoice Detail

                    // Add Invoice Detail Data 
                    var invoiceDetail = new InvoiceDetail
                    {
                        InvoiceId = invoice.InvoiceId,
                        ProductId = product.ProductId,
                        ItemName = "Profile Question",
                        ItemAmount = (double)amount,
                        ItemTax = (double)tax.TaxValue,
                        ItemDescription = "This is description!",
                        ItemGrossAmount = (double)amount,
                        PQID   = source.PqId,

                    };
                    _invoiceDetailRepository.Add(invoiceDetail);
                    _invoiceDetailRepository.SaveChanges();

                    #endregion
                }
            }
            return response;
        }
        #endregion
    }
}
