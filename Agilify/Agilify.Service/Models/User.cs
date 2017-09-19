using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace AgilifyService.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime JoinDate { get; set; }

        public static implicit operator Member(User user)
        {
            return new Member
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Image = user.Image
            };
        }
    }

    public class UserBindingModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {6} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public static implicit operator User(UserBindingModel user)
        {
            return new User
            {
                Email = user.Email,
                UserName = user.Email,
                Name = user.Name,
                JoinDate = DateTime.Now,
                Image = user.Image
            };
        }
    }

       
    }
