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

        /// <summary>
        /// Empty Constructor
        /// </summary>
        /// <remarks>
        /// Used for Initialiation of the roles in the database
        /// </remarks>
        
        public UserController()
        {
            this.context = new LuminousContext();
        }

        /// <summary>
        /// Constructor that accepts a user object
        /// </summary>
        /// <remarks>
        /// User object is used for role checking
        /// </remarks>

        public UserController(User currentUser)
        {
            this.currentUser = currentUser;
            this.context = new LuminousContext();
            this.rolectrl = new RoleController(currentUser);
        }

        /// <summary>
        /// Constructor that accepts custom context and a user object
        /// </summary>
        /// <remarks>
        /// Custom context is mainly used for Unit Testing
        /// User object is used for role checking
        /// </remarks>

        public UserController(User currentUser, LuminousContext context)
        {
            this.currentUser = currentUser;
            this.context = context;
        }

        /// <summary>
        /// Gets All Users
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <returns>
        /// Returns a ICollection of all users.
        /// </returns>

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

        /// <summary>
        /// Checks if there's a user in the database
        /// </summary>
        /// <remarks>
        /// Can be used with an empty constructor
        /// </remarks>

        public bool CheckIfUserEverCreated()
        {
            if (context.User.ToList().Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches the user by given Id
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// </summary>
        /// <returns>
        /// Returns an object of the user with the given Id
        /// </returns>

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

        /// <summary>
        /// Searches the user by given name
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <returns>
        /// Returns an object of the user with the given name.
        /// </returns>

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

        /// <summary>
        /// Searches the user by a given substring
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <returns>
        /// Returns an ICollection of all users that contain the given substring in their name.
        /// </returns>

        public ICollection<User> GetByApproximateName(string substring)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.User.Where(u => u.Name.Contains(substring)).ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }

        /// <summary>
        /// Checks if the password is valid
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Password is used to log in the user.
        /// </remarks>
        /// <returns>
        /// Returns an object of the found user.
        /// </returns>

        public User ValidatePassword(string password)
        {
            var user = context.User.FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException("Invalid User!");
            }
            return user;
        }

        /// <summary>
        /// Registers an user
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Used for the creation of the initial user, so it assigns admin role by default.
        /// </remarks>

        public void RegisterItem(string name, string password)
        {
            var user = new User(name, password, 3);
            context.User.Add(user);
            context.SaveChanges();
        }

        /// <summary>
        /// Registers an user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an role id so it can assign a role to the user.
        /// </remarks>

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

        /// <summary>
        /// Registers an user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an role name so it can assign a role to the user.
        /// </remarks>

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

        /// <summary>
        /// Updates the username of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an id for getting the user.
        /// </remarks>

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

        /// <summary>
        /// Updates the username of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts the current name for getting the user.
        /// </remarks>

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

        /// <summary>
        /// Updates the password of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an id for getting the user.
        /// </remarks>

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

        /// <summary>
        /// Updates the password of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts the name for getting the user.
        /// </remarks>

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

        /// <summary>
        /// Updates the role of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an user id for getting the user. 
        /// Accepts an role id for getting the role. 
        /// </remarks>

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

        /// <summary>
        /// Updates the role of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an user id for getting the user. 
        /// Accepts an role name for getting the role. 
        /// </remarks>

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

        /// <summary>
        /// Updates the role of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an username for getting the user.
        /// Accepts an role id for getting the role.
        /// </remarks>

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

        /// <summary>
        /// Updates the role of the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an username for getting the user.
        /// Accepts an role name for getting the role.
        /// </remarks>

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

        /// <summary>
        /// Deletes the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an user id for getting the user.
        /// </remarks>

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

        /// <summary>
        /// Deletes the given user
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an username for getting the user.
        /// </remarks>

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