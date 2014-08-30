using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Cronom.Web.Domains;

namespace Cronom.Web.Data.Mappings
{
    public class BookMapping : EntityTypeConfiguration<Book>
    {
        public BookMapping()
        {
            ToTable("Books");

            HasKey(c => c.ISBN);

            Property(c => c.Title).HasColumnName("Title").IsRequired().HasMaxLength(255);
            Property(c => c.Author).HasColumnName("Author").IsRequired().HasMaxLength(255);
            Property(c => c.Publisher).HasColumnName("Publisher").IsRequired().HasMaxLength(255);
            Property(c => c.PublicationYear).HasColumnName("PublicationYear").IsRequired().HasColumnType("int");
            Property(c => c.Slug).HasColumnName("Slug").IsRequired().HasMaxLength(255);
        }
    }
}