using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Hunter_v1
{
    public class GlobalWebApiClients
    {
        public static HttpClient WebApiClient = new HttpClient();

        static GlobalWebApiClients()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44356/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}