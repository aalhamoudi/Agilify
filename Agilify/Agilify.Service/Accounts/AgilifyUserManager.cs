using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilifyService.Data;
using AgilifyService.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AgilifyService.Accounts
{
    public class AgilifyUserManager : UserManager<User>
    {
        public AgilifyUserManager(IUserStore<User> store) : base(store)
        {
            UserValidator = new UserValidator<User>(this)
            {

            };


        }


        public static AgilifyUserManager Create(IdentityFactoryOptions<AgilifyUserManager> options, IOwinContext context)
        {
            var dbContext = context.Get<AgilifyContext>();
            var userManager = new AgilifyUserManager(new UserStore<User>(dbContext));
            return userManager;

        }

        public Member Create(User user)
        {
            return new Member
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

    }
}