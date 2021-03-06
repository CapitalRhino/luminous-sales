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
        User currentUser;

        /// <summary>
        /// Constructor that accepts a user object.
        /// <summary>
        public AdminView(User currentUser) : base(currentUser)
        {
            this.currentUser = currentUser;
        }
        /// <summary>
        /// Shows the avaliable to the user commands.
        /// </summary>
        /// <remarks>
        /// The main menu. Inherited from ManagerView and adds Admin-specific menu.
        /// </remarks>
        public override void ShowAvaliableCommands()
        {
            base.ShowAvaliableCommands();
            Console.WriteLine("3. Administration");
        }
        /// <summary>
        /// Asks the user to choose which group of action to use.
        /// </summary>
        /// <remarks>
        /// A choice is given by entering a number from the given list.
        /// Inherited from ManagerView and expanded with the Admin menu.
        /// </remarks>
        public override void ActionHandle()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
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
        /// <summary>
        /// Selection menu with admin actions.
        /// </summary>
        /// <remarks>
        /// Requires role level 3 (Admin).
        /// </remarks>
        public void AdminHandle()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== ADMINISTRATION ===");
                Console.WriteLine("0. Back");
                Console.WriteLine();
                Console.WriteLine("User Managment");
                Console.WriteLine("1. List all users");
                Console.WriteLine("2. Get user by id or name");
                Console.WriteLine("3. List users by name");
                Console.WriteLine("4. Register user");
                Console.WriteLine("5. Update role");
                Console.WriteLine("6. Update username");
                Console.WriteLine("7. Update password");
                Console.WriteLine("8. Delete user");
                Console.WriteLine();
                Console.WriteLine("Product Managment");
                Console.WriteLine("9. Add product");
                Console.WriteLine("10. List all products");
                Console.WriteLine("11. Delete product");
                Console.WriteLine("12. Update product price");
                Console.WriteLine("13. Add amount of a product");
                Console.WriteLine("14. Remove amount of a product");
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
                            GetUser();
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
                            DeleteUser();
                            break;
                        case 9:
                            AddItem();
                            break;
                        case 10:
                            GetAllItems();
                            break;
                        case 11:
                            DeleteProduct();
                            break;
                        case 12:
                            UpdateProductPrice();
                            break;
                        case 13:
                            AddProductAmount();
                            break;
                        case 14:
                            RemoveProductAmount();
                            break;
                        case 0:
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
        /// <summary>
        /// Lists all products from the database.
        /// </summary>
        private void GetAllItems()
        {
            try
            {
                ProductController productctrl = new ProductController(currentUser);
                Console.WriteLine("Getting all items...");
                Console.WriteLine("ID - Name - Amount - Price");
                foreach (var item in productctrl.GetAll())
                {
                    Console.WriteLine($"{item.Id} - {item.Name} - {item.AmountInStock} - {item.Price} ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Lists all registered users from the database.
        /// </summary>
        public void GetAllUsers()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
                Console.WriteLine("Getting all users...");
                Console.WriteLine("User ID - Username - Role");
                foreach (var item in userctl.GetAll())
                {
                    Console.WriteLine($"{item.Id} - {item.Name} - {item.Role.Name} ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Lists info about a user using their ID or name from the database.
        /// </summary>
        public void GetUser()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
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

        /// <summary>
        /// Lists all users who match the search term from the database.
        /// </summary>
        public void GetByApproximateName()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
                Console.WriteLine("Getting by name...");
                Console.Write("Enter name: ");
                string input = Console.ReadLine();
                foreach (var item in userctl.GetByApproximateName(input))
                {
                    Console.WriteLine($"{item.Id} {item.Name} {item.Role.Name}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Registers a user using the provided data.
        /// </summary>
        public void RegisterItem()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
                Console.WriteLine("Registering user...");
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                Console.WriteLine("Avaliable roles: 1 - Cashier, 2 - Manager, 3 - Admin");
                Console.Write("Enter role ID or name: ");
                string role = Console.ReadLine();
                bool result = int.TryParse(role, out int roleId);
                if (role == null)
                {
                    userctl.RegisterItem(username, password);
                    Console.WriteLine("Registered user successfully");
                }
                else if (result)
                {
                    userctl.RegisterItem(username, password, roleId);
                    Console.WriteLine("Registered user successfully");
                }
                else
                {
                    userctl.RegisterItem(username, password, role);
                    Console.WriteLine("Registered user successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Changes the role given to a specific user.
        /// </summary>
        /// <remarks>
        /// Asks for a user's ID or name and the new role ID or name.
        /// </remarks>
        public void UpdateRole()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
                Console.WriteLine("Updating role...");
                Console.Write("Enter username or user ID: ");
                string username = Console.ReadLine();
                Console.Write("Enter new role ID or name: ");
                string role = Console.ReadLine();
                bool userResult = int.TryParse(username, out int userId);
                bool roleResult = int.TryParse(role, out int roleId);
                if (userResult && roleResult)
                {
                    userctl.UpdateRole(userId, roleId);
                }
                else if (userResult && !roleResult)
                {
                    userctl.UpdateRole(userId, role);
                }
                else if (!userResult && roleResult)
                {
                    userctl.UpdateRole(username, roleId);
                }
                else
                {
                    userctl.UpdateRole(username, role);
                }
                Console.WriteLine("Updated role successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Renames a user.
        /// </summary>
        public void UpdateName()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
                Console.WriteLine("Updating username...");
                Console.Write("Enter username or ID: ");
                string user = Console.ReadLine();
                Console.Write("Enter new username: ");
                string username = Console.ReadLine();
                bool result = int.TryParse(user, out int userId);
                if (result)
                {
                    userctl.UpdateName(userId, username);
                    Console.WriteLine("Updated username successfully");
                }
                else
                {
                    userctl.UpdateName(user, username);
                    Console.WriteLine("Updated username successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Changes a users' password.
        /// </summary>
        public void UpdatePassword()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
                Console.WriteLine("Updating password...");
                Console.Write("Enter username or ID: ");
                string user = Console.ReadLine();
                Console.Write("Enter new password: ");
                string username = Console.ReadLine();
                bool result = int.TryParse(user, out int userId);
                if (result)
                {
                    userctl.UpdatePassword(userId, username);
                    Console.WriteLine("Updated password successfully");
                }
                else
                {
                    userctl.UpdatePassword(user, username);
                    Console.WriteLine("Updated password successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        public void DeleteUser()
        {
            try
            {
                UserController userctl = new UserController(currentUser);
                Console.WriteLine("Deleting user...");
                Console.Write("Enter username or ID: ");
                string user = Console.ReadLine();
                bool result = int.TryParse(user, out int userId);
                if (result)
                {
                    userctl.Delete(userId);
                    Console.WriteLine("Deleted successfully");
                }
                else
                {
                    userctl.Delete(user);
                    Console.WriteLine("Deleted successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Adds a product to the database.
        /// </summary>
        public void AddItem()
        {
            ProductController productctrl = new ProductController(currentUser);
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

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        public void DeleteProduct()
        {
            ProductController productctrl = new ProductController(currentUser);
            try
            {
                Console.WriteLine("Delete item to database...");
                Console.Write("Enter product ID or name: ");
                string product = Console.ReadLine();
                bool result = int.TryParse(product, out int productId);
                if (result)
                {
                    productctrl.Delete(productId);
                    Console.WriteLine("Deleted successfully");
                }
                else
                {
                    productctrl.Delete(product);
                    Console.WriteLine("Deleted successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Updates a product's price from the database.
        /// </summary>
        public void UpdateProductPrice()
        {
            ProductController productctrl = new ProductController(currentUser);
            try
            {
                Console.WriteLine("Updating item in database...");
                Console.Write("Enter product ID or name: ");
                string product = Console.ReadLine();
                Console.Write("Enter new price: ");
                double price = double.Parse(Console.ReadLine());
                bool result = int.TryParse(product, out int productId);
                if (result)
                {
                    productctrl.UpdatePrice(product, price);
                    Console.WriteLine("Deleted successfully");
                }
                else
                {
                    productctrl.UpdatePrice(productId, price);
                    Console.WriteLine("Deleted successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Updates a product's name from the database.
        /// </summary>
        public void UpdateProductName()
        {
            ProductController productctrl = new ProductController(currentUser);
            try
            {
                Console.WriteLine("Updating item in database...");
                Console.Write("Enter product ID or name: ");
                string product = Console.ReadLine();
                Console.Write("Enter new name: ");
                string name = Console.ReadLine();
                bool result = int.TryParse(product, out int productId);
                if (result)
                {
                    productctrl.UpdateName(product, name);
                    Console.WriteLine("Deleted successfully");
                }
                else
                {
                    productctrl.UpdateName(productId, name);
                    Console.WriteLine("Deleted successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Adds amount to a product's amount from the database.
        /// </summary>
        public void AddProductAmount()
        {
            ProductController productctrl = new ProductController(currentUser);
            try
            {
                Console.WriteLine("Updating item in database...");
                Console.Write("Enter product ID or name: ");
                string product = Console.ReadLine();
                Console.Write("Add amount: ");
                double amount = double.Parse(Console.ReadLine());
                bool result = int.TryParse(product, out int productId);
                if (result)
                {
                    productctrl.AddAmount(product, amount);
                    Console.WriteLine("Deleted successfully");
                }
                else
                {
                    productctrl.AddAmount(productId, amount);
                    Console.WriteLine("Deleted successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Removes amount to a product's amount from the database.
        /// </summary>
        public void RemoveProductAmount()
        {
            ProductController productctrl = new ProductController(currentUser);
            try
            {
                Console.WriteLine("Updating item in database...");
                Console.Write("Enter product ID or name: ");
                string product = Console.ReadLine();
                Console.Write("Remove amount: ");
                double amount = double.Parse(Console.ReadLine());
                bool result = int.TryParse(product, out int productId);
                if (result)
                {
                    productctrl.RemoveAmount(product, amount);
                    Console.WriteLine("Deleted successfully");
                }
                else
                {
                    productctrl.RemoveAmount(productId, amount);
                    Console.WriteLine("Deleted successfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
