using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_1.Models
{
    [BsonIgnoreExtraElements]
    class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [BsonIgnoreIfDefault]
        public int Age { get; set; }
        [BsonIgnoreIfNull]
        public Company Company { get; set; }
        public List<string> Languages { get; set; }
    }
}
