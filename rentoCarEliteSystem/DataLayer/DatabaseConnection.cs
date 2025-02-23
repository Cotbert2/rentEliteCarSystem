using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using EntityLayer;
using Microsoft.Extensions.Configuration;


namespace DataLayer
{
    public class DatabaseConnection
    {

        public SqlConnection cn;

        public DatabaseConnection()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),
                "appsettings.json"
                ));
            var root = builder.Build();
            var connectionString = root.GetConnectionString("cn");
            cn = new SqlConnection(connectionString);
        }

        public SqlConnection getConnection()
        {
            //singleton for connectino
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }
            return cn;
        }
    }
}
