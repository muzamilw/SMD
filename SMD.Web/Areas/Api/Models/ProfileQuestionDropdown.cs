
namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Profile Question Drop Down
    /// </summary>
    public class ProfileQuestionDropdown
    {
        public int PqId { get; set; }
        public string Question { get; set; }
        public int? CompanyId { get; set; }
    }
}