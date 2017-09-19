using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Principal;
using AgilifyService.Attributes;
using Microsoft.Azure.Mobile.Server;
using AgilifyService.Data;
using AgilifyService.Models;
using AgilifyService.Services;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;

namespace AgilifyService.Controllers
{
    public class ProjectController : ItemController<Project>
    {
        [LoadProperty("Team")]
        [LoadProperty("Owner")]
        [LoadProperty("Epics")]
        [LoadProperty("Sprints")]
        public override IQueryable<Project> GetAll()
        {
            return base.GetAll();

        }

        [LoadProperty("Team")]
        [LoadProperty("Owner")]
        [LoadProperty("Epics")]
        [LoadProperty("Sprints")]
        public IQueryable<Project> GetByMember(string memberId)
        {
            var member = AgilifyContext.Members.Find(memberId);
            return AgilifyContext.Projects.Where(p => p.Team.Members.Select(m => m.Id).Contains(member.Id));
        }

        [LoadProperty("Team")]
        [LoadProperty("Owner")]
        [LoadProperty("Epics")]
        [LoadProperty("Sprints")]
        public IQueryable<Project> GetByTeam(string teamId)
        {
            var team = AgilifyContext.Teams.Find(teamId);
            return team?.Projects.AsQueryable();
        }

        [LoadProperty("Team")]
        [LoadProperty("Owner")]
        [LoadProperty("Epics")]
        [LoadProperty("Sprints")]
        public override SingleResult<Project> Get(string id)
        {
            return base.Get(id);
        }

        

        public override async Task<IHttpActionResult> Post(Project item)
        {
            CheckOwner(item);

            var team = AgilifyContext.Teams.Find(item.Team.Id);

            if (team != null)
            {
                AgilifyContext.Entry(team).State = EntityState.Unchanged;
                
            }
            else
            {
                RepositoryManager.TeamRepository.Add(item.Team);
            }

            item.Team = null;
            item.TeamId = team.Id;

            var current = AgilifyContext.Projects.Add(item);
            AgilifyContext.SaveChanges();

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public override Task<Project> Patch(string id, Delta<Project> patch)
        {
            var project = AgilifyContext.Projects.Find(id);
            AgilifyContext.Projects.Remove(project);
            AgilifyContext.SaveChanges();

            patch.Patch(project);
            CheckOwner(project);
            AgilifyContext.Projects.Add(project);
            AgilifyContext.SaveChanges();
            return AgilifyContext.Projects.FindAsync(id);
        }
    }
}