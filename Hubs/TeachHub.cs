using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace teachBackend.Hubs
{
    public class TeachHub : Hub<ITeachClient> // 这里有user和string两个参数 这里就是传递中心 客户端是 自己传递user和message 然后接收随便乱搞 方便使用而已
    {
        // user 传入名字 groupName 传入老师的id
        public async Task AddTeacherToGroup(string user, string groupName, string time)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveChat("系统通知：", $"{user}创建了本次教学，组名{groupName}", time);
        }

        public async Task AddStudentToGroup(string user, string groupName, string time)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveChat("系统通知：", $"新同学 {user} 加入了本次教学！", time);
        }

        public async Task SendChat(string user, string message, string time)
        {
            await Clients.All.ReceiveChat(user, message, time);
        }

        public async Task SendChatInGroup(string groupName, string user, string message, string time)
        {
            await Clients.Group(groupName).ReceiveChat(user, message, time);
        }

        public async Task SendImage(string user, string image, string time)
        {
            await Clients.All.ReceiveImage(user, image, time);
        }

        public async Task SendImageInGroup(string groupName, string user, string image, string time)
        {
            await Clients.Group(groupName).ReceiveImage(user, image, time);
        }

        public async Task SendSavedImageInGroup(string groupName, string user, string image, string time)
        {
            await Clients.Group(groupName).ReceiveSavedImage(user, image, time);
        }

        public async Task SendStatusInGroup(string groupName, string status) {   
            // permission是字符串 allow 或者 unallow
            await Clients.Group(groupName).ReceiveStatus(groupName, status);
        }
    }
}
