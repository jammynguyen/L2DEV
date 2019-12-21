using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using SMF.DbEntity;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace PE.HMIWWW.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
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

			modelBuilder.Entity<IdentityUser>().ToTable("SMFUsers").Property(p => p.Id).HasColumnName("Id");
			modelBuilder.Entity<ApplicationUser>().ToTable("SMFUsers").Property(p => p.Id).HasColumnName("Id");
			modelBuilder.Entity<IdentityUserRole>().ToTable("SMFUserRoles");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("SMFUserLogins");
			modelBuilder.Entity<IdentityUserClaim>().ToTable("SMFUserClaims");
			modelBuilder.Entity<IdentityRole>().ToTable("SMFRoles");
			modelBuilder.Entity<SMFLanguage>().ToTable("SMFLanguages").Property(p => p.LanguageId).HasColumnName("LanguageId");
		}
	}
}