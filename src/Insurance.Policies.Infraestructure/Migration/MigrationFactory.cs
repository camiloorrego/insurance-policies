using Evolve.Dialect;
using Evolve.Migration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Insurance.Policies.Infraestructure.Migration
{
    internal static class MigrationFactory
    {
        public static string resourcesFolder = Path.Combine(AppContext.BaseDirectory, "Migration/Scripts/");


        public static Evolve.Evolve Build(SqlConnection connection)

        {

            var evolve = new Evolve.Evolve(connection, null)
            {
                Command = Evolve.Configuration.CommandOptions.Migrate,
                Locations = new[] { resourcesFolder },
                //StartVersion = ParseVersion("0_0", MigrationVersion.MinVersion),
                //TargetVersion = ParseVersion("2_0_0", MigrationVersion.MaxVersion),
                OutOfOrder = true
            };

            return evolve;
        }

        private static IDbConnection CreateConnection(DBMS database, string cnnStr)
        {
            IDbConnection cnn = null;

            switch (database)
            {
                case DBMS.SQLServer:
                    cnn = new SqlConnection(cnnStr);
                    break;
                default:
                    break;
            }

            return cnn;
        }

        private static MigrationVersion ParseVersion(string version, MigrationVersion defaultIfEmpty) =>
              !string.IsNullOrEmpty(version) ? new MigrationVersion(version) : defaultIfEmpty;


    }
}
