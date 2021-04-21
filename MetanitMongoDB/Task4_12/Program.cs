using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Task4_12
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            var gridFS = new GridFSBucket(database);
            using (Stream fs = new FileStream("D:\\s20fe.jpg", FileMode.Open))
            {
                ObjectId id = await gridFS.UploadFromStreamAsync("s20fe.jpg", fs);
            }
            using (Stream fs = new FileStream("D:\\s20fe_new.jpg", FileMode.OpenOrCreate))
            {
                await gridFS.DownloadToStreamByNameAsync("s20fe.jpg", fs);
            }

            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Filename, "s20fe.jpg");
            var fileInfos = await gridFS.FindAsync(filter);
            var fileInfo = fileInfos.FirstOrDefault();
            Console.WriteLine(fileInfo.Id);
            await gridFS.DeleteAsync(fileInfo.Id);
        }
    }
}
