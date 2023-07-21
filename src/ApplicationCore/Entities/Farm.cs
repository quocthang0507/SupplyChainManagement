using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    ///<summary>
    /// Nông trại
    ///</summary>
    [CollectionName("Farms")]
    public class Farm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string TypeId { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Địa chỉ")]
        public Address? Address { get; set; } = new();

        /// <summary>
        /// Chủ sở hữu
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string OwnerProfileId { get; set; }

        ///<summary>
        /// Danh sách các thửa đất trong nông trại
        ///</summary>
        public List<string> LandParcelIds { get; set; } = new();

        [Display(Name = "Diện tích")]
        public decimal Area { get; set; }

        [Display(Name = "Đơn vị diện tích")]
        public string AreaUnit { get; set; }

        [Display(Name = "Metadata")]
        public Metadata? Metadata { get; set; } = new();

        public CrudMetadata? CrudMetadata { get; set; } = new();

        [Display(Name = "Kích hoạt")]
        public bool IsActivated { get; set; } = true;
    }
}
