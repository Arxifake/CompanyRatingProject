using System.Data;
using Task1.Interfaces;
using System.Data.SqlClient;

namespace Task1.Models.DbContent;

public class RaitingContent:IRaitings
{
    public string Connection { get; set; }
    public SqlConnection con;
    public List<Rating> Ratings = new List<Rating>();
    
    public RaitingContent(string connection)
    {
        Connection = connection;
    }

    public List<Rating> getCompanyRaitings(int id)
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {


            con.Open();
            var cmd = new SqlCommand("GetRaitings", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Ratings.Clear();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Rating rating = new Rating();
                if (id == Convert.ToInt32(dr["CompanyId"]))
                {
                    rating.Id = Convert.ToInt32(dr["Id"]);
                    rating.Grade1 = Convert.ToInt32(dr["Grade1"]);
                    rating.Grade2 = Convert.ToInt32(dr["Grade2"]);
                    rating.Grade3 = Convert.ToInt32(dr["Grade3"]);
                    rating.Grade4 = Convert.ToInt32(dr["Grade4"]);
                    rating.Grade5 = Convert.ToInt32(dr["Grade5"]);
                    rating.Total = Convert.ToDouble(dr["Total"]);
                    if (Convert.IsDBNull(dr["CommentText"]))
                    {
                        rating.Text = "";
                    }
                    else
                    {
                        rating.Text = Convert.ToString(dr["CommentText"]);   
                    }
                    rating.DateTime = Convert.ToDateTime(dr["CommentDate"]);
                    rating.AuthorId = Convert.ToInt32(dr["AuthorId"]);
                    rating.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                    Ratings.Add(rating);
                }

            }
            con.Close();
        }

        return Ratings.ToList();
    }

    public Rating GetRaitingAuthor(int id)
    {
        return Ratings.First(raiting => raiting.Id.Equals(id));
    }

    public string AddRate(Rating rating)
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {
            SqlCommand cmd = new SqlCommand("sp_RateCompany", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Grade1", rating.Grade1);
            cmd.Parameters.AddWithValue("@Grade2", rating.Grade2);
            cmd.Parameters.AddWithValue("@Grade3", rating.Grade3);
            cmd.Parameters.AddWithValue("@Grade4", rating.Grade4);
            cmd.Parameters.AddWithValue("@Grade5", rating.Grade5);
            cmd.Parameters.AddWithValue("@Total", rating.Total);
            cmd.Parameters.AddWithValue("@CompanyId", rating.CompanyId);
            cmd.Parameters.AddWithValue("@AuthorId", rating.AuthorId);
            cmd.Parameters.AddWithValue("@CommentText", rating.Text);
            cmd.Parameters.AddWithValue("@DateTime", rating.DateTime);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return ("Rate adding to base");
        }
    }
}