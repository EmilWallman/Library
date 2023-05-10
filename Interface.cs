using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using library;



namespace library
{

    

    public class Interface
    {
        
        public List<User> users;
        public User currentUser;
        string currentssn;


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
        
        public void AdminMenu(int currentSsn)
        {
            Console.WriteLine("1. Edit Users");
            Console.WriteLine("2. Edit library");
            Console.WriteLine("3. Logout");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    EditUsers(currentSsn);
                    Console.Clear();
                    break;

                case 2:
                    //Going to the library
                    Console.Clear();
                    Library library = new Library();
                    library.AdminLibrary(Convert.ToString(currentSsn));
                    
                    return;

                case 3:
                    Console.WriteLine("Logging out...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Start();
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Thread.Sleep(2000);
                    Console.Clear();

                    break;
            }
            AdminMenu(currentSsn);
        }

        public void UserMenu(string username, int currentSsn)
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
                    Thread.Sleep(2000);
                    Console.Clear();
                    Start();
                    return;
                case 2:
                    ChangePasword(username, currentSsn);
                    Thread.Sleep(2000);
                    Console.Clear();

                    return;
                case 3:

                    Console.Clear();
                    Library library = new Library();
                    library.UserLibrary(username, Convert.ToString(currentSsn));

                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
            }
            UserMenu(username, currentSsn);
        }

        public void EditUsers(int currentSsn)
        {
            Console.WriteLine("1. Change Status");
            Console.WriteLine("2. Change Username");
            Console.WriteLine("3. Change User SSN");
            Console.WriteLine("4. Change User Password");
            Console.WriteLine("5. Remove a User");
            Console.WriteLine("6. Add a User");
            Console.WriteLine("7. List all users");
            Console.WriteLine("8. Back");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    //Change status
                    
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
                    //Change username


                    ChangeUsername();

                    break;
                case 3:
                    //Change SSN

                    ChangeSSN();
                    break;

                case 4:
                    //Change password

                    ListAllUsers();
                    Console.Write("Enter the username: ");
                    string usernameChoise = Console.ReadLine();
                    ChangePasword(usernameChoise, currentSsn);

                    break;

                case 5:
                    //Remove user


                    DeleteUser();
                    break;

                case 6:
                    //Add user
                    CreateAccount();

                    break;

                case 7:
                    ListAllUsers();
                    break;

                case 8:
                    AdminMenu(currentSsn);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            EditUsers(currentSsn);
        }

        public void ChangeSSN()
        {
            Console.Clear();
            ListAllUsers();
            Console.Write("Enter the user: ");
            string username = Console.ReadLine();

            int i = 0;
            foreach (var user in users)
            {
                i++;
                if (user.username == username)
                {
                    Console.Write("Enter the new SSN: ");
                    string newSSN = Console.ReadLine();

                    Console.WriteLine();
                    Console.Clear();
                    user.ssn = newSSN;

                    SaveUsers();
                    Console.WriteLine($"SSN for {user.username} has been updated. ");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                }
                if (i == users.Count)
                {
                    Console.WriteLine($"User with uresname {user.username} does not exist. ");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;
                }
            }
        }

        public void ChangeUsername()
        {
            ListAllUsers();
            Console.Write("Enter the user: ");
            string username = Console.ReadLine();

            int i = 0;
            foreach (var user in users)
            {
                i++;
                if (user.username == username)
                {
                    Console.Write("Enter the new username: ");
                    string newUsername = Console.ReadLine();

                    Console.WriteLine();
                    Console.Clear();
                    user.username = newUsername;
                    
                    SaveUsers();
                    Console.WriteLine($"Username for {username} has been updated to {newUsername}");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                }
                if (i == users.Count)
                {
                    Console.WriteLine($"User with uresname {username} does not exist. ");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;
                }
            }
        }

        public void ChangePasword(string username, int currentSsn)
        {
            int i = 0;
            foreach (var user in users) 
            {
                i++;
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
                    break;
                }
                if (i == users.Count)
                {
                    Console.WriteLine($"User with username {username} does not exist.");
                    break;
                }

            }
            if (currentUser.isAdmin == true)
            {
                EditUsers(currentSsn);
            }
            else
            {
                UserMenu(username, currentSsn);
            }

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
                        AdminMenu(int.Parse(user.ssn));
                    }
                    else
                    {
                        Console.WriteLine("Welcome, " + username + ".");
                        UserMenu(username, int.Parse(user.ssn));
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

            Thread.Sleep(1000);

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

        private void DeleteUser()
        {
            ListAllUsers();
            Console.Write("Enter the username:");
            string questionUsername = Console.ReadLine();
            
            
            foreach (User user in users)
            {
                if (user.username == questionUsername)
                {
                    int i = 0;
                    while (i == 0)
                    {
                        Console.WriteLine("Write CONFIRM in big letters to confirm the removal of " + user.username);
                        string confirm = Console.ReadLine();

                        if (confirm == "CONFIRM")
                        {
                            users.Remove(user);
                            SaveUsers();

                            Console.WriteLine(questionUsername + " has been removed");
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Someting went wrong, write again och press X to go back");
                            confirm = Console.ReadLine();
                            if (confirm.ToLower() == "x")
                            {
                                i++;
                            }
                            else
                            {
                                
                            }
                        }
                    }
                    break;
                    
                }
                
            }

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

