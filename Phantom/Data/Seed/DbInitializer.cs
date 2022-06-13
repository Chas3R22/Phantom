using Microsoft.AspNetCore.Identity;

namespace Phantom.Data.Seed
{
    public class DbInitializer
    {
        private static readonly string adminRoleId;
        private static readonly string userRoleId;

        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.AddRange(
                    new IdentityRole
                    {
                        Id = adminRoleId,
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Id = userRoleId,
                        Name = "User",
                        NormalizedName = "USER"
                    }
                );
            }
        }
    }
}
