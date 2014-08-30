using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Domains;
using Cronom.Web.Models;
using FastMapper;

namespace Cronom.Web.Infrastructure
{
    public class TypeAdapterConfiguration
    {
        public static void Initialize()
        {
            TypeAdapterConfig<Rental, RentalViewModel>
                .NewConfig()
                .MapFrom(dest => dest.Title, src => string.Format("{0}", src.Book.Title))
                .MapFrom(dest => dest.ISBN, src => string.Format("{0}", src.Book.ISBN))
                .MapFrom(dest => dest.Author, src => string.Format("{0}", src.Book.Author))
                .MapFrom(dest => dest.RentedBy, src => string.Format("{0}", src.RentedBy.FullName));

        }
    }
}