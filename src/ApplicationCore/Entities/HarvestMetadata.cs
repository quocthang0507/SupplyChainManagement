using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class HarvestMetadata
    {
        [Display(Name = "Ngày thu hoạch")]
        public DateTime Date { get; set; }

        [Display(Name = "Sản lượng")]
        public decimal Amount { get; set; } = 0;

        [Display(Name = "Đơn vị")]
        public string Unit { get; set; }
    }
}
