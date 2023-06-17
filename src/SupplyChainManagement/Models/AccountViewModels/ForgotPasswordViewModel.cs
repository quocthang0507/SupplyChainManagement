using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Địa chỉ email")]
        public string Email { get; set; }
    }
}
