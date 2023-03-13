using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

/*
To Do:
Admin kan byta lösenord
Lägga till Böcker
Ta bort Böcker
Låna Böcker
Lämna tillbaka Böker
Söka efter Böcker
Reservera Böcker
Lägg till Console.Clear
Lägg till Thread.Sleep
Koppla ihop två filer
 */

namespace library
{
    class User
    {
        public string username;
        public string password;
        public string ssn;
        public bool isAdmin;


        public User(string username, string password, string ssn, bool isAdmin)
        {
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
            this.ssn = ssn;
        }
    }
    

    class LoginSite
    {
        private List<User> users;
        private User currentUser;

        public LoginSite()
        {
            users = new List<User>();
            LoadUsers();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Login Site");

            while (true)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("3. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        CreateAccount();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        public string ChangePasword()
        {
            ListAllUsers();

            Console.WriteLine("What user do you want to change the password for?");
            string username = Console.ReadLine();

            

            foreach (var user in users) 
            { 
                if (user.username == username)
                {
                    Console.WriteLine("What is the new password?");
                    string newPassword = "";

                    //Making the password input invisible
                    ConsoleKeyInfo key;
                    do
                    {
                        key = Console.ReadKey(true);

                        // Backspace should remove the last character from the password
                        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                        {
                            newPassword += key.KeyChar;
                            Console.Write("*");
                        }
                        else if (key.Key == ConsoleKey.Backspace && newPassword.Length > 0)
                        {
                            newPassword = newPassword.Remove(newPassword.Length - 1);
                            Console.Write("\b \b");
                        }
                    } while (key.Key != ConsoleKey.Enter);

                    Console.WriteLine();

                    user.password = newPassword;
                    Console.WriteLine($"Password for {username} has been updated.");
                    
                    SaveUsers();

                    return newPassword;
                    

                }
            }
            Console.WriteLine($"User with username {username} does not exist.");
            return "1";
        }

        private void Login()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your Social Secutiry Number: ");
            string ssn = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = "";
            //To make the password only show "*"
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Backspace should remove the last character from the password
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();


            foreach (User user in users)
            {
                if (user.username == username && user.password == password && user.ssn == ssn)
                {
                    currentUser = user;
                    Console.WriteLine("Login successful.");
                    if (user.isAdmin)
                    {
                        Console.WriteLine("Welcome, Admin.");
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("Welcome, " + username + ".");
                        UserMenu();
                    }
                    return;
                }
            }

            Console.WriteLine("Incorrect username or password. Try again.");
        }

        private void CreateAccount()
        {
            Console.Write("Enter a username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your Social Secutiy Number: ");
            string ssn = Console.ReadLine();
            Console.Write("Enter a password: ");
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Backspace should remove the last character from the password
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();


            // check if a user with the same username or ssn already exists
            if (GetUserByUsername(username) != null)
            {
                Console.WriteLine("Username already exists. Try again.");
                return;
            }
            if (GetUserBySSN(ssn) != null)
            {
                Console.WriteLine("Social Security Number already exists. Try again.");
                return;
            }

            User user = new User(username, password, ssn, false);
            users.Add(user);
            SaveUsers();

            Console.WriteLine("Account created successfully.");


            Console.Clear();
        }

        private void ListAllUsers()
        {
            Console.WriteLine("List of all the users:");
            foreach(User user in users)
            {
                Console.WriteLine(user.username);
            }
        }

        private void AdminMenu()
        {
            Console.WriteLine("1. Change User Status");
            Console.WriteLine("2. Logout");
            Console.WriteLine("3. Edit library");
            Console.WriteLine("4. Change User Password");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ListAllUsers();
                    Console.Write("Enter the username:");
                    string username = Console.ReadLine();
                    User user = GetUserByUsername(username);
                    if (user != null)
                    {
                        user.isAdmin = !user.isAdmin;
                        Console.WriteLine("User status changed successfully to " + (user.isAdmin ? "Admin" : "Regular") + ".");
                        SaveUsers();
                    }

                    else
                    {
                        Console.WriteLine("User not found.");
                    }

                    break;
                case 2:
                    Console.WriteLine("Logging out...");
                    return;
                case 3:

                case 4:
                    

                    string newPassword = ChangePasword();
                    
                    

                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }



            AdminMenu();
        }

        private void UserMenu()
        {
            Console.WriteLine("1. Logout");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            UserMenu();
        }

        private User GetUserByUsername(string username)
        {
            foreach (User user in users)
            {
                if (user.username == username)
                {
                    return user;
                }
            }
            return null;
        }

        private User GetUserBySSN(string ssn)
        {
            foreach (User user in users)
            {
                if (user.ssn == ssn)
                {
                    return user;
                }
            }
            return null;
        }

        private void LoadUsers()
        {
            if (!File.Exists("users.txt"))
            {
                return;
            }

            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string username = parts[0];
                string password = parts[1];
                string ssn = parts[2];
                bool isAdmin = bool.Parse(parts[3]);

                User user = new User(username, password, ssn, isAdmin);
                users.Add(user);
            }
        }

        private void SaveUsers() { 
        
            List<string> lines = new List<string>();
            foreach (User user in users)
            {
                lines.Add(user.username + "," + user.password + "," + user.ssn + "," + user.isAdmin);
            }
            File.WriteAllLines("users.txt", lines);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            LoginSite loginSite = new LoginSite();
            loginSite.Start();
        }
    }

}

