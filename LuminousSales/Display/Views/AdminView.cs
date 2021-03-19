using Business.Business.Sales;
using Business.Business.UserManagment;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Display.Views
{
    public class AdminView : ManagerView
    {
        UserController userctl = new UserController();
        public AdminView(User currentUser) : base(currentUser)
        {

        }
        public override void ShowAvaliableCommands()
        {
            base.ShowAvaliableCommands();
            Console.WriteLine("3. User Managment");
        }
        public void GetAllUsers()
        {
            try
            {
                Console.WriteLine("Getting all users...");
                foreach (var item in userctl.GetAll())
                {
                    Console.WriteLine($"{item.Id} {item.Name} {item.Role} ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Get()
        {
            try
            {
                Console.WriteLine("Getting user...");
                Console.Write("Get user by ID or Name: ");
                string input = Console.ReadLine();
                int.TryParse(input, out int inputId);
                if (inputId != 0)
                {
                    Console.WriteLine(userctl.Get(inputId));
                }
                else
                {
                    Console.WriteLine(userctl.Get(input));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void GetByApproximateName()
        {
            try
            {
                Console.WriteLine("Getting by name...");
                Console.Write("Enter name: ");
                string input = Console.ReadLine();
                foreach (var item in userctl.GetByApproximateName(input))
                {
                    Console.WriteLine($"{item.Id} {item.Name} {item.Role}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void RegisterItem()
        {
            try
            {
                Console.WriteLine("Registering user...");
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                Console.Write("Enter role ID or name (default 1, Cashier): ");
                string role = Console.ReadLine();
                bool result = int.TryParse(role, out int roleId);
                if (role == null)
                {
                    userctl.RegisterItem(username, password);
                }
                else if (result)
                {
                    userctl.RegisterItem(username, password, roleId);
                }
                else
                {
                    userctl.RegisterItem(username, password, role);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdateRole()
        {
            try
            {
                Console.WriteLine("Updating role...");
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter new role ID or name: ");
                string role = Console.ReadLine();
                bool result = int.TryParse(role, out int roleId);
                if (result)
                {
                    userctl.UpdateRole(username, roleId);
                }
                else
                {
                    userctl.UpdateRole(username, role);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdateName()
        {
            try
            {
                Console.WriteLine("Updating username...");
                Console.Write("Enter username or ID: ");
                string user = Console.ReadLine();
                Console.Write("Enter new username: ");
                string username = Console.ReadLine();
                bool result = int.TryParse(user, out int userId);
                if (result)
                {
                    userctl.UpdateName(userId, username);
                }
                else
                {
                    userctl.UpdateRole(user, username);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdatePassword()
        {
            try
            {
                Console.WriteLine("Updating password...");
                Console.Write("Enter username or ID: ");
                string user = Console.ReadLine();
                Console.Write("Enter new password: ");
                string username = Console.ReadLine();
                bool result = int.TryParse(user, out int userId);
                if (result)
                {
                    userctl.UpdatePassword(userId, username);
                }
                else
                {
                    userctl.UpdatePassword(user, username);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Delete()
        {
            try
            {
                Console.WriteLine("Deleting user...");
                Console.Write("Enter username or ID: ");
                string user = Console.ReadLine();
                bool result = int.TryParse(user, out int userId);
                if (result)
                {
                    userctl.Delete(userId);
                }
                else
                {
                    userctl.Delete(user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
