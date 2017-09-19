using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Models;

namespace AgilifyService.Repositories
{
    public class FeatureRepository : ItemRepository<Feature>
    {
        public override Feature Delete(Feature item)
        {
            return base.Delete(item);
        }
    }
}