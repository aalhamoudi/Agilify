using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;

namespace AgilifyService.Models
{
    public class Item : EntityData
    {
        public string Name { get; set; }
        public Member Owner { get; set; }
    }
}