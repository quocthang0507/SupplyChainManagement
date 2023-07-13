using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        public FamilyClass Family { get; set; }
        public GenusClass Genus { get; set; }
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
