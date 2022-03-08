using System;
using System.Threading.Tasks;

namespace teachBackend.Hubs
{
    public interface ITeachClient
    {
        Task ReceiveChat(string user, string message, string time);
        Task ReceiveImage(string user, string image, string time);   // area3 实时预览的
        Task ReceiveSavedImage(string user, string image, string time);    // area4 保存历史数据的
        Task ReceiveStatus(string groupName, string status);    // 几个状态 allowEdit unAllowEdit End 老师给学生发
        Task ReceiveImageStream(string stream);    // 流只有一种入参
        Task ReceiveSavedImageStream(string stream);    // 流只有一种入参
    }
}
