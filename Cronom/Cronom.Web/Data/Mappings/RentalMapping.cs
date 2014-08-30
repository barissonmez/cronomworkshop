using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Cronom.Web.Domains;

namespace Cronom.Web.Data.Mappings
{
    public class RentalMapping : EntityTypeConfiguration<Rental>
    {
        public RentalMapping()
        {
            ToTable("Rentals");

            HasKey(c => c.Id);

            Property(c => c.CheckOutDate).HasColumnName("CheckOutDate").IsRequired().HasColumnType("datetime");
            Property(c => c.ReturnDate).HasColumnName("ReturnDate").IsRequired().HasColumnType("datetime");
            Property(c => c.State).HasColumnName("State").IsRequired().HasColumnType("int");

            HasRequired(a => a.RentedBy).WithMany(a => a.Rentals).HasForeignKey(a => a.RentedById);
            HasRequired(a => a.Book).WithMany(a => a.Rentals).HasForeignKey(a => a.BookISBN);
        }
    }
}