using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Task4_13.Models
{
    public class AppDbContext
    {
        IMongoDatabase database;
        IGridFSBucket gridFS;
        public AppDbContext()
        {
            //get connection string
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("Default");
            //get access to database
            var connetion = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            database = client.GetDatabase(connetion.DatabaseName);
            gridFS = new GridFSBucket(database);
        }
        public IMongoCollection<Computer> Computers
        { 
            get 
            {
                return database.GetCollection<Computer>("Computers");
            } 
        }
        //get elements by filtering
        public async Task<IEnumerable<Computer>> GetComputersAsync(int? year, string name)
        {
            var builder = new FilterDefinitionBuilder<Computer>();
            var filter = builder.Empty;

            if (!String.IsNullOrWhiteSpace(name))
            {
                filter = filter & builder.Regex("Name", new BsonRegularExpression(name));
            }
            if (year.HasValue)
            {
                filter = filter & builder.Eq("Year", year.Value);
            }
            return await Computers.Find(filter).ToListAsync();
        }
        //get element by id
        public async Task<Computer> GetComputerAsync(string id)
        {
            return await Computers.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        //add a document
        public async Task CreateAsync(Computer computer)
        {
            await Computers.InsertOneAsync(computer);
        }
        //update a document
        public async Task UpdateAsync(Computer computer)
        {
            await Computers.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(computer.Id)), computer);
        }
        //delete a document
        public async Task RemoveAsync(string id)
        {
            await Computers.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        //get image
        public async Task<byte[]> GetImageAsync(string id)
        {
            return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }
        //save image to database
        public async Task StoreImageAsync(string id, Stream imageStream, string imageName)
        {
            Computer computer = await GetComputerAsync(id);
            if (computer.HasImage())
            {
                await gridFS.DeleteAsync(new ObjectId(computer.ImageId));
            }
            ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);
            computer.ImageId = imageId.ToString();
            var filter = Builders<Computer>.Filter.Eq("_id", new ObjectId(computer.Id));
            var update = Builders<Computer>.Update.Set("ImageId", computer.ImageId);
            await Computers.UpdateOneAsync(filter, update);
        }
    }
}
