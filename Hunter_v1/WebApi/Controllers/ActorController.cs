using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Maintenances;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ActorController : ApiController
    {

        private readonly Maintenances_Actor objActor = new Maintenances_Actor();

        [HttpGet]
        public Response Datatable()
        {
            Response response = new Response();

            try
            {
                response.result = 1;
                response.data = objActor.Datatable();
            }
            catch (Exception ex)
            {

                response.result = 0;
                response.msg = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public Response insert([FromBody] Actor obj)
        {
            Response response = new Response();

            try
            {
                response.result = 1;
                response.data = objActor.insert(obj);
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public Response Find([FromBody]int Id)
        {
            Response response = new Response();

            try
            {
                response.result = 1;
                response.data = objActor.Find(Id);
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public Response Update([FromBody] Actor obj)
        {
            Response response = new Response();

            try
            {
                response.result = 1;
                response.data = objActor.update(obj);
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message.ToString();
            }

            return response;
        }

        [HttpDelete]
        public Response Delete(int Id)
        {
            Response response = new Response();

            try
            {
                response.result = 1;
                response.data = objActor.delete(Id);
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public Task<HttpResponseMessage> upload()
        {
            List<string> savedFilePath = new List<string>();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = HttpContext.Current.Server.MapPath("~/uploadFiles");
            var provider = new MultipartFileStreamProvider(rootPath);

            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }

                    foreach (MultipartFileData item in provider.FileData)
                    {
                        try
                        {
                            string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                            File.Move(item.LocalFileName, Path.Combine(rootPath, newFileName));

                            Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            string fileRelativePath = "~/uploadFiles/" + newFileName;
                            Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                            savedFilePath.Add(fileFullPath.ToString());
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });

            return task;
        }

        [HttpGet]
        public Response Actors()
        {
            Response response = new Response();

            try
            {
                response.result = 1;
                response.data = objActor.actors();
            }
            catch (Exception ex)
            {

                response.result = 0;
                response.msg = ex.Message.ToString();
            }

            return response;
        }
    }
}
