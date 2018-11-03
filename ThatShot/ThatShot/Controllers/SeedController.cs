using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ThatShot.Models;

namespace ThatShot.Controllers
{
    public class SeedController : Controller
    {
        private RoleManager<TSRole> rolemanager;
        private UserManager<TSUser> usermanager;

        public SeedController(RoleManager<TSRole> rolemanager, UserManager<TSUser> usermanager)
        {
            this.rolemanager = rolemanager;
            this.usermanager = usermanager;
        }




            
            public async Task<IActionResult> CreateAdmin()
        {

         


            var role = new TSRole();
            role.Name = "Admin";
            await rolemanager.CreateAsync(role);

            var user = new TSUser() { Email = "admin2@admin.com" };
            user.UserName = "admin2@admin.com";
            string password = "test1234";
           var result = await usermanager.CreateAsync(user, password);

           await  usermanager.AddToRoleAsync(user, "Admin");


            return View();
        }
    }
}