using InternalSystem.UserManagement.Database.DatabaseEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Database
{
    public class UserManagementDbContext : IdentityDbContext<User,Role,int>
    {
        #region Constructor
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        {
        }
        #endregion

        #region Method Ovverrides
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Configurations for identity tables 
            base.OnModelCreating(builder);
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            #endregion
        }
        #endregion

        #region DbSet
        public DbSet<User> users { get; set; }

        #endregion
    }
}
