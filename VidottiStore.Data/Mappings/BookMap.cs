using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidottiStore.Domain;

namespace VidottiStore.Data.Mappings
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("Book");

            HasKey(X => X.Id);

            Property(X => X.Title).HasMaxLength(255).IsRequired();
            Property(X => X.Price).IsRequired().HasColumnType("Money");
            Property(X => X.ReleaseDate).IsRequired();

            HasMany(x => x.Authors).WithMany(x => x.Books);
        }
    }
}
