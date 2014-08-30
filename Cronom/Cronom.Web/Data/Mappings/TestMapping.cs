using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Cronom.Web.Domains;

namespace Cronom.Web.Data.Mappings
{
    public class TestMapping : EntityTypeConfiguration<Test>
    {
        public TestMapping()
        {
            ToTable("Tests");

            HasKey(c => c.Id);

            Property(c => c.Title).HasColumnName("Title").IsRequired().HasMaxLength(255);
            Property(c => c.Description).HasColumnName("Description").IsRequired().HasMaxLength(255);
        }
    }
}