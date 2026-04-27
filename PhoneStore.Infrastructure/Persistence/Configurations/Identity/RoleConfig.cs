using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhoneStore.Infrastructure.Persistence.Configurations.Identity
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public static readonly string AdminRoleId =
            "a1111111-1111-1111-1111-111111111111";

        public static readonly string UserRoleId =
            "a2222222-2222-2222-2222-222222222222";




        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(

                new IdentityRole
                {
                    Id = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "c1111111-1111-1111-1111-111111111111"
                },

                new IdentityRole
                {
                    Id = UserRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "c2222222-2222-2222-2222-222222222222"
                }
            );
        }
    }
}
