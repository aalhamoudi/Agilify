using Agilify.Models;
using Agilify.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agilify.Helpers
{
    public interface IMessagingClient
    {
        ChatMessageViewModel ChatViewModel { get; set; }
        HubConnection Connection { get; set; }
        IHubProxy Proxy { get; set; }

        void SignalR();
        void Broadcast(ChatMessage message);
        void OnMessage(ChatMessage message);
    }
}
