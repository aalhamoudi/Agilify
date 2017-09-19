using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agilify.Helpers
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string provider);
        Task<bool> Refresh();
    }
}
