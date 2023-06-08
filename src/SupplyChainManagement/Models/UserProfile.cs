using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserProfileId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? ApplicationUserId { get; set; }

        [Required]
        [Display(Name = "Họ")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Tên")]
        public string LastName { get; set; }

        public Address Address { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool Activated { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string OldPassword { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string ProfilePicture { get; set; } = "/upload/blank-person.png";
    }
}