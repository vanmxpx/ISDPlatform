using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Cooper.Hubs {
    public class GroupChatHub : Hub<IGroupChatClient>
    {
    }
}