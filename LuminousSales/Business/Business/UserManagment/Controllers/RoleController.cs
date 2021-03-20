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
        /// Empty Constructor
        /// </summary>
        /// <remarks>
        /// Used for Initialiation of the roles in the database
        /// </remarks>
        
        public RoleController(){}

        /// <summary>
        /// Constructor that accepts a user object
        /// </summary>
        /// <remarks>
        /// User object is used for role checking
        /// </remarks>
        
        public RoleController(User currentUser)
        {
            this.context = new LuminousContext();
            this.currentUser = currentUser;
        }

        /// <summary>
        /// Constructor that accepts custom context and a user object
        /// </summary>
        /// <remarks>
        /// Custom context is mainly used for Unit Testing
        /// User object is used for role checking
        /// </remarks>
        
        public RoleController(LuminousContext context, User currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }
        
        /// <summary>
        /// Creates the roles
        /// </summary>
        /// <remarks>
        /// Almost every method of each class checks if the user has suffficient roles for the task
        /// </remarks>
        
        public void CreateInitialRoles()
        {
            var Admin = new Role("Admin");
            var Manager = new Role("Manager");
            var Cashier = new Role("Cashier");
            context.Role.AddRange(Admin, Manager, Cashier);
            context.SaveChanges();
        }
        
        /// <summary>
        /// Gets All Roles
        /// </summary>
        /// <returns>
        /// Returns a ICollection of all roles
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
        /// <returns>
        /// Returns an object of the role with the given Id
        /// 
        /// Requires Admin role.
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
        /// <returns>
        /// Returns an object of the role with the given name
        /// 
        /// Requires Admin role.
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
        /// <returns>
        /// Returns an ICollection of all roles that contain the given substring in their name.
        /// 
        /// Requires Admin role.
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
