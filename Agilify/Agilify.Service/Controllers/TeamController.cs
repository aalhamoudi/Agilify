using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AgilifyService.Attributes;
using AgilifyService.Data;
using AgilifyService.Models;
using AgilifyService.Services;

namespace AgilifyService.Controllers
{
    public class TeamController : ItemController<Team>
    {
        [LoadProperty("Owner")]
        [LoadProperty("Projects")]
        [LoadProperty("Members")]
        public override IQueryable<Team> GetAll()
        {
            return base.GetAll();
        }

        [LoadProperty("Owner")]
        [LoadProperty("Projects")]
        [LoadProperty("Members")]
        public IQueryable<Team> GetByMember(string memberId)
        {
            //return AgilifyContext.Teams.Where(t => t.Members.Select(m => m.Id).Contains(memberId));
            return AgilifyContext.Members.Find(memberId)?.Teams.AsQueryable();
        }

        [LoadProperty("Owner")]
        [LoadProperty("Projects")]
        [LoadProperty("Members")]
        public override SingleResult<Team> Get(string id)
        {
            return base.Get(id);
        }

        public override async Task<IHttpActionResult> Post(Team item)
        {
            var current = RepositoryManager.TeamRepository.Add(item);

            return CreatedAtRoute("Tables", new { id = current.Id }, current);

        }

        public override Task Delete(string id)
        {

            RepositoryManager.TeamRepository.Delete(id);
            return Task.CompletedTask;

        }


    }
}