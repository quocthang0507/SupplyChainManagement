using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    public class UserType
    {
        [Required]
        public int UserTypeId { get; set; }
        [Required]
        public string UserTypeName { get; set; }
        public string Description { get; set; }
    }
}
