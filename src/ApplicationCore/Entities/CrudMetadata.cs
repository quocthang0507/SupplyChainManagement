using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class CrudMetadata
    {
        [Display(Name = "Ngày tạo")]
        public DateTimeOffset CreatedTime { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTimeOffset UpdatedTime { get; set; }

        [Display(Name = "Ngày xóa")]
        public DateTimeOffset DeletedTime { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Người tạo")]
        public string CreatedUserProfileId { get; set; } = null!;

    }
}
