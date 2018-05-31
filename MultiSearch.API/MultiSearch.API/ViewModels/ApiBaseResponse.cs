using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiSearch.API.ViewModels
{
    public class ApiBaseResponse
    {
        public Error Error { get; set; }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}