using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Models;
using AgilifyService.Services;

namespace AgilifyService.Repositories
{
    public class EpicRepository : ItemRepository<Epic>
    {
        public override Epic Delete(Epic item)
        {
            foreach (var feature in item.Features.ToList())
                RepositoryManager.FeatureRepository.Delete(feature);
            return base.Delete(item);
        }
    }
}