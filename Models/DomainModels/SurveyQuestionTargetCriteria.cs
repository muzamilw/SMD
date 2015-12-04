﻿namespace SMD.Models.DomainModels
{
    /// <summary>
    /// SurveyQuestionTargetCriteria Domain Model
    /// </summary>
    public class SurveyQuestionTargetCriteria
    {
        public long Id { get; set; }
        public long? SqId { get; set; }
        public int? Type { get; set; }
        public int? PqId { get; set; }
        public int? PqAnswerId { get; set; }
        public long? LinkedSqId { get; set; }
        public int? LinkedSqAnswer { get; set; }
        public bool? IncludeorExclude { get; set; }

        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual ProfileQuestionAnswer ProfileQuestionAnswer { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public virtual SurveyQuestion LinkedSurveyQuestion { get; set; }
    }
}