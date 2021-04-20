using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
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
    }
}
