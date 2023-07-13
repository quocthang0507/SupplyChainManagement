using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Thửa đất
    /// </summary>
    [CollectionName("LandParcels")]
    public class LandParcel : Farm
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string FarmTypeId { get; set; }

        public PlantingMetadata PlantingMetadata { get; set; } = new();

        public List<HarvestMetadata> HarvestHistory { get; set; } = new();
    }
}
