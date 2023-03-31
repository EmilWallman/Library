using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using library;

/*
To Do:

Ta Bort användare
Användare ska kunna ändra sina lösenord

Ta bort Böcker
Låna Böcker
Lämna tillbaka Böker
Reservera Böcker

Lägg till Console.Clear
Lägg till Thread.Sleep

Ändra alla namn till interface osv
En klass för AdminMenu
En klass för User
En klass för loggin o skapa konto osv (start meny)
En klass för Bibliotek

Ta bort filer som inte används

*/

namespace library
{

    

    public class Interface
    {
        
        public List<User> users;
        public User currentUser;



        public Interface()
        {
            users = new List<User>();
            LoadUsers();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Library");

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
                        Console.Clear();
                        Login();
                        break;
                    case 2:
                        Console.Clear();
                        CreateAccount();
                        break;
                    case 3:
                        Console.Clear();
                        System.Environment.Exit(0);
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
        public void EditUsers()
        {
            Console.WriteLine("1. Change User Status");
            Console.WriteLine("2. Change User Password");
            Console.WriteLine("3. Back");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ListAllUsers();
                    Console.Write("Enter the username:");
                    string questionUsername = Console.ReadLine();
                    User user = GetUserByUsername(questionUsername);
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
                    ListAllUsers();
                    Console.WriteLine("What user do you want to change the password for?");
                    string usernameChoise = Console.ReadLine();
                    ChangePasword(usernameChoise);
                    return;
                case 3:
                    AdminMenu();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            EditUsers();
        }
        public void AdminMenu()
        {

            Console.WriteLine("1. Edit Users");
            Console.WriteLine("2. Edit library");
            Console.WriteLine("3. Logout");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    EditUsers();
                    break;

                case 2:
                    string ssn = currentUser.ssn;
                    //Going to the library
                    Library library = new Library();
                    library.AdminLibrary(ssn);
                    return;

                case 3:
                    Console.WriteLine("Logging out...");
                    Start();
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            AdminMenu();
        }

        public void ChangePasword(string username)
        {
            
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
                }
            }
            Console.WriteLine($"User with username {username} does not exist.");
        }

        private void Login()
        {
            Console.Clear();
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
            Console.Clear();
            for(int i = 0; i < 4; i++)
            {
                Console.Write("Logging in");
                Thread.Sleep(300);
                Console.Write(".");
                Thread.Sleep(300);
                Console.Write(".");
                Thread.Sleep(300);
                Console.Write(".");
                Thread.Sleep(300);
                Console.Clear();
                i++;
            }
            


            foreach (User user in users)
            {
                if (user.username == username && user.password == password && user.ssn == ssn)
                {
                    currentUser = user;
                    Console.WriteLine("Login successful.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    if (user.isAdmin)
                    {
                        Console.WriteLine("Welcome, Admin.");
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("Welcome, " + username + ".");
                        UserMenu(username);
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

        

        //Denna e TOM LÖS DE FÖRFAN
        private void DeleteUser()
        {
            
        }

        private void UserMenu(string username)
        {
            Console.WriteLine("1. Logout");
            Console.WriteLine("2. Change password");
            Console.WriteLine("3. Enter the Library");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Logging out...");
                    return;
                case 2:
                    ChangePasword(username);
                    return;
                case 3:
                    string ssn = currentUser.ssn;
                    Library library = new Library();
                    library.UserLibrary(ssn);

                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            UserMenu(username);
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

        public void LoadUsers()
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
}

