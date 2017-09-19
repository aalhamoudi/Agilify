using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AgilifyService.Data;
using AgilifyService.Models;
using AgilifyService.Services;

namespace AgilifyService.Repositories
{
    public class TeamRepository : ItemRepository<Team>
    {
        public override Team Add(Team item)
        {
            CheckOwner(item);

            var members = new List<Member>();

            foreach (var member in item.Members)
            {
                if (member.Id.Equals(item.Owner.Id))
                    members.Add(item.Owner);

                else
                {
                    var dbMember = Context.Members.Find(member.Id);

                    if (dbMember != null)
                    {
                        Context.Entry(dbMember).State = EntityState.Modified;
                        members.Add(dbMember);
                    }
                    else
                        members.Add(member);
                }

            }

            item.Members = members;

            Team current = Context.Teams.Add(item);

            foreach (var member in current.Members)
                member.Teams.Add(current);

            Context.SaveChanges();

            return current;
        }

        public override Team Delete(string itemId)
        {
            var team = Context.Teams.Find(itemId);

            foreach (var member in team?.Members.ToList())
            {
                var dbMember = Context.Members.Find(member.Id);
                
                dbMember?.Teams.Remove(team);
            }

            foreach (var project in team?.Projects?.ToList())
            {
                RepositoryManager.ProjectRepository.Delete(project);
            }

            return base.Delete(itemId);
        }
    }
}