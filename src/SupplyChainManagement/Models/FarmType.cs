using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    // / <summary>
    // / Loại nông trại
    // / </summary>
    public class FarmType
    {
        public int FarmTypeId { get; set; }
        [Required]
        public string FarmTypeName { get; set; }
        public string Description { get; set; }

    }
}
