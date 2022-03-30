using System.Data.SqlClient;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class CompanyRepository:ICompaniesRepository
    {
        public string Connection { get; set; }
        public SqlConnection Con;
        public List<Company> Companies = new List<Company>();
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
                Companies.Clear();
                    while (dr.Read())
                    {
                        Company company = new Company();
                        company.Id = Convert.ToInt32(dr["Id"]);
                        company.Name = dr["Name"].ToString();
                        company.Description = dr["Description"].ToString();
                        if (Convert.IsDBNull(dr["TotalAVG"]))
                        {
                            company.Grade1Avg = 0;
                            company.Grade2Avg = 0;
                            company.Grade3Avg = 0;
                            company.Grade4Avg = 0;
                            company.Grade5Avg = 0;
                            company.TotalAvg = 0;  
                        }
                        else
                        {

                            company.Grade1Avg = Math.Round(Convert.ToDouble(dr["Grade1AVG"]),2);
                            company.Grade2Avg = Math.Round(Convert.ToDouble(dr["Grade2AVG"]),2);
                            company.Grade3Avg = Math.Round(Convert.ToDouble(dr["Grade3AVG"]),2);
                            company.Grade4Avg = Math.Round(Convert.ToDouble(dr["Grade4AVG"]),2);
                            company.Grade5Avg = Math.Round(Convert.ToDouble(dr["Grade5AVG"]),2);
                            company.TotalAvg = Math.Round(Convert.ToDouble(dr["TotalAVG"]),2);
                        }

                        Companies.Add(company);
                    }
                

                Companies.Sort();
                Companies.Reverse();
                con.Close();
                return Companies.ToList();
            }
            
        }

        public Company GetCompanyById(int id)
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                var cmd = new SqlCommand("GetCompanyById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                Company company = new Company();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    company.Id = Convert.ToInt32(dr["Id"]);
                    company.Name = dr["Name"].ToString();
                    company.Description = dr["Description"].ToString();
                    if (Convert.IsDBNull(dr["TotalAVG"]))
                    {
                        company.Grade1Avg = 0;
                        company.Grade2Avg = 0;
                        company.Grade3Avg = 0;
                        company.Grade4Avg = 0;
                        company.Grade5Avg = 0;
                        company.TotalAvg = 0;  
                    }
                    else
                    {

                        company.Grade1Avg = Math.Round(Convert.ToDouble(dr["Grade1AVG"]),2);
                        company.Grade2Avg = Math.Round(Convert.ToDouble(dr["Grade2AVG"]),2);
                        company.Grade3Avg = Math.Round(Convert.ToDouble(dr["Grade3AVG"]),2);
                        company.Grade4Avg = Math.Round(Convert.ToDouble(dr["Grade4AVG"]),2);
                        company.Grade5Avg = Math.Round(Convert.ToDouble(dr["Grade5AVG"]),2);
                        company.TotalAvg = Math.Round(Convert.ToDouble(dr["TotalAVG"]),2);
                    }
                }
                return company;
            }
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
