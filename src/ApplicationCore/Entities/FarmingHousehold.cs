using MongoDbGenericRepository.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nông hộ
    /// </summary>
    [CollectionName("FarmingHouseholds")]
    public class FarmingHousehold : UserProfile
    {
    }
}
