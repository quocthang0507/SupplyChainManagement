using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    ///<summary>
    /// Loại nông trại
    ///</summary>
    public class FarmType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FarmTypeId { get; set; }

        [Required]
        [DisplayName("Loại nông trại")]
        public string FarmTypeName { get; set; }

        [DisplayName("Loại nông trại (tiếng Anh)")]
        public string EnglishTypeName { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

    }
}
