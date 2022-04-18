using Microsoft.AspNetCore.Identity;

namespace SpeakingInBits.Models
{
    public class IdentityHelper
    {
        public const string Instructor = "Instructor";

        public const string Student = "Student";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();
            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();

            // If no user present, make the default user
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count;
            if (numUsers == 0) // if no users are in the specified role, create it
            {
                var defaultUser = new IdentityUser()
                {
                    Email = "instructor@speakinginbits.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(defaultUser, "Programming01#"); // ***** Never Hard code passwords!!!! this is just for testing purposes!!! *********

                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
