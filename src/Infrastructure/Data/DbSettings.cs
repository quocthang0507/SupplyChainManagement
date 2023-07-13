﻿namespace Infrastructure.Data
{
    public class DbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        #region Collections

        public string UserProfilesCollectionName { get; set; } = null!;

        public string RolesCollectionName { get; set; } = null!;

        public string ApplicationUsersCollectionName { get; set; } = null!;

        public string UnitOfMeasuresCollectionName { get; set; } = null!;

        public string FarmTypesCollectionName { get; set; } = null!;

        public string FarmsCollectionName { get; set; } = null!;

        public string PhotoperiodismCollectionName { get; set; } = null!;
        #endregion
    }
}
