﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nông sản
    /// </summary>
    [CollectionName("AgriculturalProducts")]
    public class AgriculturalProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductTypeId { get; set; }

        [Required]
        [Display(Name = "Tên nông sản")]
        public string ProductName { get; set; }

        [Display(Name = "Mã nông sản")]
        public string ProductCode { get; set; }

        [Display(Name = "Mã vạch nông sản")]
        public string Barcode { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh đại diện")]
        public string ProductImageUrl { get; set; }

        [Display(Name = "UOM")]
        public int UnitOfMeasureId { get; set; }
    }
}
