using System;
namespace teachBackend.Models
{
    //这个是内存里的数据库模型
    public class Online
    {
        public long Id { get; set; }

        public long TeachRecordId { get; set; }
        public string Class { get; set; }
        public long PersonalId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public long SignalRId { get; set; }
    }
}
