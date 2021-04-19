using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.IO;

namespace Task4_3
{
    public static class MongoClientExtensions
    {
        public static string UseDefaultConnectionString(this MongoClient client)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("Default");
            return connectionString;
        }
    }
}
