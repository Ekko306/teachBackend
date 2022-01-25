using System;
namespace teachBackend.Models
{
    //这个是内存里的数据库模型
    public class Online
    {
        public long Id { get; set; }

        public string TeachRecordId { get; set; }
        public string Class { get; set; }
        public string PersonId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string SignalRId { get; set; }
    }
}
