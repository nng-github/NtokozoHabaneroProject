using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Habanero.Base;
using Habanero.Console;
using Habanero.DB;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.Migrations;
using LendingLibrary.Habanero.Web.Bootstrap;
using BORegistry = Habanero.BO.BORegistry;

namespace LendingLibrary.Habanero.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocContainer.Setup();

            GlobalRegistry.ApplicationName = "LendingLibrary.Habanero";
            GlobalRegistry.ApplicationVersion = "1.32.05 - 2016.03.24";
            var ntokozoHabaneroApp = new HabaneroAppConsole(GlobalRegistry.ApplicationName, GlobalRegistry.ApplicationVersion);
            ntokozoHabaneroApp.LoadClassDefs = false;
            var databaseConfig = DatabaseConfig.ReadFromConfigFile();

            DatabaseConnection.CurrentConnection = databaseConfig.GetDatabaseConnection();
            BORegistry.DataAccessor = new DataAccessorDB();
            BOBroker.LoadClassDefs();

            var connectionString = GetConnectionString();
            MigrateDatabaseWith(connectionString);
        }

        private int GetCurrentVerison()
        {
            var sql = "Select SettingValue from settings where SettingName='DATABASE_VERSION'";
            var databaseConnection = DatabaseConnection.CurrentConnection;
            var dataReader = databaseConnection.LoadDataReader(sql);
            dataReader.Read();
            var version = Convert.ToInt32(dataReader["SettingValue"].ToString());
            return version;
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        private void MigrateDatabaseWith(string connectionString)
        {
            var runner = new Migrator(connectionString);
            runner.MigrateToLatest();
        }
    }
}
