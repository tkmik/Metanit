using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2_1.Models
{
    //[Table("People")]
    [Index("Name", "Age")]
    class User
    {
        //[Column("user_id")]
        //[Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        //[NotMapped]// way2
        public string Position { get; set; }
    }
}
