using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }
        [Required]
        public string VendorName { get; set; }
        [Display(Name = "Vendor Type")]
        public int VendorTypeId { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
    }
}
