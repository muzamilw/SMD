using Microsoft.Practices.Unity;
using SMD.ExceptionHandling.Logger;
using SMD.Implementation.Identity;
using SMD.Implementation.Services;
using SMD.Interfaces;
using SMD.Interfaces.Logger;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;

namespace SMD.Implementation
{
    /// <summary>
    /// Type Registration for Implemention 
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Implementation
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<ISMDLogger, SMDLogger>();
            unityContainer.RegisterType<IAuthorizationChecker, AuthorizationChecker>();
            unityContainer.RegisterType<IClaimsSecurityService, ClaimsSecurityService>();
            unityContainer.RegisterType<IProfileQuestionService, ProfileQuestionService>();
            unityContainer.RegisterType<IEmailManagerService, EmailManagerService>();
            unityContainer.RegisterType<IAdvertService, AdvertService>();
            unityContainer.RegisterType<ISurveyQuestionService, SurveyQuestionService>();
            unityContainer.RegisterType<IProfileQuestionAnswerService, ProfileQuestionAnswerService>();
            unityContainer.RegisterType<IWebApiUserService, WebApiUserService>();
            unityContainer.RegisterType<IAuditLogService, AuditLogService>();
            unityContainer.RegisterType<IPaypalService, PaypalService>();
            unityContainer.RegisterType<IProfileQuestionUserAnswerService, ProfileQuestionUserAnswerService>();
            unityContainer.RegisterType<IInvoiceService, InvoiceService>();
            unityContainer.RegisterType<ITransactionService, TransactionService>();
            unityContainer.RegisterType<IInvoiceDetailService, InvoiceDetailService>();
            unityContainer.RegisterType<IStripeService, StripeService>();
            unityContainer.RegisterType<ITransactionLogService, TransactionLogService>();
            unityContainer.RegisterType<IAccountService, AccountService>();
            unityContainer.RegisterType<ICityService, CityService>();
            unityContainer.RegisterType<IEducationService, EducationService>();
            unityContainer.RegisterType<IIndustryService, IndustryService>();
            unityContainer.RegisterType<ICompanyService, CompanyService>();
            unityContainer.RegisterType<IManageUserService, ManageUserService>();
            unityContainer.RegisterType<ICouponCategoryService, CouponCategoryService>();
            unityContainer.RegisterType<ICouponService, CouponService>();
            unityContainer.RegisterType<ISectionService, SectionService>();
            unityContainer.RegisterType<IPhraseService, PhraseService>();
            unityContainer.RegisterType<IBrachCategoryService, BrachCategoryService>();
            unityContainer.RegisterType<ICompanyBranchService, CompanyBranchService>();
            unityContainer.RegisterType<IDamImageService, DamImageService>();
            unityContainer.RegisterType<IGameService, GameService>();
            unityContainer.RegisterType<IActiveUser, ActiveUser>();
            unityContainer.RegisterType<IDashboardService, DashboardService>();
            
        }
    }
}