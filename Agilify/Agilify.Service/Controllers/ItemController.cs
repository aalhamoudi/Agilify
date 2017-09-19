using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using AgilifyService.Data;
using AgilifyService.Models;

namespace AgilifyService.Controllers
{
    public class ItemController<T> : TableController<T> where T : Item
    {
        public AgilifyContext AgilifyContext { get; set; } = new AgilifyContext();
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new EntityDomainManager<T>(AgilifyContext, Request);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Query();
        }

        public virtual SingleResult<T> Get(string id)
        {
            return Lookup(id);
        }

        public virtual Task<T> Patch(string id, Delta<T> patch)
        {
            CheckOwner(patch.GetEntity());
            return UpdateAsync(id, patch);
        }

        public virtual async Task<IHttpActionResult> Post(T item)
        {
            CheckOwner(item);
            T current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public virtual Task Delete(string id)
        {
            return DeleteAsync(id);
        }

        [NonAction]
        public Item CheckOwner(Item item)
        {
            var owner = AgilifyContext.Members.Find(item.Owner.Id);
            if (owner != null)
            {
                AgilifyContext.Entry(owner).State = EntityState.Unchanged;
                item.Owner = owner;
            }
            return item;
        }
    }
}