using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Cronom.Web.Data.Mappings;
using Cronom.Web.Domains;
using Cronom.Web.Migrations;

namespace Cronom.Web.Data
{
    public class CronomDBContext : DbContext
    {
        public CronomDBContext() : base("CronomDBContext")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Database.SetInitializer(new SeedInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove();

            ConfigureMappings(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureMappings(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new BookMapping());
            modelBuilder.Configurations.Add(new RentalMapping());
        }

        public bool Commit()
        {
            try
            {
                return base.SaveChanges() > 0;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}", validationErrors.Entry.Entity.GetType().FullName,
                                      validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                //TODO Log This Error

                return false;
            }
        }
    }
}