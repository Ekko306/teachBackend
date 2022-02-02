using teachBackend.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace teachBackend.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(ITeachDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var databse = client.GetDatabase(settings.DatabaseName);

            _students = databse.GetCollection<Student>("Student");
        }

        public List<Student> Get() => 
            _students.Find(student => true).ToList();


        public Student Get(string id) =>
            _students.Find<Student>(student => student.Id == id).FirstOrDefault();

        public List<Student> GetByClass(string classId) {
            var filter = Builders<Student>.Filter.Eq("Class", classId);
            var result = _students.Find(filter).ToList();
            return result;
        }

        public Student Create(Student student)
        {
            _students.InsertOne(student); // 这些都是mongodb对应的c#写法 官网有
            return student;
        }

        public void Update(string id, Student studentIn) {
            studentIn.Id = id;
            _students.ReplaceOne(student => student.Id == id, studentIn);
        }

        public void Remove(Student studentIn) =>
            _students.DeleteOne(student => student.Id == studentIn.Id);

        public void Remove(string id) =>
            _students.DeleteOne(student => student.Id == id);



        public LoginQuery CheckStudent(string email, string password)
        {
            var builder = Builders<Student>.Filter;
            var filter = builder.And(builder.Eq("Email", email), builder.Eq("Password", password));
            var result = _students.Find(filter).FirstOrDefault();
            if(result == null )
            {
                return new LoginQuery("-1", "error");
            }
            else
            {
                return new LoginQuery(result.Id, "student");
            }
        }
    }
}
