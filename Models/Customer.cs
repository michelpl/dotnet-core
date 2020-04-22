using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace customercrud.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        //Firstname 
        [Required(ErrorMessage = "Required field")]
        [MaxLength(60, ErrorMessage = "This field should have from 3 to 60 characters")]
        [MinLength(3, ErrorMessage = "This field should have from 3 to 60 characters")]
        public string Firstname { get; set; }

        //Lastname 
        [Required(ErrorMessage = "Required field")]
        [MaxLength(60, ErrorMessage = "This field should have from 3 to 60 characters")]
        [MinLength(3, ErrorMessage = "This field should have from 3 to 60 characters")]
        public string Lastname { get; set; }
    }
}