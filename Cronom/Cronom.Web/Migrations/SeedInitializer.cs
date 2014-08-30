using System.Data.Entity.Migrations;
using System.Text;
using System.Text.RegularExpressions;
using Cronom.Web.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Cronom.Web.Domains;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Migrations
{
    public class SeedInitializer : DropCreateDatabaseIfModelChanges<CronomDBContext>
    {
        protected override void Seed(Cronom.Web.Data.CronomDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Users.AddOrUpdate(
              new User { FullName = "Super ADMIN", UserName = "super", Password = "123", UserType = UserType.SuperAdmin },
              new User { FullName = "Brs SNMZ", UserName = "brs", Password = "456", UserType = UserType.Manager },
              new User { FullName = "John BROWN", UserName = "E1250", Password = "789", UserType = UserType.Student }
            );

            context.Books.AddOrUpdate(
                new Book { ISBN = "0-126-3367-2", Author = "Alexandre Dumas", Title = "The Three Musketeers", Publisher = "Grosset & Dunlap", PublicationYear = 1953, Slug = ToSlug("The Three Musketeers") },
                new Book { ISBN = "0-129-9876-5", Author = "James Clavell", Title = "Shogu", Publisher = "Atheneum", PublicationYear = 1975, Slug = ToSlug("Shogu") },
                new Book { ISBN = "0-131-4809-X", Author = "Anne Rice", Title = "The Vampire Lestat", Publisher = "Knopf", PublicationYear = 1985, Slug = ToSlug("The Vampire Lestat") },
                new Book { ISBN = "0-132-3949-2", Author = "Mark Twain", Title = "The Prince and the Pauper", Publisher = "James R. Osgood and Co.", PublicationYear = 1882, Slug = ToSlug("The Prince and the Pauper") },
                new Book { ISBN = "0-136-3956-1", Author = "Robert Louis Stevenson", Title = "Kidnapped", Publisher = "Dodd, Mead", PublicationYear = 1949, Slug = ToSlug("Kidnapped") },
                new Book { ISBN = "0-150-3765-2", Author = "Robert Ludlum", Title = "The Aquitaine Progression", Publisher = "Random House", PublicationYear = 1984, Slug = ToSlug("The Aquitaine Progression") },
                new Book { ISBN = "0-151-9876-2", Author = "Charlotte Bronte", Title = "Vilette", Publisher = "Clarendon Press", PublicationYear = 1984, Slug = ToSlug("Vilette") },
                new Book { ISBN = "0-180-3948-2", Author = "Octavia Butler", Title = "Patternmaster", Publisher = "Doubleday", PublicationYear = 1976, Slug = ToSlug("Patternmaster") },
                new Book { ISBN = "0-185-9855-2", Author = "Tanith Lee", Title = "The Winter Players", Publisher = "Macmilla", PublicationYear = 1977, Slug = ToSlug("The Winter Players") }
            );


            context.SaveChanges();
        }

        public string ToSlug(string value)
        {

            if (string.IsNullOrEmpty(value))
                return string.Empty;

            value = value.ToLower();

            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);

            value = Encoding.ASCII.GetString(bytes);
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
            value = value.Trim('-', '_');
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return string.Format("{0}", value);
        }
    }
}