using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Hunter_v1
{
    public class MultipartFormUpload
    {
        public static string setUpload(string url, HttpPostedFileBase file)
        {
            string image = null;

            using (var content = new MultipartFormDataContent())
            {
                byte[] Bytes = new byte[file.InputStream.Length + 1];
                file.InputStream.Read(Bytes, 0, Bytes.Length);
                var fileContent = new ByteArrayContent(Bytes);
                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                content.Add(fileContent);

                var result = GlobalWebApiClients.WebApiClient.PostAsync(url, content).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    List<string> m = result.Content.ReadAsAsync<List<string>>().Result;
                    image = m.FirstOrDefault();
                }
                else
                {
                    image = null;
                }
            }

            return image;
        }
    }
}