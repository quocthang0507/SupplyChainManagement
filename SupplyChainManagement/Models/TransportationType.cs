using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    public class TransportationType
    {
        public int TransportationTypeId { get; set; }
        [Required]
        public string TransportationTypeName { get; set; }
        public string Description { get; set; }
    }
}
