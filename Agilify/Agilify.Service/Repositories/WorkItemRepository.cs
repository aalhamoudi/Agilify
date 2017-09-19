using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Models;

namespace AgilifyService.Repositories
{
    public class WorkItemRepository : ItemRepository<WorkItem>
    {
        public override WorkItem Delete(WorkItem item)
        {
            return base.Delete(item);
        }
    }
}