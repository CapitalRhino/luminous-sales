using NUnit.Framework;
using Moq;
using Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Models;
using Business.Business.UserManagment;
using Business.Business.UserManagment.Controllers;

namespace LuminousUnitTests
{
    public class Tests
    {
        private UserController userctrl;
        private RoleController rolectrl;
        private IQueryable<User> testUsers;
        [SetUp]
        public void Setup()
        {
            var mock = new Mock<DbSet<User>>();
            var mocka = new Mock<DbSet<User>>();
            testUsers = new List<User>
            {
                new User(){ Name = "Admin", Password = "adm123", RoleId = 1},
                new User(){ Name = "Goso", Password = "goso123", RoleId = 2},
                new User(){ Name = "Pesho", Password = "peso123", RoleId = 3},
            }.AsQueryable();
            mock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testUsers.Provider);
            mock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testUsers.Expression);
            mock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testUsers.ElementType);
            mock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(testUsers.GetEnumerator());
            var testContext = new Mock<LuminousContext>();
            testContext.Setup(s => s.User).Returns(mock.Object);

            userctrl = new UserController(testUsers.ToList()[0], testContext.Object);
        }

        [Test]
        public void UsersController_GetAll()
        {
            List<User> users = userctrl.GetAll().ToList();
            List<User> testUsersList = testUsers.ToList();
            Assert.AreEqual(users.Count, testUsers.Count());
            for (int i = 0; i < users.Count; i++)
            {
                Assert.AreEqual(users[i].Name, testUsersList[i].Name);
                Assert.AreEqual(users[i].Password, testUsersList[i].Password);
                Assert.AreEqual(users[i].RoleId, testUsersList[i].RoleId);
            }
        }
        [Test]
        public void UserController_AddItem()
        {
            userctrl.RegisterItem("Penka", "penka123", 3);
            List<User> users = userctrl.GetAll().ToList();
            Assert.AreEqual(users.Count, 4);
        }
    }
}