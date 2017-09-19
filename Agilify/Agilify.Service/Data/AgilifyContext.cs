using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using AgilifyService.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AgilifyService.Data
{
    public class AgilifyContext : IdentityDbContext<User>
    {
        private const string ConnectionStringName = "Server=tcp:infinitivity.database.windows.net,1433;Initial Catalog=AgilifyDB;Persist Security Info=False;User ID=theloar;Password=A@d3m4s8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public AgilifyContext() : base(ConnectionStringName)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<AgileTask> Tasks { get; set; }
        public DbSet<AgileBug> Bugs { get; set; }
        public DbSet<ChatThread> ChatThreads { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members).WithMany(m => m.Teams)
                .Map(t => t.MapLeftKey("TeamId").MapRightKey("MemberId").ToTable("TeamMember"));
                
                    
        }

        public static AgilifyContext Create()
        {
            return new AgilifyContext();
        }
    }

}
