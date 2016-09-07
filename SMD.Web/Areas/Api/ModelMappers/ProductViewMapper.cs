using System.Collections.Generic;
using System.Linq;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Product View Mapper
    /// </summary>
    public static class ProductViewMapper
    {
        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static ProductView CreateFrom(this GetProducts_Result source)
        {
            return new ProductView
                   {
                       ItemId = source.ItemId,
                       Description = !string.IsNullOrEmpty(source.description) ? source.description.Replace("\n", "\n").Replace("\\n", "\n") : source.description,
                       Type = source.Type,
                       ItemType = source.ItemType,
                       ItemName = source.ItemName,
                       AdClickRate = source.AdClickRate,
                       AdAnswer1 = source.AdAnswer1,
                       AdAnswer2 = source.AdAnswer2,
                       AdAnswer3 = source.AdAnswer3,
                       AdCorrectAnswer = source.AdCorrectAnswer,
                       AdImagePath = source.AdImagePath,
                       AdRewardType = source.AdRewardType,
                       AdVerifyQuestion = source.AdVerifyQuestion,
                       AdVideoLink = source.AdVideoLink,
                       AdVoucher1Description = source.AdVoucher1Description,
                       AdVoucher1Heading = source.AdVoucher1Heading,
                       AdVoucher1Value = source.AdVoucher1Value,
                       GameUrl = source.GameUrl,
                       SqLeftImagePath = source.SqLeftImagePath,
                       SqRightImagePath = source.SqRightImagePath,
                       SqLeftImagePercentage = source.SqLeftImagePercentage,
                       SqRightImagePercentage = source.SqRightImagePercentage,
                       AdvertisersLogoPath = source.AdvertisersLogoPath,
                       PqAnswers = source.PqAnswers != null ? source.PqAnswers.Select(pqa => pqa.CreateFromForProduct()).ToList() : 
                       new List<ProfileQuestionAnswerView>(),
                       BuyItButtonText = source.BuyItButtonText,
                       BuyItImageUrl = source.BuyItImageUrl,
                       BuyItLine1 = source.BuyItLine1,
                       BuyItLine2 = source.BuyItLine2,
                       BuyItLine3 = source.BuyItLine3,
                       LandingPageUrl = source.LandingPageUrl,
                       VoucherImagePath = source.VoucherImagePath,
                       VideoLink2 = source.VideoLink2,
                       IsShowVoucherSetting = source.IsShowVoucherSetting ?? false,
                       VouchersCount = source.VoucherCount.HasValue == true? source.VoucherCount.Value:0,
                       CompanyId = source.CompanyId.HasValue == true? source.CompanyId.Value:0,
                        GameId = source.GameId.HasValue == true ? source.GameId.Value:0,
                        FreeCouponID = source.FreeCouponID.HasValue == true ? source.FreeCouponID.Value :0,
                        SocialHandle = source.SocialHandle,
                        SocialHandleType = source.SocialHandleType
                       
                   };
        }

        /// <summary>
        /// Get Products Response Mapper
        /// </summary>
        public static ProductViewResponse CreateFrom(this GetProductsResponse source)
        {
            return new ProductViewResponse
                   {
                       Status = source.Status, 
                       Message = source.Message, 
                       TotalCount = source.TotalCount, 
                       Products = source.Products != null ? source.Products.Select(pd => pd.CreateFrom()) : new List<ProductView>()
                   };
        }
    }
}