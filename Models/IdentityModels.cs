using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mahamesh.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        ////public IDbSet<NewsModel> News { get; set; }
         public IDbSet<DistrictList> DistrictList { get; set; }

        public IDbSet<DistMaster> DistMaster { get; set; }
        public IDbSet<TalMaster> TalMaster { get; set; }
        public IDbSet<VillageMaster> VillageMaster { get; set; }

        public IDbSet<CasteUnderNTC> CasteUnderNTC { get; set; }
        public IDbSet<NoOfSheepMaster> NoOfSheepMaster { get; set; }
        public IDbSet<CrippledMaster> CrippledMaster { get; set; }

        ////public IDbSet<PressInformationModel> PressInformation { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<Mahamesh.Models.MediaFolders> MediaFolders { get; set; }

        public System.Data.Entity.DbSet<Mahamesh.Models.NewsModel> NewsModels { get; set; }

        public System.Data.Entity.DbSet<Mahamesh.Models.TenderModel> TenderModels { get; set; }

        public System.Data.Entity.DbSet<Mahamesh.Models.PressInformationModel> PressInformationModels { get; set; }

        public System.Data.Entity.DbSet<Mahamesh.Models.FeedbackModel> FeedbackModels { get; set; }

        public System.Data.Entity.DbSet<Mahamesh.Models.MediaGalleryModel> MediaGalleryModels { get; set; }

        public System.Data.Entity.DbSet<Mahamesh.Models.ApplicantRegistration> ApplicantRegistrations { get; set; }
    }
}