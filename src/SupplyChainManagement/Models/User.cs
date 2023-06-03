using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Họ và tên")]
        public string UserName { get; set; }

        [Display(Name = "Loại")]
        public string UserTypeId { get; set; }

        public Address Address { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool Activated { get; set; }
    }
}
