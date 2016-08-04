
namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Profile Question Answer Web Model 
    /// </summary>
    public class ProfileQuestionAnswer
    {
        public int PqAnswerId { get; set; }
        public int? PqId { get; set; }
        public int? Type { get; set; }
        public string AnswerString { get; set; }
        public string ImagePath { get; set; }
        public int? SortOrder { get; set; }
        public int? LinkedQuestion1Id { get; set; }
        public int? LinkedQuestion2Id { get; set; }
        public int? LinkedQuestion3Id { get; set; }
        public int? LinkedQuestion4Id { get; set; }
        public int? LinkedQuestion5Id { get; set; }
        public int? LinkedQuestion6Id { get; set; }
    }
}