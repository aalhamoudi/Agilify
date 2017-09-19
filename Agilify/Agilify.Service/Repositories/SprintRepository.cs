using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Models;
using AgilifyService.Services;

namespace AgilifyService.Repositories
{
    public class SprintRepository : ItemRepository<Sprint>
    {
        public override Sprint Delete(Sprint item)
        {
            if (Set.Find(item.Id) == null)
                return item;
            foreach (var workItem in item.WorkItems.ToList())
            {
                RepositoryManager.WorkItemRepository.Delete(workItem);
            }
            return base.Delete(item);
        }
    }
}