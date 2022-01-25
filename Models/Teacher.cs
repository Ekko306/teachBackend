using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace teachBackend.Models
{
    public class Teacher
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
        [BsonElement("department")]
        public Depart Department { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("home")]
        public string Home { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("avatar")]
        public string Avatar { get; set; }
        [BsonElement("class")]
        public List<string> Class { get; set; }
    }
    public class Depart
    {
        public string first { get; set; }
        public string second { get; set; }
    }
}
