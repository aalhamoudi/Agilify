using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilifyService.Models
{
    public class ChatMessage : Item
    {
        public string Username { get; set; }
        public string Message { get; set; }
    }
}