using System.Data;
using System.Data.SqlClient;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class NicknameRepository:INicknameRepository
{
    public string Connection { get; set; }
    public SqlConnection Con;
    public NicknameRepository(string connection)
    {
        Connection = connection;
    }
    
    public Nickname GetNickname()
    {
        using (SqlConnection con = new SqlConnection(Connection))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("GetNickname",con);
            cmd.CommandType = CommandType.StoredProcedure;
            Nickname name = new Nickname();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                name.Name = Convert.ToString(dr["Nickname"]);
            }
            con.Close();
            return name;
        }
    }
}