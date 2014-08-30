using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Cronom.Web.Domains;

namespace Cronom.Web.Data.Mappings
{
    public class UserMapping: EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("Users");

            HasKey(c => c.Id);

            Property(c => c.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(100);
            Property(c => c.FullName).HasColumnName("FullName").IsRequired().HasMaxLength(100);
            Property(c => c.Password).HasColumnName("Password").IsRequired().HasMaxLength(50);
            Property(c => c.UserType).HasColumnName("UserType").IsRequired().HasColumnType("int");
        }
    }
}