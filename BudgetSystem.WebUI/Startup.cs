using BudgetSystem.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetSystem.WebUI.Startup))]
namespace BudgetSystem.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login  
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup I am creating first Admin Role and creating a default Admin User   
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                 

                var user = new ApplicationUser
                {
                    UserName = "ptbvibar",
                    Email = "pbvibar@gmail.com"
                };

                string userPWD = "12345";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Processor role   
            if (!roleManager.RoleExists("Processor"))
            {
                var role = new IdentityRole();
                role.Name = "Processor";
                roleManager.Create(role);

            }

            //// creating Creating Employee role   
            if (!roleManager.RoleExists("Approver"))
            {
                var role = new IdentityRole();
                role.Name = "Approver";
                roleManager.Create(role);

            }
        }
    }
}
