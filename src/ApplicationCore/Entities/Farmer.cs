using MongoDbGenericRepository.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nông dân
    /// </summary>
    [CollectionName("Farmers")]
    public class Farmer : UserProfile
    {
    }
}
