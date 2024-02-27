using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Hồ sơ người dùng
    /// </summary>
    [CollectionName("UserProfiles")]
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserProfileId { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string? ApplicationUserId { get; set; }

        [Display(Name = "Họ và tên đệm")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Tên")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Địa chỉ")]
        public Address Address { get; set; } = new();

        [Display(Name = "Số điện thoại")]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required]
        public DateTime Birthday { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool Activated { get; set; } = true;

        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Mật khẩu cũ")]
        public string? OldPassword { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string ProfilePicture { get; set; } = "/upload/blank-person.png";

        [Display(Name = "Họ và tên")]
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
    }
}