namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Profile Question Answer Api Model
    /// </summary>
    public class ProfileQuestionAnswerView
    {
        public int PqAnswerId { get; set; }
        public string Answer { get; set; }
        public int? PqId { get; set; }
        public int? LinkedQuestion1Id { get; set; }
        public int? LinkedQuestion2Id { get; set; }
        public int? Type { get; set; }
        public int? SortOrder { get; set; }
        public string ImagePath { get; set; }
    }
}