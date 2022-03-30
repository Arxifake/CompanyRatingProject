using System.Data;
using System.Data.SqlClient;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class AuthorRepository:IAuthorsRepository
{
    public string Connection { get; set; }
    public SqlConnection Con;
    public List<Author> Authors = new List<Author>();
    public AuthorRepository(string connection)
    {
        Connection = connection;
    }
    public List<Author> AuthorList()
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {
            con.Open();
            var cmd = new SqlCommand("GetAuthors",con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Authors.Clear();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Author author = new Author();
                author.Id = Convert.ToInt32(dr["Id"]);
                author.Nickname = Convert.ToString(dr["NickName"]);
                Authors.Add(author);
            }
            con.Close();
            return Authors.ToList();
        }
    }

    public Author GetAuthorById(int id)
    { 
        using (SqlConnection con = new SqlConnection(Connection))
        {
            con.Open();
            var cmd = new SqlCommand("GetAuthorById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            Author author = new Author();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                author.Id = Convert.ToInt32(dr["Id"]);
                author.Nickname = dr["NickName"].ToString();
                
            }
            return author;
        }
    }

    public void NewAuthor(Author author)
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {
            SqlCommand cmd = new SqlCommand("sp_AddAuthor",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("NickName", author.Nickname);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}