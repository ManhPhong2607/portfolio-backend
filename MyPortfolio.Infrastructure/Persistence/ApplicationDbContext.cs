using Microsoft.EntityFrameworkCore;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<ReFreshToken> RefreshTokens => Set<ReFreshToken>();
        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Technology> Technologies => Set<Technology>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<Experience> Experiences => Set<Experience>();
        public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
        public DbSet<MediaFile> MediaFiles => Set<MediaFile>();
        public DbSet<AboutProfile> AboutProfiles => Set<AboutProfile>();
        public DbSet<SocialLink> SocialLinks => Set<SocialLink>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}