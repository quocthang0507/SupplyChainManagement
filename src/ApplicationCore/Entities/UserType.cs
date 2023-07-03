using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class UserType
    {
        [Required]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserTypeId { get; set; }
        [Required]
        public string UserTypeName { get; set; }
        public string Description { get; set; }
    }
}
