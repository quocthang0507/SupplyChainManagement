namespace SupplyChainManagement.Data
{
    public class DbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        #region Collections

        public string UserProfilesCollectionName { get; set; } = null!;

        public string RolesCollectionName { get; set; } = null!;

        public string ApplicationUsersCollectionName { get; set; } = null!;

        #endregion
    }
}
