using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidottiStore.Data.Mappings;
using VidottiStore.Domain;

namespace VidottiStore.Data.DataContexts
{
    public class VidottiStoreDataContext : DbContext
    {
        public VidottiStoreDataContext() : base("VidottiStoreConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;   // carregamento preguiçoso 
            Configuration.ProxyCreationEnabled = false; // destivar o proxy  
        }

        public DbSet<Book> _Books { get; set; }
        public DbSet<Author> _Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new AuthorMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
