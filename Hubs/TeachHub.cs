using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public static List<string> imageStrings = new List<string>();

        public struct ToJsonMy
        {
            public string groupName { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
            public string user { get; set; }
            public string time { get; set; }   
        }

        // 下面定义客户端向服务端流传输 服务端全部接收后向所有客户端发送 和上面SendImageInGroup成对比
        public async Task UploadImageStream(IAsyncEnumerable<string> stream)
        {
            await foreach (var item in stream)
            {
                imageStrings.Add(item.ToString());
                char[] MyChar = { '#', ',' };

                // 不光要直接中转客户端数据 还需要拆分出来 groupName 只传递到group里
                if (item.ToString().Contains("#"))
                {
                    string finalString = string.Join("", imageStrings.ToArray()).TrimEnd(MyChar);
                    string[] splitStrings = finalString.Split("$");
                    ToJsonMy temp = JsonConvert.DeserializeObject<ToJsonMy>(splitStrings[1]);

                    await Clients.Group(temp.groupName).ReceiveImageStream(finalString);
                    imageStrings = new List<string>();
                }
            }
        }

        //  因为同步的图片变大了，保存历史图片也要变大 照葫芦画瓢  和上面SendSavedImageInGroup成对比
        public async Task UploadSavedImageStream(IAsyncEnumerable<string> stream)
        {
            await foreach (var item in stream)
            {
                imageStrings.Add(item.ToString());
                char[] MyChar = { '#', ',' };

                // 不光要直接中转客户端数据 还需要拆分出来 groupName 只传递到group里
                if (item.ToString().Contains("#"))
                {
                    string finalString = string.Join("", imageStrings.ToArray()).TrimEnd(MyChar);
                    string[] splitStrings = finalString.Split("$");
                    ToJsonMy temp = JsonConvert.DeserializeObject<ToJsonMy>(splitStrings[1]);

                    await Clients.Group(temp.groupName).ReceiveSavedImageStream(finalString);
                    imageStrings = new List<string>();
                }
            }
        }
    }
}
