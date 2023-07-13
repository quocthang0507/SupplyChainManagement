using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class CrudMetadata
    {
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdatedTime { get; set; }

        [Display(Name = "Ngày xóa")]
        public DateTime DeletedTime { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Người tạo")]
        public string CreatedUserProfileId { get; set; } = null!;

    }
}
