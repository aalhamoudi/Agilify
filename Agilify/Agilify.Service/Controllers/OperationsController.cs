using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using AgilifyService.Data;
using AgilifyService.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace AgilifyService
{
    [MobileAppController]
    [RoutePrefix("api/operations")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OperationsController : ApiController
    {
        public AgilifyContext AgilifyContext { get; set; } = new AgilifyContext();

        [Route("addmember")]
        [HttpGet]
        public bool AddMember(string teamId, string email)
        {
            var team = AgilifyContext.Teams.Find(teamId);
            Member member = null;

            if (AgilifyContext.Members.Any() && AgilifyContext.Members.Where(m => m.Email.Equals(email)).Any())
                member = AgilifyContext.Members.First(m => m.Email.Equals(email));

            if (team == null || member == null)
                return false;

            if (!team.Members.Contains(member))
                team.Members.Add(member);
            if (!member.Teams.Contains(team))
                member.Teams.Add(team);

            AgilifyContext.Entry(team).State = EntityState.Modified;
            AgilifyContext.Entry(member).State = EntityState.Modified;
            AgilifyContext.SaveChanges();
            return true;
        }

        [Route("deletemember")]
        [HttpDelete]
        public bool DeleteMember(string teamId, string email)
        {
            var team = AgilifyContext.Teams.Find(teamId);
            Member member = null;

            if (AgilifyContext.Members.Any() && AgilifyContext.Members.Where(m => m.Email.Equals(email)).Any())
                member = AgilifyContext.Members.First(m => m.Email.Equals(email));

            if (team == null || member == null)
                return false;

            if (team.Members.Contains(member))
                team.Members.Remove(member);
            if (member.Teams.Contains(team))
                member.Teams.Remove(team);

            AgilifyContext.Entry(team).State = EntityState.Modified;
            AgilifyContext.Entry(member).State = EntityState.Modified;
            AgilifyContext.SaveChanges();
            return true;
        }
    }
}