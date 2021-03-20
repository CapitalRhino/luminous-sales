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
using System;

namespace LuminousUnitTests
{
    public class Tests
    {
        private UserController userctrl;
        private RoleController rolectrl;
        private Mock<LuminousContext> testContext;
        private Mock<DbSet<User>> UserMock;
        private Mock<DbSet<Role>> RoleMock;
        private IQueryable<User> testUsers;
        private IQueryable<Role> testRoles;
        [SetUp]
        public void Setup()
        {
            UserMock = new Mock<DbSet<User>>();
            RoleMock = new Mock<DbSet<Role>>();
            testUsers = new List<User>
            {
                new User(){ Name = "Admin", Password = "adm123", RoleId = 1},
                new User(){ Name = "Goso", Password = "goso123", RoleId = 2},
                new User(){ Name = "Pesho", Password = "peso123", RoleId = 3},
            }.AsQueryable();
            testRoles = new List<Role>
            {
                new Role { Name = "Cashier"},
                new Role { Name = "Manager"},
                new Role { Name = "Admin" }
            }.AsQueryable();

            UserMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testUsers.Provider);
            UserMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testUsers.Expression);
            UserMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testUsers.ElementType);
            UserMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(testUsers.GetEnumerator());

            RoleMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testUsers.Provider);
            RoleMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testUsers.Expression);
            RoleMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testUsers.ElementType);
            RoleMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(testUsers.GetEnumerator());

            testContext = new Mock<LuminousContext>();
            testContext.Setup(s => s.User).Returns(UserMock.Object);
            testContext.Setup(s => s.Role).Returns(RoleMock.Object);

            userctrl = new UserController(testUsers.ToList()[0], testContext.Object);
            rolectrl = new RoleController(testContext.Object);
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
            rolectrl.CreateInitialRoles();
            RoleMock.Verify(m => m.AddRange(It.IsAny<Role[]>()));
            testContext.Verify(m => m.SaveChanges());
            userctrl.RegisterItem("Penka", "penka123", 3);
            UserMock.Verify(m => m.Add(It.IsAny<User>()));
            testContext.Verify(m => m.SaveChanges());
        }
        public void UserController_DeleteUser()
        {
            userctrl.Delete(1);
            UserMock.Verify(m => m.Remove(It.IsAny<User>()));
        }
    }
}