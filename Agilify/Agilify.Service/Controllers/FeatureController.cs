using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Models;

namespace AgilifyService.Controllers
{
    public class FeatureController : ItemController<Feature>
    {
        public IQueryable<Feature> GetByMember(string memberId)
        {
            var items = new List<Feature>();
            var member = AgilifyContext.Members.Find(memberId);

            if (member == null)
                return null;

            foreach (var team in member.Teams)
            {
                foreach (var project in team.Projects)
                {
                    foreach (var epic in project.Epics)
                    {
                        items.AddRange(epic.Features);
                    }
                }
            }

            return items.AsQueryable();
        }
    }
}