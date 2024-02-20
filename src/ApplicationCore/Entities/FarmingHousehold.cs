using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nông hộ
    /// </summary>
    public class FarmingHousehold
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FarmingHouseholdId { get; set; }
    }
}
