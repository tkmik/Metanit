using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Task4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
           
            //GetDatabaseNames(client).GetAwaiter();
            GetCollectionsNames(client).GetAwaiter();
            BsonDocument doc = new BsonDocument()
            {
                { "Name", "Mikita" },
                { "Surname", "Tkach" },
                { "Age", new BsonInt32(25) },
                { "Company",
                    new BsonDocument
                    {
                        { "Name", "None" },
                        { "Year", new BsonInt32(2021) }
                    }
                },
                { "Countries", new BsonArray(new[] { "Belarus", "Russia" })}
            };
            Console.WriteLine(doc);

            Console.ReadLine();
        }

        private static async Task GetDatabaseNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var databaseDocuments = await cursor.ToListAsync();
                foreach (var databaseDocument in databaseDocuments)
                {
                    Console.WriteLine(databaseDocument["name"]);
                }
                Console.WriteLine();
            }
        }
        private static async Task GetCollectionsNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var dbs = await cursor.ToListAsync();
                foreach (var db in dbs)
                {
                    Console.WriteLine($"{db["name"]} contains the following collections:");

                    IMongoDatabase database = client.GetDatabase(db["name"].ToString());
                    using (var collCursor = await database.ListCollectionsAsync())
                    {
                        var colls = await collCursor.ToListAsync();
                        foreach (var col in colls)
                        {
                            Console.WriteLine($"\t{col["name"]}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
