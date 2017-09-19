using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Models;
using AgilifyService.Services;

namespace AgilifyService.Repositories
{
    public class ProjectRepository : ItemRepository<Project>
    {
        public override Project Delete(Project item)
        {
            foreach (var epic in item.Epics.ToList())
                RepositoryManager.EpicRepository.Delete(epic);

            foreach (var sprint in item.Sprints.ToList())
                RepositoryManager.SprintRepository.Delete(sprint);

            return base.Delete(item);
        }
    }
}