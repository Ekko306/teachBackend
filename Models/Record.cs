using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace teachBackend.Models
{
    public class Record
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("time")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time { get; set; }

        [BsonElement("introduction")]
        public string Introduction { get; set; }
        [BsonElement("end")]
        public bool End { get; set; }
        [BsonElement("chat_content")]
        public string Chat_content { get; set; }
        [BsonElement("images")]
        public List<string> Images { get; set; }

        [BsonElement("teacherId")]
        [BsonRepresentation(BsonType.ObjectId)]

        public string TeacherId { get; set; }
        [BsonElement("signalRId")]
        public string SignalRId { get; set; }
        [BsonElement("class")]
        public string Class { get; set; }
    }
}
