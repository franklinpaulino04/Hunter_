using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenances
{
    public class Maintenances_Actor : IActor<Actor>
    {
        private SqlConnection con = DbConexion.ObtenerConexion();
        private SqlCommand cmd;

        public List<Actor> Datatable()
        {
            List<Actor> actors = new List<Actor>();
            cmd = new SqlCommand("SELECT [actorId],[name],[birth_date],[sexo],[photo],[hidden] FROM [dbo].[ai_actors] WHERE hidden = 0", con);
            con.Open();
            SqlDataReader render = cmd.ExecuteReader();

            while (render.Read())
            {
                Actor actor = new Actor
                {
                    ActorId = int.Parse(render["actorId"].ToString()),
                    Name = render["name"].ToString(),
                    Birth_date = DateTime.Parse(render["birth_date"].ToString()),
                    Sexo = int.Parse(render["sexo"].ToString()),
                    Photo = render["photo"].ToString(),
                    Hidden = int.Parse(render["hidden"].ToString()),
                };

                actors.Add(actor);
            }

            con.Close();

            return actors;
        }

        public int delete(int Id)
        {
            cmd = new SqlCommand("UPDATE [dbo].[ai_actors] SET [hidden] = 1 WHERE actorId = @actorId", con);
            cmd.Parameters.Add("@actorId", SqlDbType.Int);
            cmd.Parameters["@actorId"].Value = Id;

            con.Open();
            int r = cmd.ExecuteNonQuery();
            con.Close();

            return r;

        }

        public Actor Find(int Id)
        {
            Actor actor = new Actor();
            cmd = new SqlCommand("SELECT [actorId],[name],[birth_date],[sexo],[photo],[hidden] FROM [dbo].[ai_actors] WHERE hidden = 0 AND actorId = @actorId", con);
            cmd.Parameters.Add("@actorId", SqlDbType.Int);
            cmd.Parameters["@actorId"].Value = Id;

            con.Open();
            SqlDataReader render = cmd.ExecuteReader();

            if (render.Read())
            {
                actor.ActorId = int.Parse(render["actorId"].ToString());
                actor.Name = render["name"].ToString();
                actor.Birth_date = DateTime.Parse(render["birth_date"].ToString());
                actor.Sexo = int.Parse(render["sexo"].ToString());
                actor.Photo = render["photo"].ToString();
                actor.Hidden = int.Parse(render["hidden"].ToString());
            }

            con.Close();

            return actor;
        }

        public int insert(Actor obj)
        {
            cmd = new SqlCommand("INSERT INTO ai_actors (name,birth_date,sexo,photo) VALUES (@name, @birth_date, @sexo, @photo)", con);
            cmd.Parameters.Add("@name", SqlDbType.VarChar);
            cmd.Parameters["@name"].Value = obj.Name;

            cmd.Parameters.Add("@birth_date", SqlDbType.DateTime);
            cmd.Parameters["@birth_date"].Value = obj.Birth_date;

            cmd.Parameters.Add("@sexo", SqlDbType.Int);
            cmd.Parameters["@sexo"].Value = obj.Sexo;

            cmd.Parameters.Add("@photo", SqlDbType.VarChar);
            cmd.Parameters["@photo"].Value = obj.Photo;

            con.Open();
            int r = cmd.ExecuteNonQuery();
            con.Close();

            return r;
        }

        public int update(Actor obj)
        {
            cmd = new SqlCommand("UPDATE ai_actors SET [name] = @name, birth_date = @birth_date, sexo = @sexo, photo = @photo WHERE actorId = @actorId", con);
            cmd.Parameters.Add("@actorId", SqlDbType.Int);
            cmd.Parameters["@actorId"].Value = obj.ActorId;

            cmd.Parameters.Add("@name", SqlDbType.VarChar);
            cmd.Parameters["@name"].Value = obj.Name;

            cmd.Parameters.Add("@birth_date", SqlDbType.DateTime);
            cmd.Parameters["@birth_date"].Value = obj.Birth_date;

            cmd.Parameters.Add("@sexo", SqlDbType.Int);
            cmd.Parameters["@sexo"].Value = obj.Sexo;

            cmd.Parameters.Add("@photo", SqlDbType.VarChar);
            cmd.Parameters["@photo"].Value = obj.Photo;

            con.Open();
            int r = cmd.ExecuteNonQuery();
            con.Close();

            return r;
        }

        public List<Actor> actors()
        {
            List<Actor> actors = new List<Actor>();
            cmd = new SqlCommand("SELECT actorId, name FROM ai_actors", con);
            con.Open();
            SqlDataReader render = cmd.ExecuteReader();

            while (render.Read())
            {
                Actor actor = new Actor
                {
                    ActorId = int.Parse(render["actorId"].ToString()),
                    Name = render["name"].ToString(),
                };

                actors.Add(actor);
            }

            con.Close();

            return actors;
        }
    }
}
