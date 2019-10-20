using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace PatiroTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPut("[action]")]
        public void Initialize()
        {
            //To reset DB, delete it and run PMS: update-database
            using (var scope = serviceScopeFactory.CreateScope())
            {
                //Quick and dirty build Employees
                roleManager.CreateAsync(new IdentityRole("Employee")).Wait();

                var employee = new IdentityUser("employee@gmail.com")
                {
                    Email = "employee@gmail.com"
                };
                userManager.CreateAsync(employee, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(employee, "Employee").Wait();

                var clinic1creator = new IdentityUser("clinic1creator@gmail.com")
                {
                    Email = "clinic1creator@gmail.com"
                };
                userManager.CreateAsync(clinic1creator, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(clinic1creator, "Employee").Wait();

                //Quick and dirty build Partner
                roleManager.CreateAsync(new IdentityRole("Partner")).Wait();
                var partner = new IdentityUser("partner@gmail.com")
                {
                    Email = "partner@gmail.com"
                };
                userManager.CreateAsync(partner, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(partner, "Partner").Wait();

                var clinic2member = new IdentityUser("clinic2member@gmail.com")
                {
                    Email = "clinic2member@gmail.com"
                };
                userManager.CreateAsync(clinic2member, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(clinic2member, "Partner").Wait();

                //Quick and dirty build Admin
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                var admin = new IdentityUser("admin@gmail.com")
                {
                    Email = "admin@gmail.com"
                };
                userManager.CreateAsync(admin, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(admin, "Admin").Wait();
            }
        }

        [HttpPost("[action]")]
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Authenticate([FromBody]AuthInfo authInfo) =>
            await signInManager.PasswordSignInAsync(authInfo.Email, authInfo.Password, true, false);

        [HttpGet("[action]")]
        public async Task SignOut() => await signInManager.SignOutAsync();

        public class AuthInfo
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}