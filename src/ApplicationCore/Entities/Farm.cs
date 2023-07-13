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
        public UserProfile? OwnerProfile { get; set; }

        ///<summary>
        /// Danh sách các thửa đất trong nông trại
        ///</summary>
        public IList<LandParcel> LandParcels { get; }

        [Display(Name = "Diện tích")]
        public decimal Area { get; set; }

        [Display(Name = "Đơn vị diện tích")]
        public string AreaUnit { get; set; }

        [Display(Name = "Metadata")]
        public Metadata? Metadata { get; set; }

        public CrudMetadata? CrudMetadata { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool IsActivated { get; set; } = true;
    }
}
