
namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Domain Model Tax
    /// </summary>
    public class Tax
    {
        public int TaxId { get; set; }

        public string TaxName { get; set; }

        public double? TaxValue { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

    }
}
