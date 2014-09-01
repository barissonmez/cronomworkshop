using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cronom.Web.Domains;
using Cronom.Web.Models;

namespace Cronom.Web.Services.Contracts
{
    public interface IRentalService
    {
        JsonResultModel RentBook(RentalInModel model);
        List<RentalViewModel> GetPendingRequests();
        List<RentalViewModel> GetRentalRequests();
        JsonResultModel Check(RequestCheckInModel model);
        JsonResultModel Cancel(RequestCheckInModel model);
        MostRentedBooksPagerViewModel GetPager(int? page, string dateRange);
    }
}


