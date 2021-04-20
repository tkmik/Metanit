using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Task4_1.Models;

namespace Task4_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person { Name = "person1", Surname = "Tkach", Age = 25 };
            person1.Company = new Company { Name = "None" };

            BsonDocument document = new BsonDocument()
            {
                { "Name", "person2" },
                { "Surname", "Tkach" },
                { "Age", new BsonInt32(25) },
                { "Company", 
                    new BsonDocument() {                    
                        { "Name", "None" }
                    } 
                }
            };
            Person person2 = BsonSerializer.Deserialize<Person>(document);
            BsonDocument person3 = person1.ToBsonDocument();

            Console.WriteLine(person1.ToJson());
            Console.WriteLine(person2.ToJson());


            //BsonClassMap.RegisterClassMap<Person>(map => 
            //{
            //    map.AutoMap();
            //    map.MapMember(p => p.Name).SetElementName("name");
            //});

            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            //client.UseDefaultConnectionString();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");

            collection.InsertOne(document);
            //collection.InsertMany(new[] { document, person3 });
            var collection2 = database.GetCollection<Person>("people");
            collection2.InsertOne(person1);
            Console.ReadLine();
        }

        private static async Task FindDocs()
        {
            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            var filter = new BsonDocument("$or", new BsonArray 
            {
                new BsonDocument("Age", new BsonDocument("$gte", 30)),
                new BsonDocument("Name", "Mikita")
            });
            var builder = Builders<BsonDocument>.Filter;
            var filter2 = builder.Gte("Age", 30) | builder.Eq("Name", "Mikita");
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var people = cursor.Current;
                    foreach (var doc in people)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }
            var people2 = await collection.Find(filter).Skip(2).Limit(3).ToListAsync();

            var collection2 = database.GetCollection<Person>("people");
            var people3 = await collection2.Find(p => p.Name == "Mikita" && p.Age >= 30).SortBy(p => p.Name).ToListAsync();
            var builder2 = Builders<Person>.Filter.Where(p => p.Name == "Mikita" && p.Age >= 30);

        }
    }
}
