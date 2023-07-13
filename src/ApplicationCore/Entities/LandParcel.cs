using MongoDbGenericRepository.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Thửa đất
    /// </summary>
    [CollectionName("LandParcels")]
    public class LandParcel : Farm
    {
        public Taxonomy Taxonomy { get; set; } = new();
    }
}
