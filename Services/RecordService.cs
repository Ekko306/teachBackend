using teachBackend.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System;

namespace teachBackend.Services
{
    public class RecordService
    {
        private readonly IMongoCollection<Record> _records;

        public RecordService(ITeachDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var databse = client.GetDatabase(settings.DatabaseName);

            _records = databse.GetCollection<Record>("Record");
        }

        public Record Get(string id) =>
            _records.Find<Record>(record => record.Id == id).FirstOrDefault();

        public Record Create(Record record)
        {
            _records.InsertOne(record); // 这些都是mongodb对应的c#写法 官网有
            return record;
        }

        public void Update(string id, Record recordIn)
        {
            recordIn.Id = id;
            _records.ReplaceOne(record => record.Id == id, recordIn);
        }

        public List<Record> GetByTeacher(string teacherId)
        {
            var filter = Builders<Record>.Filter.Eq("TeacherId", teacherId);
            var result = _records.Find(filter).ToList();
            return result;
        }
    }
}
