﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cronom.Web.Data.DBInteractions.Contracts;
using Cronom.Web.Domains;
using Cronom.Web.Domains.Enums;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;
using FastMapper;

namespace Cronom.Web.Services.Implementations
{

    public class RentalService : AbstractBaseService, IRentalService
    {
        private readonly IRepository<Rental> _rentalRepo;
        private readonly IRepository<Book> _bookRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IUnitOfWork _uow;
        private const int AllowedRentalsCount = 2;

        public RentalService(IRepository<Rental> rentalRepo, IRepository<Book> bookRepo, IRepository<User> userRepo, IUnitOfWork uow)
        {
            _rentalRepo = rentalRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _uow = uow;
        }

        [HttpPost]
        public JsonResultModel RentBook(RentalInModel model)
        {
            var result = new JsonResultModel();

            var book = _bookRepo.Get(a => a.ISBN == model.ISBN);
            var user = _userRepo.GetById(Guid.Parse(model.StudentId));
            var dates = GetFormattedDates(model.FullDate);

            var activeRentalsCount = _rentalRepo.GetMany(a => a.RentedById == user.Id && a.State == RentalState.Pending || a.State == RentalState.Approved).Count();

            if (activeRentalsCount < AllowedRentalsCount)
            {
                var rental = new Rental()
                {
                    Book = book,
                    BookISBN = book.ISBN,
                    RentedBy = user,
                    RentedById = user.Id,
                    State = RentalState.Pending,
                    CheckOutDate = dates[0],
                    ReturnDate = dates[1]
                };

                _rentalRepo.Add(rental);
                
                if (_uow.Commit())
                {
                    result.Message = "İşleminiz başarılı şekilde gerçekleşti.";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.ResultType = JsonResultType.Error.ToString();
                    result.Message = "Bir sorun oluştu. Lütfen tekrar deneyin.";
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.ResultType = JsonResultType.Warning.ToString();
                result.Message = "İzin verilen kiralama sınırını aştığınız için işleminiz devam edemiyor.  Daha önce kiraladığınız kitapları iade ederek veya bekleyen taleplerinizi iptal ederek yeni kiralama talebi oluşturabilirsiniz.";
            }

            return result;
        }

        private DateTime[] GetFormattedDates(string fullDate)
        {
            DateTime[] dates = new DateTime[2];

            var splittedDate = fullDate.Split('-');

            for (int i = 0; i < splittedDate.Length; i++)
            {
                var dt = DateTime.ParseExact(splittedDate[i].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dates[i] = dt;
            }

            return dates;
        }


        public List<RentalViewModel> GetPendingRequests()
        {
            var pendingRequests = _rentalRepo.QueryObjectGraph(a => a.State == RentalState.Pending,"Book","RentedBy").ToList();

            var pendingRequestsModel = TypeAdapter.Adapt<List<Rental>, List<RentalViewModel>>(pendingRequests);

            return pendingRequestsModel;
        }

        public JsonResultModel Check(RequestCheckInModel model)
        {
            var result = new JsonResultModel();

            var rental = _rentalRepo.GetById(Guid.Parse(model.Id));

            if (rental != null)
            {

                switch (model.State)
                {
                    case true:
                        rental.State = RentalState.Approved;
                        break;
                    case false:
                        rental.State = RentalState.Rejected;
                        break;
                }

                _rentalRepo.Update(rental);

                if (_uow.Commit()) return result;

                result.IsSuccessful = false;
                result.Message = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Kayıt bulunamadığı için işleminiz gerçekleştirilemedi.";
            }
            return result;
        }

        public JsonResultModel Cancel(RequestCheckInModel model)
        {
            var result = new JsonResultModel();

            var rental = _rentalRepo.GetById(Guid.Parse(model.Id));

            if (rental != null)
            {
                _rentalRepo.Delete(rental);

                if (_uow.Commit()) return result;

                result.IsSuccessful = false;
                result.Message = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = "Kayıt bulunamadığı için işleminiz gerçekleştirilemedi.";
            }
            return result;
        }


        public List<RentalViewModel> GetRentalRequests()
        {
            var requests = _rentalRepo.QueryObjectGraph(a => a.State != RentalState.Returned, "Book", "RentedBy").ToList();

            var requestsModel = TypeAdapter.Adapt<List<Rental>, List<RentalViewModel>>(requests);

            return requestsModel;
        }
    }
}