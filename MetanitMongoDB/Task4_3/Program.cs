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
        static async Task Main(string[] args)
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

            //Console.WriteLine(person1.ToJson());
            //Console.WriteLine(person2.ToJson());


            //BsonClassMap.RegisterClassMap<Person>(map => 
            //{
            //    map.AutoMap();
            //    map.MapMember(p => p.Name).SetElementName("name");
            //});

            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            //client.UseDefaultConnectionString();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");

            //collection.InsertOne(document);
            //collection.InsertMany(new[] { document, person3 });
            var collection2 = database.GetCollection<Person>("people");
            //collection2.InsertOne(person1);
            await FindDocs();
            Console.WriteLine();
            await GroupDoc();
            Console.WriteLine();
            await UpdateDoc();
            Console.ReadLine();

        }

        private static async Task FindDocs()
        {
            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            var filter = new BsonDocument("$or", new BsonArray 
            {
                new BsonDocument("Age", new BsonDocument("$lte", 30)),
                //new BsonDocument("Name", "Mikita")
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
        private static async Task GroupDoc()
        {
            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            var people = await collection.Aggregate()
                .Group(new BsonDocument { {"_id", "$Age" }, { "count", new BsonDocument("$sum", 1) } })
                .ToListAsync();
            foreach (var person in people)
            {
                Console.WriteLine($"{person.GetValue("_id")}, {person.GetValue("count")}");
            }

            var collection2 = database.GetCollection<Person>("people");
            var people2 = await collection2.Aggregate()
                .Match(new BsonDocument { { "Name", "person1" }, { "Company.Name", "None" } })
                .ToListAsync();
            foreach (Person person in people2)
            {
                Console.WriteLine($"{person.Name}({person.Company?.Name})");
            }
        }

        private static async Task UpdateDoc()
        {
            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            var result = await collection.ReplaceOneAsync(new BsonDocument("Name", "Test"),
                new BsonDocument 
                {
                    { "Name", "New Test" },
                    { "Age", 40 },
                    { "Languages", new BsonArray(new []{ "English", "German" })},
                    { "Company",
                        new BsonDocument
                        {
                            { "Name", "Apple" }
                        }
                    }                    
                },
                new ReplaceOptions { IsUpsert = true });
            Console.WriteLine($"Found {result.MatchedCount}, updated {result.ModifiedCount}");
            var people = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
            var result2 = await collection.UpdateOneAsync(
                new BsonDocument("Name", "Tom"),
                new BsonDocument("$set", new BsonDocument("Age", 28)));

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("Name", "Test") | builder.Eq("Age", 30);
            var update = Builders<BsonDocument>.Update.Set("Age", 33).CurrentDate("LastModified");
            var result3 = await collection.UpdateOneAsync(filter, update);
            var result4 = await collection.UpdateManyAsync(filter, update);

        }

        private async Task DeleteDoc()
        {
            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("Name", "New Test");
            await collection.DeleteOneAsync(filter);

            var people = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var person in people)
            {
                Console.WriteLine($"{person}");
            }
        }
        private static async Task BulkOperation()
        {
            var client = new MongoClient(ConnectionToMongo.UseDefaultConnectionString());
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            var filter = Builders<BsonDocument>.Filter.Eq("Company.Name", "Google");
            var update = Builders<BsonDocument>.Update.Set("Company.Name", "Google Inc.");
            var newDoc = new BsonDocument
            {
                {"Name", "Bill"},
                {"Age", 32},
                {"Languages", 
                    new BsonArray{"english", "german"}},
                {"Company", 
                    new BsonDocument{
                        {"Name","Google"}
                    }
                }
            };
            var result = await collection.BulkWriteAsync(new WriteModel<BsonDocument>[]
            {
                new DeleteOneModel<BsonDocument>(new BsonDocument("Name", "Robert")),
                new UpdateManyModel<BsonDocument>(filter, update),
                new InsertOneModel<BsonDocument>(newDoc)
            });
            Console.WriteLine($"deleted {result.DeletedCount}/ " +
                $"added {result.InsertedCount}/ " +
                $"updated {result.ModifiedCount}");
        }
    }
}
