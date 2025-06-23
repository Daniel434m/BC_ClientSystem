using BC_ClientSystem.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BC_ClientSystem.DAL
{
    public class ClientContext : DbContext
    {
        public ClientContext() : base("ClientContext") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Avoid pluralization
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configure many-to-many
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Contacts)
                .WithMany(c => c.Clients)
                .Map(cs =>
                {
                    cs.MapLeftKey("ClientId");
                    cs.MapRightKey("ContactId");
                    cs.ToTable("ClientContacts");
                });
        }
    }

}







