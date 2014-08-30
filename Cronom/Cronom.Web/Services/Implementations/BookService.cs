using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Data.DBInteractions.Contracts;
using Cronom.Web.Domains;
using Cronom.Web.Domains.Enums;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;
using FastMapper;

namespace Cronom.Web.Services.Implementations
{
    public class BookService : AbstractBaseService, IBookService
    {
        private readonly IRepository<Book> _repo;
        private readonly IRepository<Rental> _rentalRepo;

        public BookService(IRepository<Book> repo, IRepository<Rental> rentalRepo)
        {
            _repo = repo;
            _rentalRepo = rentalRepo;
        }

        public List<Book> GetBooks()
        {
            return _repo.GetAll().ToList();
        }


        public Book GetBookBySlug(string slug)
        {
            return _repo.Get(a => a.Slug.Equals(slug));
        }


        public StockOutModel GetStock()
        {
            var rentedBooks = _rentalRepo.GetMany(a => a.State == RentalState.Approved).Select(a => a.BookISBN);

            var books = _repo.GetMany(a => !rentedBooks.Contains(a.ISBN)).ToList();

            var booksModel = TypeAdapter.Adapt<List<Book>, List<BookViewModel>>(books);

            return new StockOutModel()
            {
                Books = booksModel,
                Count = books.Count
            };
        }
    }
}