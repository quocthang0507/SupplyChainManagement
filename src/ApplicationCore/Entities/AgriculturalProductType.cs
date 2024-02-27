using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Loại nông sản
    /// </summary>
    public class AgriculturalProductType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductTypeId { get; set; }

        [Required]
        [DisplayName("Loại nông sản")]
        public string ProductTypeName { get; set; }

        [DisplayName("Loại nông sản (tiếng Anh)")]
        public string EnglishProductTypeName { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }
    }
}
