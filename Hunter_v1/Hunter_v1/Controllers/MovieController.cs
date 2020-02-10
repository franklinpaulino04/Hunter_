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
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.GetAsync("movie/Datatable").Result;
            Response_Movie MovieList = JsonConvert.DeserializeObject<Response_Movie>(response.Content.ReadAsStringAsync().Result);
            List<Movie> movies = MovieList.data;
            return View(movies);
        }

        public ActionResult Create()
        {
            ViewBag.generos = this.getGeneros();
            ViewBag.actors = this.getActors();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie, HttpPostedFileBase file)
        {
            string image = MultipartFormUpload.setUpload("movie/upload", file);

            if (image != null)
            {
                movie.Photo = image;
                HttpResponseMessage response = GlobalWebApiClients.WebApiClient.PostAsJsonAsync("movie/insert", movie).Result;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.PostAsJsonAsync("movie/Find", Id.ToString()).Result;
            Response_Movie_Update_ViewModel MovieList = JsonConvert.DeserializeObject<Response_Movie_Update_ViewModel>(response.Content.ReadAsStringAsync().Result);
            Movie movie = MovieList.data;
            ViewBag.generos = this.getGeneros();
            ViewBag.actors = this.getActors();
            ViewBag.items = MovieList.data.Items;

            return View(movie);
        }

        [HttpPost]
        public ActionResult Update(Movie movie, HttpPostedFileBase file)
        {
            string image = MultipartFormUpload.setUpload("movie/upload", file);

            if (image != null)
            {
                movie.Photo = image;
                HttpResponseMessage response = GlobalWebApiClients.WebApiClient.PostAsJsonAsync("movie/Update", movie).Result;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.DeleteAsync("movie/Delete/" + Id.ToString()).Result;
            return RedirectToAction("Index");
        }

        private List<Genero> getGeneros()
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.GetAsync("movie/Generos").Result;
            Response_Genero generoList = JsonConvert.DeserializeObject<Response_Genero>(response.Content.ReadAsStringAsync().Result);
            List<Genero> generos = generoList.data;
            return generos;
        }

        private List<Actor> getActors()
        {
            HttpResponseMessage response = GlobalWebApiClients.WebApiClient.GetAsync("actor/actors").Result;
            Response_Actor_dropdown generoList = JsonConvert.DeserializeObject<Response_Actor_dropdown>(response.Content.ReadAsStringAsync().Result);
            List<Actor> actors = generoList.data;
            return actors;
        }
    }
}