using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EKS.Models
{
    public class CustomResponse
    {
        public string message { get; set; }
        public string data { get; set; }
        public int status { get; set; }
    }
}