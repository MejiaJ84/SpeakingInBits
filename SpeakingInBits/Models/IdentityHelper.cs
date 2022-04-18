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
    }
}
