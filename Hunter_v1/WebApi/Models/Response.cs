using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Response
    {
        public int result { get; set; }
        public object data { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }
}