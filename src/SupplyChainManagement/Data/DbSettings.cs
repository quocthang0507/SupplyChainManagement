namespace SupplyChainManagement.Data
{
    public class DbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserProfilesCollectionName { get; set; } = null!;    
    }
}
