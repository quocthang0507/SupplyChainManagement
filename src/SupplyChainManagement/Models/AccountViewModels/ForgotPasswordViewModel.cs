using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
