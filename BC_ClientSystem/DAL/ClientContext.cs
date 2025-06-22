using BC_ClientSystem.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BC_ClientSystem.DAL
{
    public class ClientContext : DbContext
    {
        // ✅ Constructor pointing to "DefaultConnection"
        public ClientContext() : base("ClientContext")
        {
        }

        // ✅ Use DbSet<T> instead of ISet<T> 
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}







