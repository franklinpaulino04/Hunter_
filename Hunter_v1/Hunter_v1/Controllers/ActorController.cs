using Hunter_v1.Models;
using Maintenances;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Hunter_v1.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.GetAsync("actor/Datatable").Result;
            Response_Actor ActorList = JsonConvert.DeserializeObject<Response_Actor>(response.Content.ReadAsStringAsync().Result);
            List<Actor> actors = ActorList.data;
            return View(actors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Actor actor, HttpPostedFileBase file)
        {
            string image = MultipartFormUpload.setUpload("actor/upload", file);

            if (image != null)
            {
                actor.Photo = image;
                HttpResponseMessage response = GlobalWebApiClients.WebApiClient.PostAsJsonAsync("actor/insert", actor).Result;
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.PostAsJsonAsync("actor/Find", Id.ToString()).Result;
            Response_Actor_Update_ViewModel ActorList = JsonConvert.DeserializeObject<Response_Actor_Update_ViewModel>(response.Content.ReadAsStringAsync().Result);
            Actor actor = ActorList.data;

            return View(actor);
        }

        [HttpPost]
        public ActionResult Update(Actor actor, HttpPostedFileBase file)
        {
            string image = MultipartFormUpload.setUpload("actor/upload", file);

            if (image != null)
            {
                actor.Photo = image;
                HttpResponseMessage response = GlobalWebApiClients.WebApiClient.PostAsJsonAsync("actor/Update", actor).Result;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.DeleteAsync("actor/Delete/" + Id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}