using Agilify.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Agilify.ViewModels
{
    public class ChatMessageViewModel
    {
        public ObservableCollection<ChatMessage> Messages { get; set; } = new ObservableCollection<ChatMessage>();
    }
}
