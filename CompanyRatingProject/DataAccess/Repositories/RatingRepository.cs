using System.Data;
using System.Data.SqlClient;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class RatingRepository:IRatingsRepository
{
    public string Connection { get; set; }
    public SqlConnection Con;
    public List<Rating> Ratings = new List<Rating>();
    
    public RatingRepository(string connection)
    {
        Connection = connection;
    }

    public List<Rating> GetRatingsByCompanyId(int id)
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

    public Rating GetRatingByAuthorId(int id,int companyId)
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {
            SqlCommand cmd = new SqlCommand("GetRatingByAuthorId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@CompanyId", companyId);
            Rating rating = new Rating();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
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
            }
            con.Close();
            return rating;
        }
    }
    public Rating GetRatingById(int id)
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {
            SqlCommand cmd = new SqlCommand("GetRatingById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            Rating rating = new Rating();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
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
            }
            con.Close();
            return rating;
        }
    }

    public void AddRate(Rating rating)
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
        }
    }

    public void ChangeRate(Rating rate)
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {
            var cmd = new SqlCommand("EditRate", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", rate.Id);
            cmd.Parameters.AddWithValue("@Text", rate.Text);
            cmd.Parameters.AddWithValue("@Grade1", rate.Grade1);
            cmd.Parameters.AddWithValue("@Grade2", rate.Grade2);
            cmd.Parameters.AddWithValue("@Grade3", rate.Grade3);
            cmd.Parameters.AddWithValue("@Grade4", rate.Grade4);
            cmd.Parameters.AddWithValue("@Grade5", rate.Grade5);
            cmd.Parameters.AddWithValue("@Total", rate.Total);
            cmd.Parameters.AddWithValue("@NewTime", rate.DateTime);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}