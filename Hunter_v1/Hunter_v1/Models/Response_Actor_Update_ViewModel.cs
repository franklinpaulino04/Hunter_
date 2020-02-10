using Maintenances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hunter_v1.Models
{
    public class Response_Actor_Update_ViewModel
    {
        public int result { get; set; }
        public Actor data { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }
}