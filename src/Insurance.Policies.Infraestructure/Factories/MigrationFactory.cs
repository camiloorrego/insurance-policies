using Evolve.Dialect;
using Evolve.Migration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Insurance.Policies.Infraestructure.Factories
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
                OutOfOrder = true
            };

            return evolve;
        }
    }
}
