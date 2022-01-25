using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace teachBackend.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("introduction")]
        public string Introduction { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("avatar")]
        public string Avatar { get; set; }
        [BsonElement("class")]
        public string Class { get; set; }
    }
}
