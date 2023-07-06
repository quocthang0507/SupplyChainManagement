using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    ///<summary>
    /// Nông trại
    ///</summary>
    public class Farm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FarmId { get; set; }

        public string FarmTypeId { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Địa chỉ")]
        public Address FarmAddress { get; set; }

        ///<summary>
        /// Danh sách các thửa đất trong nông trại
        ///</summary>
        public List<LandParcel> LandParcels { get; set; }

        [Display(Name = "Diện tích")]
        public decimal Area { get; set; }

        [Display(Name = "Đơn vị diện tích")]
        public string AreaUnit { get; set; }

        [Display(Name = "Metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        public CrudMetadata CrudMetadata { get; set; }
    }
}
