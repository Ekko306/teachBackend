namespace teachBackend.Models
{
    public class TeachDbSettings: ITeachDbSettings
    {
        public string TeacherCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface ITeachDbSettings
    {
        string TeacherCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
