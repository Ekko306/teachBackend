using teachBackend.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace teachBackend.Services
{
    public class TeacherService
    {
        private readonly IMongoCollection<Teacher> _teachers;
        private readonly IMongoCollection<Student> _students;

        public TeacherService(ITeachDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var databse = client.GetDatabase(settings.DatabaseName);

            _teachers = databse.GetCollection<Teacher>("Teacher");
            _students = databse.GetCollection<Student>("Student");
        }

        // teacher
        public List<Teacher> Get() =>
            _teachers.Find(teacher => true).ToList();
        
           
        public Teacher Get(string id) =>
            _teachers.Find<Teacher>(teacher => teacher.Id == id).FirstOrDefault();

        public List<Teacher> GetByClass(string classId)
        {
            var filter = Builders<Teacher>.Filter.Eq("Class", classId);
            var result = _teachers.Find(filter).ToList();
            return result;
        }

        public Teacher Create(Teacher teacher)
        {
            _teachers.InsertOne(teacher); // 这些都是mongodb对应的c#写法 官网有
            return teacher;
        }

        public void Update(string id, Teacher teacherIn) {
            teacherIn.Id = id;
            _teachers.ReplaceOne(teacher => teacher.Id == id, teacherIn);
        }

        public void Remove(Teacher teacherIn) =>
            _teachers.DeleteOne(teacher => teacher.Id == teacherIn.Id);

        public void Remove(string id) =>
            _teachers.DeleteOne(teacher => teacher.Id == id);


        public LoginQuery CheckTeacher(string email, string password)
        {
            var builder = Builders<Teacher>.Filter;
            var filter = builder.And(builder.Eq("Email", email), builder.Eq("Password", password));
            var result = _teachers.Find(filter).FirstOrDefault(); ;
            if (result == null)
            {
                return new LoginQuery("-1", "error");
            }
            else
            {
                return new LoginQuery(result.Id, "teacher");
            }
        }

     
    }
}
