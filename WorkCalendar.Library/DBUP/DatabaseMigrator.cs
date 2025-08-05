using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbUp.SqlServer;

namespace WorkCalendar.Library.DBUP
{
    public class DatabaseMigrator
    {
        public static void Run(IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionString"];

            var executingAssembly = typeof(DatabaseMigrator).Assembly.Location;
            FileInfo fi = new FileInfo(executingAssembly);

            var executingPath = Path.Combine(fi.DirectoryName, "DBUP", "SCRIPTS");

              var upgrader =  DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(executingPath)
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
        }


    }
}
