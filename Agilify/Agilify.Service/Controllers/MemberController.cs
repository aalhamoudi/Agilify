using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Principal;
using System.Web.Http.Cors;
using AgilifyService.Attributes;
using Microsoft.Azure.Mobile.Server;
using AgilifyService.Data;
using AgilifyService.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Config;

namespace AgilifyService.Controllers
{
    [MobileAppController]
    public class MemberController : ItemController<Member>
    {
        public AgilifyContext AgilifyContext { get; set; } = new AgilifyContext();

        [LoadProperty("Teams")]
        public override IQueryable<Member> GetAll()
        {
            return AgilifyContext.Members.AsQueryable();

        }

        [LoadProperty("Teams")]
        public IQueryable<Member> GetByMember(string memberId)
        {
            var items = new List<Member>();
            var member = AgilifyContext.Members.Find(memberId);

            if (member == null)
                return null;

            foreach (var team in member.Teams)
            {
                items.AddRange(team.Members);
            }

            return items.AsQueryable();
        }

        public override async Task<IHttpActionResult> Post(Member item)
        {
            var member = await DomainManager.InsertAsync(item);
            return Ok();
        }

        //[LoadProperty("Teams")]
        //public Member Get(string memberId)
        //{
        //    return AgilifyContext.Members.Find(memberId);
        //}
    }
}