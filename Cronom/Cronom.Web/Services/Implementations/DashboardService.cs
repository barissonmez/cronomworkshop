using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Data.DBInteractions.Contracts;
using Cronom.Web.Domains;
using Cronom.Web.Domains.Enums;
using Cronom.Web.Helpers;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;

namespace Cronom.Web.Services.Implementations
{
    public class DashboardService : AbstractBaseService, IDashboardService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Book> _bookRepo;
        private readonly IRepository<Rental> _rentalRepo;

        public DashboardService(IRepository<User> userRepo, IRepository<Book> bookRepo, IRepository<Rental> rentalRepo)
        {
            _userRepo = userRepo;
            _bookRepo = bookRepo;
            _rentalRepo = rentalRepo;
        }

        public DashboardViewModel GetStats()
        {
            var totalBookStock = _bookRepo.GetAll().Count();
            var rentedBooks = _rentalRepo.GetMany(a => a.State == RentalState.Approved).Select(a => a.BookISBN);
            var currentBookStock = _bookRepo.GetMany(a => !rentedBooks.Contains(a.ISBN)).Count();
            var studentCount = _userRepo.GetMany(a => a.UserType == UserType.Student).Count();
            var managerCount = _userRepo.GetMany(a => a.UserType == UserType.Manager).Count();
            var requestCount = _rentalRepo.GetMany(a => a.State == RentalState.Pending).Count();
            var studentSpecificPendingRequestCount =
                _rentalRepo.GetMany(a => a.RentedById == CurrentUser.Identity.Id && a.State == RentalState.Pending)
                    .Count();
            var studentSpecificRejectedRequestCount =
                _rentalRepo.GetMany(a => a.RentedById == CurrentUser.Identity.Id && a.State == RentalState.Rejected)
                    .Count();
            var studentSpecificApprovedRequestCount =
                _rentalRepo.GetMany(a => a.RentedById == CurrentUser.Identity.Id && a.State == RentalState.Approved)
                    .Count();
            var studentSpecificTotalReadCount =
                _rentalRepo.GetMany(a => a.RentedById == CurrentUser.Identity.Id && a.State == RentalState.Returned)
                    .Count();


            return new DashboardViewModel()
            {
                CurrentStock = currentBookStock,
                ManagerCount = managerCount,
                RequestCount = requestCount,
                StudentCount = studentCount,
                TotalStock = totalBookStock,
                StudentSpecificPendingRequestCount = studentSpecificPendingRequestCount,
                StudentSpecificRejectedRequestCount = studentSpecificRejectedRequestCount,
                StudentSpecificApprovedRequestCount = studentSpecificApprovedRequestCount,
                StudentSpecificTotalReadCount = studentSpecificTotalReadCount
            };
        }
    }
}