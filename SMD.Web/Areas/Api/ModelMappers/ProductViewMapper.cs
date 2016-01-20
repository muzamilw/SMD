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
                       Description = source.Description,
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
                       PqAnswers = source.PqAnswers != null ? source.PqAnswers.Select(pqa => pqa.CreateFromDropdown()).ToList() : 
                       new List<ProfileQuestionAnswerDropdown>()
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