using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenances
{
    public class Maintenances_Movie : IMovie<Movie>
    {
        private SqlConnection con = DbConexion.ObtenerConexion();
        private SqlCommand cmd;

        public List<Movie> Datatable()
        {
            List<Movie> movies = new List<Movie>();
            cmd = new SqlCommand("SELECT [movieId],[title],[release_date],[generoId],[photo],[hidden] FROM [dbo].[ai_movies] WHERE hidden = 0", con);
            con.Open();
            SqlDataReader render = cmd.ExecuteReader();

            while (render.Read())
            {
                Movie movie = new Movie
                {
                    MovieId = int.Parse(render["movieId"].ToString()),
                    Title = render["title"].ToString(),
                    Release_date = DateTime.Parse(render["release_date"].ToString()),
                    Genero = int.Parse(render["generoId"].ToString()),
                    Photo = render["photo"].ToString(),
                };
                
                movies.Add(movie);
            }

            con.Close();

            return movies;
        }

        public int delete(int Id)
        {
            cmd = new SqlCommand("UPDATE [dbo].[ai_movies] SET [hidden] = 1 WHERE movieId = @movieId", con);
            cmd.Parameters.Add("@movieId", SqlDbType.Int);
            cmd.Parameters["@movieId"].Value = Id;

            con.Open();
            int r = cmd.ExecuteNonQuery();
            con.Close();

            return r;

        }

        public int delete_itemId(int Id)
        {
            cmd = new SqlCommand("UPDATE [dbo].[ai_movies_items] SET [hidden] = 1 WHERE itemId = @itemId", con);
            cmd.Parameters.Add("@itemId", SqlDbType.Int);
            cmd.Parameters["@itemId"].Value = Id;

            con.Open();
            int r = cmd.ExecuteNonQuery();
            con.Close();

            return r;

        }

        public Movie Find(int Id)
        {
            Movie actor = new Movie();
            cmd = new SqlCommand("SELECT [movieId],[title],[release_date],[generoId],[photo],[hidden] FROM [dbo].[ai_movies] WHERE hidden = 0 AND movieId = @movieId", con);
            cmd.Parameters.Add("@movieId", SqlDbType.Int);
            cmd.Parameters["@movieId"].Value = Id;

            con.Open();
            SqlDataReader render = cmd.ExecuteReader();

            if (render.Read())
            {
                actor.MovieId = int.Parse(render["movieId"].ToString());
                actor.Title = render["title"].ToString();
                actor.Release_date = DateTime.Parse(render["release_date"].ToString());
                actor.Genero = int.Parse(render["generoId"].ToString());
                actor.Photo = render["photo"].ToString();
                actor.Hidden = int.Parse(render["hidden"].ToString());
               
            }

            con.Close();
            actor.Items = this.getItems(Id);

            return actor;
        }

        public int insert(Movie obj)
        {
            cmd = new SqlCommand("INSERT INTO ai_movies ([title],[release_date],[generoId],[photo]) VALUES (@title, @release_date, @genero, @photo); SELECT SCOPE_IDENTITY(); ", con);
            cmd.Parameters.Add("@title", SqlDbType.VarChar);
            cmd.Parameters["@title"].Value = obj.Title;

            cmd.Parameters.Add("@release_date", SqlDbType.DateTime);
            cmd.Parameters["@release_date"].Value = obj.Release_date;

            cmd.Parameters.Add("@genero", SqlDbType.Int);
            cmd.Parameters["@genero"].Value = obj.Genero;

            cmd.Parameters.Add("@photo", SqlDbType.VarChar);
            cmd.Parameters["@photo"].Value = obj.Photo;
            
            con.Open();
            int r = int.Parse(cmd.ExecuteScalar().ToString());
            
            foreach (var item in obj.Items)
            {
                cmd = new SqlCommand("INSERT INTO ai_movies_items ([movieId],[actorId]) VALUES (@movieId, @actorId)", con);
                cmd.Parameters.Add("@movieId", SqlDbType.Int);
                cmd.Parameters["@movieId"].Value = r;

                cmd.Parameters.Add("@actorId", SqlDbType.Int);
                cmd.Parameters["@actorId"].Value = item.ActorId;

                cmd.ExecuteNonQuery();
            }

            con.Close();
            return r;
        }

        public int update(Movie obj)
        {
            cmd = new SqlCommand("UPDATE ai_movies SET [title] = @title, release_date = @release_date, generoId = @genero, photo = @photo WHERE movieId = @movieId", con);
            cmd.Parameters.Add("@movieId", SqlDbType.Int);
            cmd.Parameters["@movieId"].Value = obj.MovieId;

            cmd.Parameters.Add("@title", SqlDbType.VarChar);
            cmd.Parameters["@title"].Value = obj.Title;

            cmd.Parameters.Add("@release_date", SqlDbType.DateTime);
            cmd.Parameters["@release_date"].Value = obj.Release_date;

            cmd.Parameters.Add("@genero", SqlDbType.Int);
            cmd.Parameters["@genero"].Value = obj.Genero;

            cmd.Parameters.Add("@photo", SqlDbType.VarChar);
            cmd.Parameters["@photo"].Value = obj.Photo;

            con.Open();
            int r = cmd.ExecuteNonQuery();

            foreach (var item in obj.Items)
            {
                if (item.itemId == 0)
                {
                    cmd = new SqlCommand("INSERT INTO ai_movies_items ([movieId],[actorId]) VALUES (@movieId, @actorId)", con);
                }
                else
                {
                    cmd = new SqlCommand("UPDATE ai_movies_items SET actorId = @actorId WHERE itemId = @itemId", con);
                }

                cmd.Parameters.Add("@itemId", SqlDbType.Int);
                cmd.Parameters["@itemId"].Value = item.itemId;

                cmd.Parameters.Add("@actorId", SqlDbType.Int);
                cmd.Parameters["@actorId"].Value = item.ActorId;

                cmd.ExecuteNonQuery();
            }

            con.Close();

            return r;
        }

        public List<Genero> Generos()
        {
            List<Genero> generos = new List<Genero>();
            cmd = new SqlCommand("SELECT generoId, name FROM ai_generos", con);
            con.Open();
            SqlDataReader render = cmd.ExecuteReader();

            while (render.Read())
            {
                Genero genero = new Genero
                {
                    GeneroId = int.Parse(render["generoId"].ToString()),
                    Name = render["name"].ToString(),
                };

                generos.Add(genero);
            }

            con.Close();

            return generos;
        }

        private List<Movie_Items> getItems(int Id) 
        {
            List<Movie_Items> items = new List<Movie_Items>();
            cmd = new SqlCommand("SELECT [movieId],[itemId],[ActorId] FROM [dbo].[ai_movies_items] WHERE hidden = 0 AND movieId = @movieId", con);
            cmd.Parameters.Add("@movieId", SqlDbType.Int);
            cmd.Parameters["@movieId"].Value = Id;
           
            con.Open();
            SqlDataReader render_items = cmd.ExecuteReader();
            
            while (render_items.Read())
            {
                Movie_Items item = new Movie_Items
                {
                    itemId = int.Parse(render_items["itemId"].ToString()),
                    ActorId = int.Parse(render_items["actorId"].ToString()),
                    MovieId = int.Parse(render_items["movieId"].ToString()),
                };

                items.Add(item);
            }
            con.Close();

            return items;
        }
    }
}
