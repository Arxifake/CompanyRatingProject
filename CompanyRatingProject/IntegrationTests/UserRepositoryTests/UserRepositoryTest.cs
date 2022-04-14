using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using NUnit.Framework;

namespace IntegrationTests.UserRepositoryTests;

public class UserRepositoryTest
{
    private IUsersRepository _usersRepository;
    private TestDataBase _base;

    private List<User> Users()
    {
        List <User> users = new List<User>()
        {
            new User(){Nickname = "1"},new User(){Nickname = "2"},
            new User(){Nickname = "3"},new User(){Nickname = "4"}
        };
        return users;
    }
    private static List<User> UsersForAdd()
    {
        List <User> users = new List<User>()
        {
            new User(){Nickname = "5"},new User(){Nickname = "6"}
        };
        return users;
    }
    [OneTimeSetUp]
    public void SetUp()
    {
        _base = new TestDataBase();
        _base.Init();
        _base.CreateDatabase();
        _base.CreateTableUsers();
        _base.CreateProceduresForUsers();
        _base.InsertIntoUsers(Users());
        _usersRepository = new UsersRepository(_base._connectionString);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _base.DeleteDb();
    }

    [Test]
    public void GetUsers_GetList_GetListUsers()
    {
        var users = _usersRepository.UsersList();
        var usersList = Users();
        Assert.AreEqual(users.First().Nickname,Users().First().Nickname);
        Assert.AreEqual(users.Last().Nickname,Users().Last().Nickname);
        Assert.AreEqual(users.Count(),Users().Count());
    }
    [Test]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(1)]
    public void GetUserById_UserId_UserWithThatId(int id)
    {
        var user =_usersRepository.GetUserById(id);
        Assert.AreEqual(user.Id,id);
    }
    [Test]
    [TestCaseSource(nameof(UsersForAdd))]
    public void AddUser_User_UserAddToDb(User user)
    {
        _usersRepository.NewUser(user);
        var users = _usersRepository.UsersList();
        Assert.AreEqual(user.Nickname,users.Last().Nickname);
    }
}