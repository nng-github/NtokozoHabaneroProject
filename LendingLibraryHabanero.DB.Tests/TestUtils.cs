using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Habanero.Base;
using Habanero.BO;
using Habanero.DB;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.Web.Bootstrap.QuotePro.Bootstrap;
using BORegistry = Habanero.BO.BORegistry;

namespace LendingLibrary.Habanero.DB.Tests
{
    public static class TestUtils
    {
        private static IWindsorContainer _container;
        public static IWindsorContainer Container { get { return _container; } }
        public static void SetupFixture()
        {
            BORegistry.DataAccessor = new DataAccessorInMemory();
            ConfigureContainer();
            BOBroker.LoadClassDefs();
        }

        public static void OverrideContainerWithInstance<T>(T obj) where T : class
        {
            Container.Register(Component.For<T>().Instance(obj).OverridesExistingRegistration());
        }

        public static void ConfigureContainer()
        {
            var diBootstrapper = new WindsorBootstrapper(isUnitTestEnvironment: true);
            _container = diBootstrapper.BootstrapForTests();
        }

        /*public static void SetupDatabase()
        {
            BORegistry.DataAccessor = new DataAccessorDB(GetDatabaseConnection());
            DatabaseConnection.CurrentConnection.ExecuteRawSql(
                "Delete from Quote Where TemplateType is null and TemplateName is null");
            var quoteProVersionUpgrader = new NtokozoVersionUpgrader(GetDatabaseConfig());
            quoteProVersionUpgrader.Upgrade();

        }*/

        private static void SetupDbConnection()
        {
            var databaseConfig = GetDatabaseConfig();

            DatabaseConnection.CurrentConnection = new DatabaseConnectionFactory().CreateConnection(databaseConfig);
            DatabaseConnection.CurrentConnection.GetConnection();
        }


        private static DatabaseConfig GetDatabaseConfig()
        {
            return new DatabaseConfig("SqlServer", "localhost", "quotepro_test", GetIntegrationTestingUserName(), GetIntegrationTestingPassword(), "");
        }

        private static string GetIntegrationTestingUserName()
        {
            return Environment.GetEnvironmentVariable("SQLSERVER_INTEGRATION_TESTING_LOGIN") ?? "sa";
        }

        private static string GetIntegrationTestingPassword()
        {
            return Environment.GetEnvironmentVariable("SQLSERVER_INTEGRATION_TESTING_PASSWORD") ?? "sa";
        }
        
        public static IDatabaseConnection GetDatabaseConnection()
        {
            SetupDbConnection();
            return DatabaseConnection.CurrentConnection;
        }
    }
    public static class TestWindsorExtensions
    {
        public static ComponentRegistration<T> OverridesExistingRegistration<T>(this ComponentRegistration<T> componentRegistration) where T : class
        {
            return componentRegistration
                                .Named(Guid.NewGuid().ToString())
                                .IsDefault();
        }
    }
}