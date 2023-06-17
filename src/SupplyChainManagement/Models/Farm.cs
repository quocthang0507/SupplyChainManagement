using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SupplyChainManagement.Models
{
    ///<summary>
    ///Nông trại
    ///</summary>
    public class Farm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FarmId { get; set; }
        public string FarmTypeId { get; set; }
        public string FarmName { get; set; }
        public string Description { get; set; }
        public Address FarmAddress { get; set; }
        ///<summary>
        ///Danh sách các thửa đất trong nông trại
        ///</summary>
        public List<LandParcel> LandParcels { get; set; }
    }
}
