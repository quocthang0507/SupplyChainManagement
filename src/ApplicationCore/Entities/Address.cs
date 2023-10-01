using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    public class Ward
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("codename")]
        public string Codename { get; set; }

        [JsonPropertyName("short_codename")]
        public string ShortCodename { get; set; }

        [JsonPropertyName("division_type")]
        public string DivisionType { get; set; }
    }

    public class District
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("codename")]
        public string Codename { get; set; }

        [JsonPropertyName("short_codename")]
        public string ShortCodename { get; set; }

        [JsonPropertyName("division_type")]
        public string DivisionType { get; set; }

        public List<Ward> Wards { get; set; } = new List<Ward> { };
    }

    public class Province
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("phone_code")]
        public int PhoneCode { get; set; }

        [JsonPropertyName("codename")]
        public string Codename { get; set; }

        [JsonPropertyName("division_type")]
        public string DivisionType { get; set; }

        public List<District> Districts { get; set; } = new List<District> { };
    }
}
