using System;
namespace teachBackend.Models
{
    public class LoginQuery
    {
        // 这个比较简单 不是和数据库一一对应的 自定义的查询类 小写id
        public string Id { get; set; }
        public string Kind { get; set; }

        public LoginQuery(string idIn, string kindIn)
        {
            Id = idIn;
            Kind = kindIn;
        }
    }
}
