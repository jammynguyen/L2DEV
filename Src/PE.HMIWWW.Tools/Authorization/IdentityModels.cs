using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using SMF.DbEntity;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PE.HMIWWW.Core.Authorization
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
		[ForeignKey("LanguageId")]
		public virtual SMFLanguage Language { get; set; }
		public short HMIViewOrientation { get; set; }
		public long LanguageId { get; set; } 

	}
	public class SMFLanguage
	{
		[Key]
		public long LanguageId { get; set; } // LanguageId (Primary key) 
		public string LanguageCode { get; set; } // LanguageCode (length: 10) 
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
				: base("DefaultConnection", throwIfV1Schema: false)
		{

		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

      modelBuilder.HasDefaultSchema("smf");
			modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Id");
			modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Id");
			modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles");
			modelBuilder.Entity<SMFLanguage>().ToTable("Languages").Property(p => p.LanguageId).HasColumnName("LanguageId");
		}
	}
}
