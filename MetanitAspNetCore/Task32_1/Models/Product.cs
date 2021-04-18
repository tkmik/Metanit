using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task32_1.Models
{
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "Model")]
        public string Name { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "Price")]
        public int Price { get; set; }
        public string ImageId { get; set; }
        public bool HasImage()
        {
            return !String.IsNullOrWhiteSpace(ImageId);
        }
    }
}
