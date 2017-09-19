using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Models;

namespace AgilifyService.Controllers
{
    public class WorkItemController : ItemController<WorkItem>
    {
        public IQueryable<WorkItem> GetByMember(string memberId)
        {
            var items = new List<WorkItem>();
            var member = AgilifyContext.Members.Find(memberId);

            if (member == null)
                return null;

            foreach (var team in member.Teams)
            {
                foreach (var project in team.Projects)
                {
                    foreach (var sprint in project.Sprints)
                        items.AddRange(sprint.WorkItems);
                }
            }

            return items.AsQueryable();
        }

    }
}