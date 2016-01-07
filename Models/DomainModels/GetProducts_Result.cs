namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Get Products Result Domain Model
    /// </summary>
    public class GetProducts_Result
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public long? Weightage { get; set; }
        public int? TotalItems { get; set; }
    }
}
