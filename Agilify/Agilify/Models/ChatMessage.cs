using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilify.Models
{
    public class ChatMessage : Item
    {
        public string Username { get; set; }
        public string Message { get; set; }
    }
}