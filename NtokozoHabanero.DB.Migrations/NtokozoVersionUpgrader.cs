using System;
using Habanero.Base;
using Habanero.DB;

namespace NtokozoHabanero.DB.Migrations
{
    public class NtokozoVersionUpgrader : IApplicationVersionUpgrader
    {
        private readonly MissingDBFallBackOptions _missingDBFallBackOptions;
        public IDatabaseConfig DatabaseConfig { get; private set; }

        public NtokozoVersionUpgrader(DatabaseConfig databaseConfig)
        {
            if (databaseConfig == null) throw new ArgumentNullException("databaseConfig");
            DatabaseConfig = databaseConfig;
            _missingDBFallBackOptions = MissingDBFallBackOptions.CreateDatabase;
        }

        public void Upgrade()
        {
            UpdateDatabase(DatabaseConfig);
        }

        private void UpdateDatabase(IDatabaseConfig databaseConfig)
        {
            var migrationsRunner = new MigrationsRunner<NtokozoMigrations>(databaseConfig, _missingDBFallBackOptions);
            migrationsRunner.MigrateToLatest();
        }
    }

    internal class NtokozoMigrations
    {
    }
}
