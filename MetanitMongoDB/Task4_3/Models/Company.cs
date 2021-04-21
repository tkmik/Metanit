using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_1.Models
{
    [BsonIgnoreExtraElements]
    class Company
    {
        public string Name { get; set; }
    }
}
