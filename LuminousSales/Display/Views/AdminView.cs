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
            Console.WriteLine("3. Administration");
        }
        public override void ActionHandle()
        {
            try
            {
                while (true)
                {
                    ShowAvaliableCommands();
                    Console.Write("> ");
                    int input = int.Parse(Console.ReadLine());
                    if (input == 0)
                    {
                        Environment.Exit(0);
                    }
                    else if (input == 1)
                    {
                        SaleHandle();
                    }
                    else if (input == 2)
                    {
                        ManageHandle();
                    }
                    else if (input == 3)
                    {
                        AdminHandle();
                    }
                    else Console.WriteLine("Invalid operation");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AdminHandle()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("User Managment");
                Console.WriteLine("1. GetAll");
                Console.WriteLine("2. Get");
                Console.WriteLine("3. GetByApproximateName");
                Console.WriteLine("4. Register user");
                Console.WriteLine("5. Update Role");
                Console.WriteLine("6. Update username");
                Console.WriteLine("7. UpdatePassword");
                Console.WriteLine("8. Delete User");
                Console.WriteLine();
                Console.WriteLine("Product Managment");
                Console.WriteLine("9. AddItem");
                Console.WriteLine();
                Console.WriteLine("10. Back");
                Console.Write("> ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            GetAllUsers();
                            break;
                        case 2:
                            Get();
                            break;
                        case 3:
                            GetByApproximateName();
                            break;
                        case 4:
                            RegisterItem();
                            break;
                        case 5:
                            UpdateRole();
                            break;
                        case 6:
                            UpdateName();
                            break;
                        case 7:
                            UpdatePassword();
                            break;
                        case 8:
                            Delete();
                            break;
                        case 9:
                            AddItem();
                            break;
                        case 10:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Option!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

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
        public void AddItem()
        {
            try
            {
                Console.WriteLine("Adding item to database...");
                Console.Write("Enter product name: ");
                string product = Console.ReadLine();
                Console.Write("Enter price: ");
                double price = double.Parse(Console.ReadLine());
                productctrl.AddItem(product, price);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
