using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Repositories;

namespace AgilifyService.Services
{
    public class RepositoryManager
    {
        public static TeamRepository TeamRepository { get; set; } = new TeamRepository();
        public static ProjectRepository ProjectRepository { get; set; } = new ProjectRepository();
        public static EpicRepository EpicRepository { get; set; } = new EpicRepository();
        public static SprintRepository SprintRepository { get; set; } = new SprintRepository();
        public static FeatureRepository FeatureRepository { get; set; } = new FeatureRepository();
        public static WorkItemRepository WorkItemRepository { get; set; } = new WorkItemRepository();
    }
}