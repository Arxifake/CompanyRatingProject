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
            new User(){Id = "111111111111111111111111",Nickname = "1"},new User(){Id = "222222222222222222222222",Nickname = "2"},
            new User(){Id = "333333333333333333333333",Nickname = "3"},new User(){Id = "444444444444444444444444",Nickname = "4"}
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
        _base.AddUsers(Users());
        _usersRepository = new UserRepository("mongodb://localhost:27017/",_base._databaseName);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _base.Dispose();
    }

    [Test]
    public void AGetUsers_GetList_GetListUsers()
    {
        var users = _usersRepository.UsersList();
        var usersList = Users();
        Assert.AreEqual(users.First().Nickname,Users().First().Nickname);
        Assert.AreEqual(users.Last().Nickname,Users().Last().Nickname);
        Assert.AreEqual(users.Count(),Users().Count());
    }
    [Test]
    [TestCase("222222222222222222222222")]
    [TestCase("333333333333333333333333")]
    [TestCase("111111111111111111111111")]
    public void BGetUserById_UserId_UserWithThatId(string id)
    {
        var user =_usersRepository.GetUserById(id);
        Assert.AreEqual(user.Id,id);
    }
    [Test]
    [TestCaseSource(nameof(UsersForAdd))]
    public void CAddUser_User_UserAddToDb(User user)
    {
        _usersRepository.NewUser(user);
        var users = _usersRepository.UsersList();
        Assert.AreEqual(user.Nickname,users.Last().Nickname);
    }
}