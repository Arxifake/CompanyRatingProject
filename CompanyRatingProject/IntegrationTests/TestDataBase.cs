using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccess.Models;
using NUnit.Framework;


namespace IntegrationTests;
[SetUpFixture]
public class TestDataBase
{
    private string _databaseName;
    public string _connectionString;
    private bool _init = false;

    public void Init()
    {
        _databaseName = "TestDatabase";
        _connectionString = "Server = WS-MMALOFIIENKO;User ID = MEDE\\MikhailoMalofiienko; Trusted_Connection=True;";
        _init = true;
    }

    public SqlConnection GetSqlConnection()
    {
        if (_init)
        {
            return new SqlConnection(_connectionString);
        }

        return null;
    }

    public void CreateDatabase()
    {
        if (!_init)
        {
            return;
        }
        using (var connection = GetSqlConnection())
        {
            string createDatabase = $"CREATE DATABASE {_databaseName}";
            connection.Open();
            var cmd = new SqlCommand(createDatabase, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            _connectionString = $"Server = WS-MMALOFIIENKO;Database= {_databaseName};User ID = MEDE\\MikhailoMalofiienko; Trusted_Connection=True;";
        }
    }

    public void CreateTableCompanies()
    {
        using (var con = GetSqlConnection())
        {
            var createCompaniesTable = "Create table dbo.Companies(\n    Id int primary key identity (1,1),\n    Name varchar (max) not null ,\n    Description varchar (max) not null,\n    GradeAvg1 float,\n\tGradeAvg2 float,\n\tGradeAvg3 float,\n\tGradeAvg4 float,\n\tGradeAvg5 float,\n\tTotalAvg float \n);";
            con.Open();
            var cmd = new SqlCommand(createCompaniesTable, con);
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
    }

    public void InsertIntoCompanies(List<Company> companies)
    {
        using (var con = GetSqlConnection())
        {
            var insertIntoCompanies =
                $"Insert into dbo.Companies\n    (Name,\n     Description)\n     VALUES \n";
            foreach
                (var company in companies)
            {
                if (company.Name==companies[0].Name)
                {
                    insertIntoCompanies += $"     (\n      '{company.Name}',\n     '{company.Description}'\n     )\n";    
                }
                else
                {
                    insertIntoCompanies += $"     ,(\n      '{company.Name}',\n     '{company.Description}'\n     )\n";
                }
                
            }

            insertIntoCompanies += " ;";
            con.Open();
            var cmd = new SqlCommand(insertIntoCompanies, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public void CreateProceduresForCompanies()
    {
        using (var con = GetSqlConnection())
        {
            var createProcedureGetCompanies = "Create procedure GetCompanies " +
                                              "As " +
                                              "Begin " +
                                              "Select * from Companies " +
                                              "End";
            var createProcedureGetCompanyById = "Create procedure GetCompanyById " +
                                                "@ID int "+
                                                "As " +
                                                "Begin " +
                                                "Select * from Companies where Id=@ID " +
                                                "End";
            var createProcedureEditCompany = "Create procedure EditCompany " +
                                             "@CompanyId int, "+
                                             "@CompanyName varchar(MAX), "+
                                             "@CompanyDescription varchar(MAX) "+
                                             "As " +
                                             "Begin " +
                                             "Update Companies "+
                                             "set [Name]=@CompanyName, "+
                                             "[Description] =@CompanyDescription "+
                                             "where Id=@CompanyId " +
                                             "End";
            var createProcedureDeleteCompany = "Create procedure DeleteCompany " +
                                               "@CompanyId int "+
                                               "As " +
                                               "Begin " +
                                               "Delete Companies where Id=@CompanyId " +
                                               "End";
            var createProcedureAddCompany = "Create procedure AddCompany " +
                                            "@CompanyName varchar(MAX), "+
                                            "@CompanyDescription varchar(MAX) "+
                                            "As "+
                                            "Begin "+
                                            "Insert into dbo.Companies\n    (Name, Description)\n "+
                                            "Values(@CompanyName , @CompanyDescription )"+
                                            "End";
            con.Open();
            var cmd = new SqlCommand(createProcedureGetCompanies, con);
            var cmd2 = new SqlCommand(createProcedureGetCompanyById,con);
            var cmd3 = new SqlCommand(createProcedureEditCompany, con);
            var cmd4 = new SqlCommand(createProcedureDeleteCompany, con);
            var cmd5 = new SqlCommand(createProcedureAddCompany, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            con.Close();

        }
    }
    public void CreateTableRatings()
    {
        using (var con = GetSqlConnection())
        {
            var createCompaniesTable = "Create table dbo.Ratings(\n    Id int primary key identity (1,1),\n    Grade1 int not null,\n\tGrade2 int not null,\n\tGrade3 int not null,\n\tGrade4 int not null,\n\tGrade5 int not null,\n\tTotal float not null,\n\tCommentText text,\n\tCommentDate datetime not null,\n\tCompanyId int not null,\n\tAuthorId int not null\n);";
            con.Open();
            var cmd = new SqlCommand(createCompaniesTable, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public void InsertIntoRatings(List<Rating> ratings)
    {
        using (var con = GetSqlConnection())
        {
            var insertIntoRatings =
                $"Insert into dbo.Ratings\n    (Grade1,\n     Grade2,\n      Grade3,\n     Grade4,\n     Grade5,\n     Total,\n     CommentDate,\n     CompanyId,\n     AuthorId)\n     VALUES \n";
            foreach
                (var rating in ratings)
            {
                if (rating.Grade1==ratings[0].Grade1)
                {
                    insertIntoRatings += $"     (\n      '{rating.Grade1}',\n     '{rating.Grade2}',\n   '{rating.Grade3}',\n     '{rating.Grade4}',\n      '{rating.Grade5}',\n    '{rating.Total}',\n     '{rating.DateTime}',\n      '{rating.CompanyId}',\n     '{rating.UserId}'\n    )\n";    
                }
                else
                {
                    insertIntoRatings += $"     ,(\n      '{rating.Grade1}',\n     '{rating.Grade2}',\n   '{rating.Grade3}',\n     '{rating.Grade4}',\n      '{rating.Grade5}',\n    '{rating.Total}',\n     '{rating.DateTime}',\n      '{rating.CompanyId}',\n     '{rating.UserId}'\n    )\n";
                }
                
            }

            insertIntoRatings += " ;";
            con.Open();
            var cmd = new SqlCommand(insertIntoRatings, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
    public void CreateProceduresForRatings()
    {
        using (var con = GetSqlConnection())
        {
            var createProcedureGetRatingsByCompanyId = "Create procedure GetRaitings " +
                                                       "As " +
                                                       "Begin " +
                                                       "Select * from Ratings " +
                                                       "End";
            var createProcedureGetRatingById = "Create procedure GetRatingById " +
                                                "@Id int "+
                                                "As " +
                                                "Begin " +
                                                "Select * from Ratings where Id=@Id " +
                                                "End";
            var createProcedureEditRate = "Create procedure EditRate " +
                                          "@Id int, "+
                                          "@Text varchar(MAX), "+
                                          "@Grade1 int, @Grade2 int, @Grade3 int, @Grade4 int, @Grade5 int, @Total float, "+
                                          "@NewTime datetime "+
                                          "As "+
                                          "Begin "+
                                          "Update Ratings "+
                                          "set [Grade1]=@Grade1, [Grade2]=@Grade2, [Grade3]=@Grade3, [Grade4]=@Grade4, "+
                                          "[Grade5]=@Grade5, [Total]=@Total, [CommentText]=@Text, [CommentDate]=@NewTime "+
                                          "where Id=@Id " +
                                          "End";
            var createProcedureAddRate ="Create procedure sp_RateCompany " +
                                        "@CommentText varchar(MAX), "+
                                        "@Grade1 int, @Grade2 int, @Grade3 int, @Grade4 int, @Grade5 int, @Total float, "+
                                        "@DateTime datetime, "+
                                        "@AuthorId int, @CompanyId int "+
                                        "As "+
                                        "Begin "+
                                        "Insert into dbo.Ratings\n    (Grade1,\n     Grade2,\n      Grade3,\n     Grade4,\n     Grade5,\n     Total,\n     CommentDate,\n     CompanyId,\n     AuthorId, CommentText)\n "+
                                        "Values(@Grade1 , @Grade2 , @Grade3 , @Grade4 , @Grade5 , @Total ,@DateTime,@CompanyId,@AuthorId, @CommentText)"+
                                        "End";

            con.Open();
            var cmd = new SqlCommand(createProcedureGetRatingsByCompanyId, con);
            var cmd2 = new SqlCommand(createProcedureGetRatingById,con);
            var cmd3 = new SqlCommand(createProcedureEditRate, con);
            var cmd4 = new SqlCommand(createProcedureAddRate, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            con.Close();

        }
    }
    public void CreateTableUsers()
    {
        using (var con = GetSqlConnection())
        {
            var createAuthorTable = "Create table dbo.Authors(\n    Id int primary key identity (1,1),\n    NickName varchar (max));";
            con.Open();
            var cmd = new SqlCommand(createAuthorTable, con);
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
    }
    public void InsertIntoUsers(List<User> users)
    {
        using (var con = GetSqlConnection())
        {
            var insertIntoCompanies =
                $"Insert into dbo.Authors\n    (NickName)\n     VALUES \n";
            foreach
                (var user in users)
            {
                if (user.Nickname==users[0].Nickname)
                {
                    insertIntoCompanies += $"     (\n      '{user.Nickname}')\n";    
                }
                else
                {
                    insertIntoCompanies += $"     ,(\n      '{user.Nickname}')\n";
                }
                
            }

            insertIntoCompanies += " ;";
            con.Open();
            var cmd = new SqlCommand(insertIntoCompanies, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
    public void CreateProceduresForUsers()
    {
        using (var con = GetSqlConnection())
        {
            var createProcedureGetUsers = "Create procedure GetAuthors " +
                                                       "As " +
                                                       "Begin " +
                                                       "Select * from Authors " +
                                                       "End";
            var createProcedureGetUserById = "Create procedure GetAuthorById " +
                                                "@Id int "+
                                                "As " +
                                                "Begin " +
                                                "Select * from Authors where Id=@Id " +
                                                "End";
            var createProcedureAddUser ="Create procedure sp_AddAuthor " +
                                        "@NickName varchar(MAX) "+
                                        "As "+
                                        "Begin "+
                                        "Insert into dbo.Authors\n    (NickName)\n "+
                                        "Values(@NickName)"+
                                        "End";

            con.Open();
            var cmd = new SqlCommand(createProcedureGetUsers, con);
            var cmd2 = new SqlCommand(createProcedureGetUserById,con);
            var cmd3 = new SqlCommand(createProcedureAddUser, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            con.Close();

        }
    }
    public void DeleteDb()
    {
        SqlConnection.ClearAllPools();
        using (var con = GetSqlConnection())
        {
            con.Open();
            var deleteDb = $"use master drop database {_databaseName}";
            var cmd = new SqlCommand(deleteDb, con);
            cmd.ExecuteNonQuery();
        }
    }
}