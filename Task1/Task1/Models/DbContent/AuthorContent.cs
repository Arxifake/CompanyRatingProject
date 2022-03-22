using System.Data.SqlClient;
using System.Data;
using Task1.Interfaces;
using Task1.Models.Interfaces;

namespace Task1.Models.DbContent;

public class AuthorContent:IAuthors
{
    public string Connection { get; set; }
    public SqlConnection con;
    public List<Author> authors = new List<Author>();
    public AuthorContent(string connection)
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
            authors.Clear();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Author author = new Author();
                author.Id = Convert.ToInt32(dr["Id"]);
                author.Nickname = Convert.ToString(dr["NickName"]);
                authors.Add(author);
            }
            con.Close();
            return authors.ToList();
        }
    }

    public Author getAuthor(int id)
    { 
        return authors.First(author => author.Id.Equals(id));
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