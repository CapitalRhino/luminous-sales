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
        private LuminousContext context;
        private RoleController rolectrl;
        private User currentUser;
        public UserController()
        {
            this.context = new LuminousContext();
        }
        public UserController(User currentUser)
        {
            this.currentUser = currentUser;
            this.context = new LuminousContext();
            this.rolectrl = new RoleController(currentUser);
        }
        public ICollection<User> GetAll()
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.User.ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public bool CheckIfUserEverCreated()
        {
            if (context.User.ToList().Any())
            {
                return false;
            }
            return true;
        }
        public User Get(int id)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.User.Find(id);
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public User Get(string name)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.User.FirstOrDefault(u => u.Name == name);
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
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
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.User.Where(u => u.Name.Contains(name)).ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void RegisterItem(string name, string password)
        {
            var user = new User(name, password, 1);
            context.User.Add(user);
            context.SaveChanges();
        }
        public void RegisterItem(string name, string password, int roleId)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void RegisterItem(string name, string password, string roleName)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateName(int id, string newName)
        {
            if (currentUser != null || currentUser.Id == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateName(string oldName, string newName)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdatePassword(int id, string newPassword)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdatePassword(string name, string newPassword)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateRole(int id, int RoleId)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateRole(int id, string roleName)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateRole(string name, int roleId)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateRole(string name, string roleName)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void Delete(int id)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void Delete(string name)
        {
            if (currentUser != null || currentUser.RoleId == 3)
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
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
    }
}