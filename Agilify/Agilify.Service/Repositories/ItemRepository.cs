using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AgilifyService.Data;
using AgilifyService.Models;

namespace AgilifyService.Repositories
{
    public class ItemRepository<T> where T : Item
    {
        public AgilifyContext Context { get; set; } = new AgilifyContext();
        public DbSet<T> Set { get; set; }

        public ItemRepository()
        {
            Set = Context.Set<T>();
        }

        public virtual T Add(T item)
        {
            CheckOwner(item);
            var addedItem = Set.Add(item);
            Context.SaveChanges();
            return addedItem;
        }

        public virtual T Delete(string itemId)
        {
            var foundItem = Set.Find(itemId);

            if (foundItem == null)
                return foundItem;
            else
            {
                var removedItem = Set.Remove(foundItem);
                Context.SaveChanges();
                return removedItem;
            }
           
        }

        public virtual T Delete(T item)
        {
            var foundItem = Set.Find(item.Id);

            if (foundItem == null)
                return foundItem;
            else
            {
                var removedItem = Set.Remove(foundItem);
                Context.SaveChanges();
                return removedItem;
            }

        }

        public Item CheckOwner(Item item)
        {
            var owner = Context.Members.Find(item.Owner.Id);
            if (owner != null)
            {
                Context.Entry(owner).State = EntityState.Unchanged;
                item.Owner = owner;
            }
            return item;
        }
    }
}