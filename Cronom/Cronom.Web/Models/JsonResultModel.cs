using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Models
{
    public class JsonResultModel
    {
        public JsonResultModel()
        {
            IsSuccessful = true;
            ResultType = JsonResultType.Success.ToString();
        }

        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
        public string ResultType { get; set; }
    }
}