using System.Data.SqlClient;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class CompanyRepository:ICompaniesRepository
    {
        public string Connection { get; set; }
        public SqlConnection con;
        public List<Company> companies = new List<Company>();
        public CompanyRepository(string connection)
        {
            Connection = connection;
        }
        public List<Company> CompanyList() 
        {
            using (SqlConnection con = new SqlConnection(Connection))
            { 
                con.Open();
                var cmd = new SqlCommand("GetCompanies",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
                SqlDataReader dr = cmd.ExecuteReader();
                companies.Clear();
                    while (dr.Read())
                    {
                        Company company = new Company();
                        company.Id = Convert.ToInt32(dr["Id"]);
                        company.Name = dr["Name"].ToString();
                        company.Description = dr["Description"].ToString();
                        if (Convert.IsDBNull(dr["TotalAVG"]))
                        {
                            company.Grade1AVG = 0;
                            company.Grade2AVG = 0;
                            company.Grade3AVG = 0;
                            company.Grade4AVG = 0;
                            company.Grade5AVG = 0;
                            company.TotalAVG = 0;  
                        }
                        else
                        {

                            company.Grade1AVG = Math.Round(Convert.ToDouble(dr["Grade1AVG"]),2);
                            company.Grade2AVG = Math.Round(Convert.ToDouble(dr["Grade2AVG"]),2);
                            company.Grade3AVG = Math.Round(Convert.ToDouble(dr["Grade3AVG"]),2);
                            company.Grade4AVG = Math.Round(Convert.ToDouble(dr["Grade4AVG"]),2);
                            company.Grade5AVG = Math.Round(Convert.ToDouble(dr["Grade5AVG"]),2);
                            company.TotalAVG = Math.Round(Convert.ToDouble(dr["TotalAVG"]),2);
                        }

                        companies.Add(company);
                    }
                

                companies.Sort();
                companies.Reverse();
                con.Close();
                return companies.ToList();
            }
            
        }

        public Company GetCompanyById(int id)
        {
            return companies.Find(company => company.Id.Equals(id) );
        }

        public void DeleteCompany(int id)
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                var cmd = new SqlCommand("DeleteCompany", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void EditCompany(Company company)
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                var cmd = new SqlCommand("EditCompany", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", company.Id);
                cmd.Parameters.AddWithValue("@CompanyName", company.Name);
                cmd.Parameters.AddWithValue("@CompanyDescription", company.Description);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddCompany(Company company)
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                var cmd = new SqlCommand("AddCompany", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyName", company.Name);
                cmd.Parameters.AddWithValue("@CompanyDescription", company.Description);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
