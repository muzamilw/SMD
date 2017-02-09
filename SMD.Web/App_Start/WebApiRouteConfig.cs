using System.Net.Http;
using System.Web.Http;

// ReSharper disable CheckNamespace
namespace SMD.MIS
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// WebApi configurations 
    /// </summary>
    public class WebApiRouteConfig
    {
        public static void RegisterRoutes(HttpConfiguration config, HttpMessageHandler routeHandlers)
        {
            // Standard Login route
            config.Routes.MapHttpRoute(
                "StandardLogin",
                "Login/Standard/",
                new { controller = "StandardLogin" },
                null,
                routeHandlers);

            // External Login route
            config.Routes.MapHttpRoute(
                "ExternalLogin",
                "Login/External/",
                new { controller = "ExternalLogin" },
                null,
                routeHandlers);

            // Register External route
            config.Routes.MapHttpRoute(
                "RegisterExternal",
                "Register/External/",
                new { controller = "RegisterExternal" },
                null,
                routeHandlers);

            // Register Custom route
            config.Routes.MapHttpRoute(
                "RegisterCustom",
                "Register/Custom/",
                new { controller = "RegisterCustom" },
                null,
                routeHandlers);

            // Confirm Email - Register Custom route
            config.Routes.MapHttpRoute(
                "ConfirmEmail",
                "Register/Confirm/",
                new { controller = "ConfirmEmail" },
                null,
                routeHandlers);

            // Update User Profile Custom route
            config.Routes.MapHttpRoute(
                "UpdateUserProfile",
                "Update/User/{AuthenticationToken}/",
                new { controller = "UpdateUserProfile" },
                null,
                routeHandlers);

            // Archive User route
            config.Routes.MapHttpRoute(
                "ArchiveUserAccount",
                "User/Archive/{AuthenticationToken}/",
                new { controller = "ArchiveUserAccount" },
                null,
                routeHandlers);

            // Ad Viewed route
            config.Routes.MapHttpRoute(
                "AdViewed",
                "Ad/Viewed/{AuthenticationToken}/",
                new { controller = "AdViewed" },
                null,
                routeHandlers);

            // Ad Viewed route
            config.Routes.MapHttpRoute(
                "ApproveSurvey",
                "Survey/Approve/{AuthenticationToken}/",
                new { controller = "ApproveSurvey" },
                null,
                routeHandlers);

            // Get Ads route
            config.Routes.MapHttpRoute(
                "GetAdsForApi",
                "GetAdsForApi/{AuthenticationToken}/",
                new { controller = "GetAdsForApi" },
                null,
                null);

            // Get AudienceAdCampaign route
            config.Routes.MapHttpRoute(
                "GetAudienceAdCampaignForApi",
                "GetAudienceAdCampaignForApi/{AuthenticationToken}/",
                new { controller = "GetAudienceAdCampaignForApi" },
                null,
                null);

            // Get Audience Survey route
            config.Routes.MapHttpRoute(
                "GetAudienceSurveyForApi",
                "GetAudienceSurveyForApi/{AuthenticationToken}/",
                new { controller = "GetAudienceSurveyForApi" },
                null,
                null);

            // Get Profile Question route
            config.Routes.MapHttpRoute(
                "GetProfileQuestionForApi",
                "GetProfileQuestionForApi/{AuthenticationToken}/",
                new { controller = "GetProfileQuestionForApi" },
                null,
                null);

            // Get Surveys route
            config.Routes.MapHttpRoute(
                "GetSurveysForApi",
                "GetSurveysForApi/{AuthenticationToken}/",
                new { controller = "GetSurveysForApi" },
                null,
                null);

            // Update Question Answer route
            config.Routes.MapHttpRoute(
                "UpdateQuestionAnswerForApi",
                "UpdateQuestionAnswerForApi/{AuthenticationToken}/",
                new { controller = "UpdateQuestionAnswerForApi" },
                null,
                null);

            // Update User Profile Custom route
            config.Routes.MapHttpRoute(
                "UpdateUserProfileImage",
                "Update/User/{AuthenticationToken}/ProfileImage/",
                new { controller = "UpdateUserProfileImage" },
                null,
                null);

            // Get Products (Ads, Surveys, Questions) Custom route
            config.Routes.MapHttpRoute(
                "Products",
                "Products/{AuthenticationToken}/",
                new { controller = "Products" },
                null,
                null);

            // Get Users Balance Custom route
            config.Routes.MapHttpRoute(
                "UserBalanceInquiry",
                "Balance/Inquire/{AuthenticationToken}/",
                new { controller = "UserBalanceInquiry" },
                null,
                null);

            // Get Education Custom route
            config.Routes.MapHttpRoute(
                "Education",
                "Education/{AuthenticationToken}/",
                new { controller = "Education" },
                null,
                null);

            // Get Industry Custom route
            config.Routes.MapHttpRoute(
                "Industry",
                "Industry/{AuthenticationToken}/",
                new { controller = "Industry" },
                null,
                null);
            // Get Industry Custom route
            config.Routes.MapHttpRoute(
                "GenerateSMS",
                "GenerateSMS/{AuthenticationToken}/",
                new { controller = "GenerateSMS" },
                null,
                null);

            // Get Industry Custom route
            config.Routes.MapHttpRoute(
                "BuyItEmail",
                "BuyItEmail/{AuthenticationToken}/",
                new { controller = "BuyIt" },
                null,
                null);
            // Get Industry Custom route
            config.Routes.MapHttpRoute(
                "GetCoupons",
                "GetCoupons/{AuthenticationToken}/",
                new { controller = "GetCoupon" },
                null,
                null);
           
            //
            config.Routes.MapHttpRoute(
              "GetAllCoupons",
              "GetAllCoupons/{AuthenticationToken}/",
              new { controller = "GetCouponAndCategories" },
              null,
              null);
            // Get transaction Custom route
            config.Routes.MapHttpRoute(
                "UserTransactionInquiry",
                "Balance/Transactions/{AuthenticationToken}/",
                new { controller = "Statement" },
                null,
                null);
            // Get coupon categories route
            config.Routes.MapHttpRoute(
                "CouponCategories",
                "CouponCategories/{AuthenticationToken}/",
                new { controller = "CouponCategory" },
                null,
                null);
            config.Routes.MapHttpRoute(
               "InviteUserByEmail",
               "InviteUserByEmail/{AuthenticationToken}/",
               new { controller = "InviteUser" },
               null,
               null);
           
            config.Routes.MapHttpRoute(
            "SearchCoupons",
            "SearchCoupons/",
            new { controller = "SearchCoupons" },
            null,
            routeHandlers);


            config.Routes.MapHttpRoute(
         "DealDecrement",
         "DealDecrement/",
         new { controller = "DealDecrement" },
         null,
         routeHandlers);


            config.Routes.MapHttpRoute(
               "UserFavouriteCoupon",
               "UserFavouriteCoupon/{AuthenticationToken}/",
               new { controller = "UserFavouriteCoupon" },
               null,
               null);

            config.Routes.MapHttpRoute(
            "IsValidCoupon",
            "IsValidCoupon/{AuthenticationToken}/",
            new { controller = "GenerateCoupon" },
            null,
            null);



              config.Routes.MapHttpRoute(
            "GetCouponById",
            "GetCouponById/",
            new { controller = "GetCouponById" },
            null,
            routeHandlers);

            
            
              config.Routes.MapHttpRoute(
            "GetCouponByCompanyId",
            "GetCouponByCompanyId/",
            new { controller = "GetCouponByCompanyId" },
            null,
            routeHandlers);




              config.Routes.MapHttpRoute(
            "UserPurchasedCoupon",
            "UserPurchasedCoupon/{AuthenticationToken}/",
            new { controller = "UserPurchasedCoupon" },
            null,
            routeHandlers);

              config.Routes.MapHttpRoute(
         "UserCashout",
         "UserCashout/{AuthenticationToken}/",
         new { controller = "UserCashout" },
         null,
         routeHandlers);

        config.Routes.MapHttpRoute(
         "UserFeedback",
         "UserFeedback/{AuthenticationToken}/",
         new { controller = "UserFeedback" },
         null,
         routeHandlers);

            
              config.Routes.MapHttpRoute(
            "RedeemPurchasedCoupon",
            "RedeemPurchasedCoupon/{AuthenticationToken}/",
            new { controller = "RedeemPurchasedCoupon" },
            null,
            routeHandlers);


           config.Routes.MapHttpRoute(
            "ReferUsers",
            "ReferUsers/{AuthenticationToken}/",
            new { controller = "ReferUsers" },
            null,
            routeHandlers);


           config.Routes.MapHttpRoute(
      "GetRandomGame",
      "GetRandomGame/{AuthenticationToken}/",
      new { controller = "GetRandomGame" },
      null,
      routeHandlers);


               config.Routes.MapHttpRoute(
      "SurveySharingGroup",
      "SurveySharingGroup/{AuthenticationToken}/",
      new { controller = "SurveySharingGroup" },
      null,
      null);


    config.Routes.MapHttpRoute(
      "SharedSurveyQuestion",
      "SharedSurveyQuestion/{AuthenticationToken}/",
      new { controller = "SharedSurveyQuestion" },
      null,
      null);

              config.Routes.MapHttpRoute(
      "Notifications",
      "Notifications/{AuthenticationToken}/",
      new { controller = "Notifications" },
      null,
      null);

              config.Routes.MapHttpRoute(
      "Country",
      "Country/{AuthenticationToken}/",
      new { controller = "Country" },
      null,
      null);

              config.Routes.MapHttpRoute(
    "CouponRatingReview",
    "CouponRatingReview/{AuthenticationToken}/",
    new { controller = "CouponRatingReview" },
    null,
    null);


              config.Routes.MapHttpRoute(
                "UserCouponCategoryClick",
                "UserCouponCategoryClick/{AuthenticationToken}/",
                new { controller = "UserCouponCategoryClick" },
                null,
                null);

             config.Routes.MapHttpRoute(
                "UserProfileQuestions",
                "UserProfileQuestions/{AuthenticationToken}/",
                new { controller = "UserProfileQuestions" },
                null,
                null);


            
            


           //config.Routes.MapHttpRoute(
           //"CouponApproval",
           //"CouponApproval/{AuthenticationToken}/",
           //new { controller = "CouponApproval" },
           //null,
           //routeHandlers);
            

            
        }
    }
}