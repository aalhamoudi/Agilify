using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using AgilifyService.Attributes;
using Microsoft.Azure.Mobile.Server;
using AgilifyService.Data;
using AgilifyService.Models;

namespace AgilifyService.Controllers
{
    public class EpicController : ItemController<Epic>
    {
        [LoadProperty("Features")]
        public IQueryable<Epic> GetByMember(string memberId)
        {
            var items = new List<Epic>();
            var member = AgilifyContext.Members.Find(memberId);

            if (member == null)
                return null;

            foreach (var team in member.Teams)
            {
                foreach (var project in team.Projects)
                {
                    items.AddRange(project.Epics);
                }
            }

            return items.AsQueryable();
        }
    }
}