using Business.Business.Interfaces;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Business.UserManagment.Controllers
{
    public class RoleController : IReadOnlyController<Role>
    {
        private LuminousContext context;
        private User currentUser;

        /// <summary>
        /// Empty Constructor.
        /// </summary>
        /// <remarks>
        /// Used for Initialiation of the roles in the database.
        /// </remarks>
        
        public RoleController()
        {
            this.context = new LuminousContext();
        }

        public RoleController(LuminousContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Constructor that accepts a user object.
        /// </summary>
        /// <remarks>
        /// User object is used for role checking.
        /// </remarks>
        
        public RoleController(User currentUser)
        {
            this.context = new LuminousContext();
            this.currentUser = currentUser;
        }

        /// <summary>
        /// Constructor that accepts custom context and a user object.
        /// </summary>
        /// <remarks>
        /// Custom context is mainly used for Unit Testing.
        /// </remarks>
        /// <remarks>
        /// User object is used for role checking.
        /// </remarks>

        public RoleController(User currentUser, LuminousContext context)
        {
            this.context = context;
            this.currentUser = currentUser;
        }

        /// <summary>
        /// Creates the roles.
        /// </summary>
        /// <remarks>
        /// Requires no special roles. Not even an registered user.
        /// </remarks>
        /// <remarks>
        /// Almost every method of each class checks if the user has suffficient roles for the task.
        /// </remarks>

        public void CreateInitialRoles()
        {
            var Cashier = new Role("Cashier");
            var Manager = new Role("Manager");
            var Admin = new Role("Admin");
            context.Role.AddRange(Cashier, Manager, Admin);
            context.SaveChanges();
        }
        
        /// <summary>
        /// Gets All Roles.
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <returns>
        /// Returns a ICollection of all roles.
        /// </returns>
        
        public ICollection<Role> GetAll()
        {
            if (currentUser.RoleId == 3)
            {
                return context.Role.ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }

        /// <summary>
        /// Searches the role by given Id
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <returns>
        /// Returns an object of the role with the given Id.
        /// </returns>

        public Role Get(int id)
        {
            if (currentUser.RoleId == 3)
            {
                return context.Role.Find(id);
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }

        /// <summary>
        /// Searches the role by given name
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <returns>
        /// Returns an object of the role with the given name.
        /// </returns>

        public Role Get(string name)
        {
            if (currentUser.RoleId == 3)
            {
                return context.Role.FirstOrDefault(u => u.Name == name);
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }

        /// <summary>
        /// Searches the role by a given substring
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <returns>
        /// Returns an ICollection of all roles that contain the given substring in their name.
        /// </returns>

        public ICollection<Role> GetByApproximateName(string substring)
        {
            if (currentUser.RoleId == 3)
            {
                return context.Role.Where(u => u.Name.Contains(substring)).ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
    }
}
