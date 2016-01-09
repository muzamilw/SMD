namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Get Products Result Domain Model
    /// </summary>
// ReSharper disable InconsistentNaming
    public class GetProducts_Result
// ReSharper restore InconsistentNaming
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string Type { get; set; }
        public int? ItemType { get; set; }
        public string Description { get; set; }
        public double? AdClickRate { get; set; }
        public string AdImagePath { get; set; }
        public string AdVideoLink { get; set; }
        public string AdAnswer1 { get; set; }
        public string AdAnswer2 { get; set; }
        public string AdAnswer3 { get; set; }
        public int? AdCorrectAnswer { get; set; }
        public string AdVerifyQuestion { get; set; }
        public int? AdRewardType { get; set; }
        public string AdVoucher1Heading { get; set; }
        public string AdVoucher1Description { get; set; }
        public string AdVoucher1Value { get; set; }
        public string SqLeftImagePath { get; set; }
        public string SqRightImagePath { get; set; }
        public string GameUrl { get; set; }
        public int? PqAnswer1Id { get; set; }
        public string PqAnswer1 { get; set; }
        public int? PqAnswer2Id { get; set; }
        public string PqAnswer2 { get; set; }
        public int? PqAnswer3Id { get; set; }
        public string PqAnswer3 { get; set; }
        public long? Weightage { get; set; }
        public int? TotalItems { get; set; }

    }
}
