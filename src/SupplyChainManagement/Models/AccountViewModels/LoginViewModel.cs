using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Địa chỉ email không được bỏ trống")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhớ tài khoản đăng nhập này?")]
        public bool RememberMe { get; set; }
    }
}
