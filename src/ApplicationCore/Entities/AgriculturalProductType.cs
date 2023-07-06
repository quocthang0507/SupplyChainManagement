using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public string ProductTypeName { get; set; }

        public string Description { get; set; }
    }
}
