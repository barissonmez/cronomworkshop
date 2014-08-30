using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalStock { get; set; }
        public int CurrentStock { get; set; }
        public int StudentCount { get; set; }
        public int ManagerCount { get; set; }
        public int RequestCount { get; set; }
        public int StudentSpecificPendingRequestCount { get; set; }
        public int StudentSpecificRejectedRequestCount { get; set; }
        public int StudentSpecificApprovedRequestCount { get; set; }
        public int StudentSpecificTotalReadCount { get; set; }
    }
}