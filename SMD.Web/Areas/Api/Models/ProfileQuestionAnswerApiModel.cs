
namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Model For APIs
    /// </summary>
    public class ProfileQuestionAnswerApiModel
    {
        public int PqAnswerId { get; set; }
        public int? PqId { get; set; }
        public int? Type { get; set; }
        public string AnswerString { get; set; }
        public string ImagePath { get; set; }
    }
}