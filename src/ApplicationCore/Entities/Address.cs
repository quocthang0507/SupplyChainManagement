using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    ///<summary>
    /// Địa chỉ
    ///</summary>
    public class Address
    {
        [Display(Name = "Số nhà")]
        public string AddressNumber { get; set; }

        [Display(Name = "Đường")]
        public string Street { get; set; }

        [Display(Name = "Xã/phường")]
        public string Commune { get; set; }

        [Display(Name = "Huyện/quận")]
        public string District { get; set; }

        [Display(Name = "Tỉnh/thành phố")]
        public string Province { get; set; }

        [Display(Name = "Mã bưu chính")]
        public string ZipCode { get; set; }

        public GeoCoordinate? GeoCoordinate { get; set; }

        public override string ToString()
        {
            string[] arr = { AddressNumber, Street, Commune, District, Province };
            return string.Join(", ", arr.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
