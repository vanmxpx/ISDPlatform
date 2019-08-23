using Cooper.Models;
using System.Threading.Tasks;

namespace Cooper.SignalR
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(Message message);
        Task BroadcastChat(Chat chat);
    }
}
