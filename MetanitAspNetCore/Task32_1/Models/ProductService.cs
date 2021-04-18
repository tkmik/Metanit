using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Task32_1.Models
{
    public class ProductService
    {
        IGridFSBucket gridFS;           // file repository
        IMongoCollection<Product> Products; //collection in the database
        public ProductService()
        {
            //connection string
            string connectionString = "mongodb://localhost:27017/mobilestore";
            var connection = new MongoUrlBuilder(connectionString);
            //get a client to interact with the database
            MongoClient client = new MongoClient(connectionString);
            //get access to the database
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            //get access to file repository
            gridFS = new GridFSBucket(database);
            //connect to collection Products
            Products = database.GetCollection<Product>("Products");
        }
        // get all the documents with filters
        public async Task<IEnumerable<Product>> GetProducts(int? minPrice, int? maxPrice, string name)
        {
            //filter builder
            var builder = new FilterDefinitionBuilder<Product>();
            var filter = builder.Empty; 
            // filter by name
            if (!String.IsNullOrWhiteSpace(name))
            {
                filter = filter & builder.Regex("Name", new BsonRegularExpression(name));
            }
            if (minPrice.HasValue)  //filter by min price
            {
                filter = filter & builder.Gte("Price", minPrice.Value);
            }
            if (maxPrice.HasValue)  //filter by max price
            {
                filter = filter & builder.Lte("Price", maxPrice.Value);
            }
            return await Products.Find(filter).ToListAsync();
        }

        //get one document by 'id'
        public async Task<Product> GetProduct(string id)
        {
            return await Products.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        //add document
        public async Task Create(Product p)
        {
            await Products.InsertOneAsync(p);
        }
        //update document
        public async Task Update(Product p)
        {
            await Products.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(p.Id)), p);
        }
        //delete document
        public async Task Remove(string id)
        {
            await Products.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        //get image
        public async Task<byte[]> GetImage(string id)
        {
            return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }

        //save image
        public async Task StoreImage(string id, Stream imageStream, string imageName)
        {
            Product p = await GetProduct(id);
            if (p.HasImage())
            {
                //delete image if it exists before
                await gridFS.DeleteAsync(new ObjectId(p.ImageId));
            }
            //save image
            ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);
            //update data
            p.ImageId = imageId.ToString();
            var filter = Builders<Product>.Filter.Eq("_id", new ObjectId(p.Id));
            var update = Builders<Product>.Update.Set("ImageId", p.ImageId);
            await Products.UpdateOneAsync(filter, update);
        }
    }
}
