using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    ///<summary>
    ///Địa chỉ
    ///</summary>
    public class Address
    {
        [Display(Name = "Số nhà")]
        public string AddressNumber { get; set; }

        [Display(Name = "Xã/phường")]
        public string Commune { get; set; }

        [Display(Name = "Huyện/quận")]
        public string District { get; set; }

        [Display(Name = "Tỉnh/thành phố")]
        public string Province { get; set; }

        [Display(Name = "Mã bưu chính")]
        public string ZipCode { get; set; }

        public GeoCoordinate GeoCoordinate { get; set; }
    }
}
