using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Bộ phân loại cho thực vật
    /// </summary>
    public class Taxonomy
    {
        /// <summary>
        /// Họ
        /// </summary>
        public class FamilyClass
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string? Id { get; set; }

            public string Name { get; set; }
        }

        /// <summary>
        /// Chi
        /// </summary>
        public class GenusClass
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string? Id { get; set; }

            public string Name { get; set; }
        }

        /// <summary>
        /// Loài
        /// </summary>
        public class SpeciesClass
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string? Id { get; set; }

            public string Name { get; set; }
        }

        /// <summary>
        /// Quang chu kỳ
        /// </summary>
        [CollectionName("Photoperiodism")]
        public class Photoperiodism
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string? Id { get; set; }

            [Display(Name = "Tên")]
            public string Name { get; set; }

            [Display(Name = "Tên tiếng Anh")]
            public string EnglishName { get; set; }

            [Display(Name = "Mô tả")]
            public string? Description { get; set; }

        }

        [Display(Name = "Họ")]
        public FamilyClass Family { get; set; }

        [Display(Name = "Chi")]
        public GenusClass Genus { get; set; }

        [Display(Name = "Loài")]
        public SpeciesClass Species { get; set; }



        public Taxonomy()
        {
            Family = new();
            Genus = new();
            Species = new();
        }

        public Taxonomy(string family, string genus, string species)
        {
            Family = new() { Name = family };
            Genus = new() { Name = genus };
            Species = new() { Name = species };
        }
    }
}
