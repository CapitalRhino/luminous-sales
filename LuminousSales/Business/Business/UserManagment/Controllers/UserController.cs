using Business.Business.UserManagment.Controllers;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Business.UserManagment
{
    public class UserController : IController<User>
    {
        private LuminousContext context = new LuminousContext();
        private RoleController rolectrl = new RoleController();
        public ICollection<User> GetAll()
        {
            return context.User.ToList();
        }
        public void CheckIfUserEverCreated()
        {
            if (!GetAll().Any())
            {
                throw new ArgumentException("No users in the database!");
            }
        }
        public User Get(int id)
        {
            return context.User.Find(id);
        }
        public User Get(string name)
        {
            return context.User.FirstOrDefault(u => u.Name == name);
        }
        public User ValidatePassword(string password)
        {
            var user = context.User.FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException("Invalid User!");
            }
            return user;
        }
        public ICollection<User> GetByApproximateName(string name)
        {
            return context.User.Where(u => u.Name.Contains(name)).ToList();
        }
        public void RegisterItem(string name, string password, int roleId)
        {
            if (GetAll().Where(u => u.Name == name).Any())
            {
                throw new ArgumentException("The username is already taken!");
            }
            else if (GetAll().Where(u => u.Password == password).Any())
            {
                throw new ArgumentException("The password is already taken"!);
            }
            else
            {
                var foundRole = rolectrl.Get(roleId);
                if (foundRole != null)
                {
                    var user = new User(name, password, roleId);
                    context.User.Add(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Role not found!");
                }
            }
        }
        public void RegisterItem(string name, string password, string roleName)
        {
            if (GetAll().Where(u => u.Name == name).Any())
            {
                throw new ArgumentException("The username is already taken!");
            }
            else if (GetAll().Where(u => u.Password == password).Any())
            {
                throw new ArgumentException("The password is already taken"!);
            }
            else
            {
                var foundRole = rolectrl.Get(roleName);
                if (foundRole != null)
                {
                    var user = new User(name, password, foundRole.Id);
                    context.User.Add(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Role not found!");
                }
            }
        }
        public void UpdateName(int id, string newName)
        {
            var user = Get(id);
            if (user != null)
            {
                if (user.Name != newName)
                {
                    user.Name = newName;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Usernames match. Please choose another username!");
                }
            }
            else
            {
                throw new ArgumentException("No user with such id");
            }
        }
        public void UpdateName(string oldName, string newName)
        {
            if (oldName != newName)
            {
                var user = Get(oldName);
                if (user != null)
                {
                    user.Name = newName;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("No user with such name!");
                }
            }
            else
            {
                throw new ArgumentException("Usernames match. Please use another username!");
            }

        }
        public void UpdatePassword(int id, string newPassword)
        {
            var user = Get(id);
            if (user != null)
            {
                if (user.Password != newPassword)
                {
                    user.Password = newPassword;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Passwords match! Please use another password!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void UpdatePassword(string name, string newPassword)
        {
            var user = Get(name);
            if (user != null)
            {
                if (user.Password != newPassword)
                {
                    user.Password = newPassword;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Passwords match! Please use another password!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void UpdateRole(int id, int RoleId)
        {
            var user = Get(id);
            if (user != null)
            {
                var foundRole = rolectrl.Get(RoleId);
                if (foundRole != null)
                {
                    user.RoleId = RoleId;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Role not found!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void UpdateRole(int id, string roleName)
        {
            var user = Get(id);
            if (user != null)
            {
                var foundRole = rolectrl.Get(roleName);
                if (foundRole != null)
                {
                    user.RoleId = foundRole.Id;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Role not found!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void UpdateRole(string name, int roleId)
        {
            var user = Get(name);
            if (user != null)
            {
                var foundRole = rolectrl.Get(roleId);
                if (foundRole != null)
                {
                    user.RoleId = roleId;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Role not found!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void UpdateRole(string name, string roleName)
        {
            var user = Get(name);
            if (user != null)
            {
                var foundRole = rolectrl.Get(roleName);
                if (foundRole != null)
                {
                    user.RoleId = foundRole.Id;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Role not found!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void Delete(int id)
        {
            var user = Get(id);
            if (user != null)
            {
                context.User.Remove(user);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void Delete(string name)
        {
            var user = Get(name);
            if (user != null)
            {
                context.User.Remove(user);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
    }
}