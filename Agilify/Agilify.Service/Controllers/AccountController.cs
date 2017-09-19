using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using AgilifyService.Accounts;
using AgilifyService.Data;
using AgilifyService.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables;

namespace AgilifyService.Controllers
{
    [MobileAppController]
    [RoutePrefix("api/account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        public AgilifyContext AgilifyContext { get; set; } = new AgilifyContext();
        public EntityDomainManager<Member> DomainManager { get; set; }

        protected AgilifyUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<AgilifyUserManager>();
            }
        }

        protected IList<User> Users
        {
            get
            {
                return UserManager?.Users
                    .Include(u => u.Claims)
                    .Include(u => u.Logins)
                    .Include(u => u.Roles)
                    .ToList();
            }
        }

        public ProviderManager ProviderManager { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new EntityDomainManager<Member>(AgilifyContext, Request);
        }

        [Route("users")]
        [HttpGet]
        public IQueryable<User> GetAll()
        {
            return Users.AsQueryable();
        }

        [Route("user")]
        public async Task<Member> Get()
        {
            ProviderManager = new ProviderManager(Request, this.User);
            var user = await ProviderManager.Get();

            if (user == null)
            {
                return null;
            }

            User foundUser = await UserManager.FindByEmailAsync(user.Email);

            if (foundUser != null)
            {
                if (!AgilifyContext.Members.Select(m => m.Id).Contains(foundUser.Id))
                {
                    return await DomainManager.InsertAsync(foundUser);
                }
                else
                {
                    return AgilifyContext.Members.Find(foundUser.Id);
                }
            }
            else
            {
                return await Create(user);
            }

        }

        [Route("user/{id:guid}")]
        public async Task<Member> GetById(string id)
        {
            var model = await UserManager.FindByIdAsync(id);

            return model;
        }

        //[Route("user/{name}")]
        //public async Task<IHttpActionResult> GetByName(string name)
        //{
        //    var user = await UserManager.FindByNameAsync(name);

        //    if (user != null)
        //    {
        //        return Ok(UserManager.Create(user));
        //    }

        //    return NotFound();

        //}

        [Route("user/{email}")]
        public async Task<Member> GetByEmail(string email)
        {
            var member = await UserManager.FindByEmailAsync(email);

            return member;

        }

        [Route("create")]
        [HttpPost]
        [ActionName("Create")]
        public async Task<Member> Create(User user)
        {
            IdentityResult result = await UserManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return null;
            }

            var createdUser = await UserManager.FindByEmailAsync(user.Email);
            return await DomainManager.InsertAsync(createdUser);
        }
    }
}
