﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using library;
using System.Runtime.InteropServices;
using System.IO.Pipes;
using System.Collections.Immutable;
using System.Runtime.ExceptionServices;
using System.ComponentModel.Design;
using System.Threading;
using System.ComponentModel;



namespace library
{

    public class Library
    {

        public List<Book> books;
        public List<Queue> queues;
        public List<Copy> copies;

        public Library()
        {
            queues = new List<Queue>();
            books = new List<Book>();
            copies = new List<Copy>();
            LoadCopies();
            LoadBooks();
        }

        public void AdminLibrary(string ssn)
        {
            string currentSsn = ssn;
            Console.WriteLine("1. Add Books");
            Console.WriteLine("2. Delete Books");
            Console.WriteLine("3. List all books");
            Console.WriteLine("4. Lend Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Search for Books");
            Console.WriteLine("7. Back");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    //Adds a book to the library, if it already exists it creates a copy
                    AddBooks();
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case 2:
                    DeleteBooks();
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case 3:
                    //List all books
                    ListAllBooks();
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case 4:
                    //Lend a book and if it is already lended to someone it puts you in a queue for all the copies
                    LendBook(currentSsn);
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case 5:
                    //Return Book
                    ReturnBook(currentSsn);
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case 6:
                    Console.Write("Enter the book you want to search for: ");
                    string search = Console.ReadLine();

                    List<Book> matchedBooks = SearchBooks(search);

                    if (matchedBooks.Count == 0)
                    {
                        Console.WriteLine("No books found matching the search query.");
                    }
                    else
                    {
                        Console.WriteLine("{0} books found matching the search query:", matchedBooks.Count);

                        int numOfCopies = 0;
                        foreach (Book book in matchedBooks)
                        {
                            
                            foreach (Copy copy in copies)
                            {
                                if(copy.id == book.id)
                                {
                                    numOfCopies++;
                                }
                            }

                            Console.WriteLine("Title: {0}\nAuthor: {1}\nGenre: {2}\nNumber Of Copies: " + numOfCopies, book.title, book.author, book.genre, numOfCopies);
                        }
                    }
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();

                    break;
                    
                case 7:

                    Console.Clear();
                    Interface loginSite = new Interface();
                    loginSite.AdminMenu(int.Parse(ssn));

                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
            }
            AdminLibrary(ssn);
        }

        public void UserLibrary(string username, string ssn)
        {
            
            Console.WriteLine("1. List all books");
            Console.WriteLine("2. Lend Book");
            Console.WriteLine("3. Return Book");
            Console.WriteLine("4. Search for Books");
            Console.WriteLine("5. Back");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ListAllBooks();
                    break;

                case 2:
                    LendBook(ssn);
                    break;
                
                case 3: 
                    ReturnBook(ssn);
                    break;
                
                case 4:
                    Console.Write("Enter the book you want to search for: ");
                    string search = Console.ReadLine();

                    List<Book> matchedBooks = SearchBooks(search);

                    if (matchedBooks.Count == 0)
                    {
                        Console.WriteLine("No books found matching the search query.");
                    }
                    else
                    {
                        Console.WriteLine("{0} books found matching the search query:", matchedBooks.Count);

                        int numOfCopies = 0;
                        foreach (Book book in matchedBooks)
                        {

                            foreach (Copy copy in copies)
                            {
                                if (copy.id == book.id)
                                {
                                    numOfCopies++;
                                }
                            }

                            Console.WriteLine("Title: {0}\nAuthor: {1}\nGenre: {2}\nNumber Of Copies: {3}\n ", book.title, book.author, book.genre, numOfCopies);
                        }
                    }

                    break;
                
                case 5:
                    Interface loginSite = new Interface();
                    loginSite.UserMenu(username, int.Parse(ssn));
                    break;
                
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            UserLibrary(username, ssn);
        }

        
        public void LendBook(string ssn)
        {
            int ssnUser = Int32.Parse(ssn);
            Console.Write("Enter the book you want to search for: ");
            string search = Console.ReadLine();

            List<Book> matchedBooks = SearchBooks(search);

            if (matchedBooks.Count == 0)
            {
                Console.WriteLine("No books found matching the search query.");
            }
            //add if the book is avalible or not by looking in the copies
            else
            {
                Console.WriteLine("{0} books found matching the search query:", matchedBooks.Count);
                foreach (Book book in matchedBooks)
                {
                    
                    Console.WriteLine("Title: {0}\nAuthor: {1}\nGenre:  {2}\nID: {3}", book.title, book.author, book.genre, book.id);
                }
            }

            Console.Write("What book do you want (ID)?: ");
            int lendbook = Convert.ToInt32(Console.ReadLine());

            //Check if book is avalible

            int bookId = -1;
            string title = "";
            foreach(Book book in books)
            {
                if (book.id == lendbook)
                {
                    bookId = book.id;
                    title = book.title;
                }
            }

            bool avalible = false;
            int numberOfCopies = 0;
            if (1 == 1)
            {
                foreach(Copy copy in copies)
                {
                    if(copy.id == bookId) 
                    {
                        numberOfCopies++;
                    }
                }
            }


            int i = 0;
            int y = 0;
            foreach (Copy copy in copies)
            {
                y++;
                if (copy.id == bookId)
                {

                    i++;
                    if (copy.ssn == -1)
                    {
                        avalible = true;
                        Console.WriteLine("The book " + title + " is avalible.");
                        Console.WriteLine("Would you like to lend it? y/n");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() == "y")
                        {
                            copy.ssn = ssnUser;
                            SaveCopies();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Okay taking you back to the menu");
                            break;
                        }
                    }
                    
                    //If all copies are occupied
                    if (i == numberOfCopies)
                    {
                        Console.WriteLine("The book " + lendbook + " is not avalible right now.");
                        Console.WriteLine("Would you like to reserve it? y/n");
                        string answer = Console.ReadLine();
                        if(answer.ToLower() == "y")
                        {
                            
                            if (File.Exists("queue_" + copy.id + ".txt"))
                            {
                                LoadQueue(copy.id);
                                Queue queue = new Queue(copy.id, ssnUser);
                                queues.Add(queue);
                                SaveQueue(copy.id);
                                
                            }
                            if(!File.Exists("queue_" + copy.id + ".txt"))
                            {
                                var myFile = File.Create("queue_" + bookId + ".txt");
                                myFile.Close();
                                LoadQueue(copy.id);
                                Queue queue = new Queue(copy.id, ssnUser);
                                queues.Add(queue);

                                SaveQueue(copy.id);

                            }
                        }
                    }
                }
                if (copy.id != bookId && y == copies.Count)
                {
                    Console.WriteLine("The book with ID: " + bookId + "does not exist in this library, please check your input and try again");
                }
                
            }
        }

        public void ReturnBook(string ssn)
        {
            int ssnUser = Int32.Parse(ssn);

            Console.WriteLine("What book do you want to return?");

            //List all the users books

            List<int> usersBooks = new List<int>();

            foreach(Copy copy in copies)
            {
                if(copy.ssn == ssnUser)
                {
                    usersBooks.Add(copy.id);
                }
            }
            
            for (int i = 0; i < usersBooks.Count; i++)
            {
                foreach(Book book in books)
                {
                    if (usersBooks[i] == book.id)
                    {
                        //List the books
                        Console.WriteLine("Title: " + book.title);
                    }    
                }
            }
            int returnBook = -1;
            string answer = Console.ReadLine();

            foreach(Book book in books)
            {
                if(answer == book.title)
                {
                    for(int i = 0;i < usersBooks.Count;i++)
                    {
                        if (usersBooks[i] == book.id)
                        {
                            Console.WriteLine("Returning the book " + book.title);
                            returnBook = book.id;
                        }
                    }
                }
            }
            int ssnNewUser = -1;
            foreach(Copy copy in copies)
            {
                if(copy.id == returnBook && copy.ssn == ssnUser)
                {
                    
                    
                    //If there is someone in the queue
                    if (File.Exists("queue_" + copy.id + ".txt"))
                    {
                        LoadQueue(copy.id);
                        foreach(Queue queue in queues)
                        {
                            if(queue.ISBN == copy.id)
                            {
                                ssnNewUser = queue.ssn;
                                Queue userToDelete = queues.Find(b => b.ssn == ssnNewUser);
                                if (userToDelete != null)
                                {
                                    queues.Remove(userToDelete);
                                    SaveQueue(copy.id);
                                }
                                copy.ssn = ssnNewUser;
                                SaveCopies();
                                break;
                            }
                        }
                    }

                    else
                    {
                        copy.ssn = -1;
                        SaveCopies();
                    }
                }
            }



        }

        
        public void ListAllBooks()
        {
            Console.WriteLine("All the books: ");

            Console.WriteLine("______________________________________");
            Console.WriteLine("|   Title   |   Author   |   Genre   |");
            Console.WriteLine("-------------------------------------");


            foreach (Book book in books)
            {
                
                Console.WriteLine("|    " + book.title + "   |   " + book.author + "   |   " + book.genre + "   |");
                Console.WriteLine("------------------------------------------------------------------------------");
            }
        }

        
        public void DeleteBooks()
        {
            ListAllBooks();
            Console.Write("Enter the title of the book that you want to delete: ");
            string title = Console.ReadLine();


            bool delete = false;
            int numOfCopies = 0;
            int bookId = -1;


            foreach (Book book in books)
            {

                if (book.title == title)
                {
                    delete = true;
                    bookId = book.id;

                    foreach (Copy copy in copies)
                    {
                        if (copy.id == book.id )
                        {

                            numOfCopies++;
                        }
                    }                    
                }
            }

            if (delete == true)
            {
                Console.WriteLine("The book has " + numOfCopies + " number of copies");

                foreach (Book book in books)
                {
                    if (book.id == bookId)
                    {
                        books.Remove(book);
                        SaveBooks();
                        break;
                    }
                }
                
                for (int i = 0; i < numOfCopies; i++)
                {
                    foreach (Copy copy in copies)
                    {
                        if (copy.id == bookId)
                        {
                            copies.Remove(copy);
                            SaveCopies();
                            break;
                        }
                    }
                }

                
            }

            

        }

        public void AddBooks()
        {
            int ssn = -1;

            //Makes it so that the next book gest a higher ID
            int id = 0;

            for (int i = 0; i < books.Count; i++)
            {
                id = books[i].id + 1;
            }
            int ISBN = 0;
            for (int i = 0; i < copies.Count; i++)
            {
                ISBN = copies[i].ISBN + 1;
            }
            int check = 0;
            Console.Write("Enter the Title: ");
            string title = Console.ReadLine();

            //If the book already exists creating a copy of it in Copy

            foreach (Book book in books)
            {
                if(book.title == title)
                {
                    Console.WriteLine("This book already exists in the system, creating a copy");

                    
                    check = 1;
                    id = book.id;
                    if (check == 1)
                    {
                        Copy copy = new Copy(id, ISBN, ssn);
                        copies.Add(copy);
                        SaveCopies();
                    }
                    break;
                }
            }

            if (check == 0)
            {
                Console.Write("Enter the Author: ");
                string author = Console.ReadLine();
                Console.Write("Enter the Genre: ");
                string genre = Console.ReadLine();

                Console.WriteLine();


                Copy copy = new Copy(id, ISBN, ssn);

                Book book = new Book(id, title, author, genre);
                copies.Add(copy);
                books.Add(book);
                SaveCopies();
                SaveBooks();

                Console.WriteLine("Book Successfully added.");

                Console.Clear();
                
            }

            check = 0;


        }

        

        
        public List<Book> SearchBooks(string searchTerm)
        {
            const int MAX_DISTANCE = 2; // Maximum Levenshtein distance for a match

            int isbn;
            bool isNumeric = int.TryParse(searchTerm, out isbn);

            // Search for books that match the search term in the title, author, genre, or ISBN
            List<Book> matchingBooks = books.Where(b =>
                ((isNumeric && b.id == isbn) || (!isNumeric &&
                (b.title.ToLower().Contains(searchTerm.ToLower()) ||
                b.author.ToLower().Contains(searchTerm.ToLower()) ||
                b.genre.ToLower().Contains(searchTerm.ToLower()))))
            ).ToList();

            // Search for close matches using Levenshtein distance
            foreach (Book book in books)
            {
                int titleDistance = ComputeLevenshteinDistance(book.title.ToLower(), searchTerm.ToLower());
                int authorDistance = ComputeLevenshteinDistance(book.author.ToLower(), searchTerm.ToLower());
                int genreDistance = ComputeLevenshteinDistance(book.genre.ToLower(), searchTerm.ToLower());

                if ((titleDistance <= MAX_DISTANCE || authorDistance <= MAX_DISTANCE || genreDistance <= MAX_DISTANCE) && !matchingBooks.Contains(book))
                {
                    matchingBooks.Add(book);
                }
            }

            return matchingBooks;
        }

        

        private static int ComputeLevenshteinDistance(string s, string t)
        {
            int[,] d = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
            {
                d[i, 0] = i;
            }

            for (int j = 0; j <= t.Length; j++)
            {
                d[0, j] = j;
            }

            for (int j = 1; j <= t.Length; j++)
            {
                for (int i = 1; i <= s.Length; i++)
                {
                    int substitutionCost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(Math.Min(
                        d[i - 1, j] + 1,      // Deletion
                        d[i, j - 1] + 1),     // Insertion
                        d[i - 1, j - 1] + substitutionCost); // Substitution
                }
            }

            return d[s.Length, t.Length];
        }

        public void LoadBooks()
        {
            if (!File.Exists("books.txt"))
            {
                return;
            }

            string[] lines = File.ReadAllLines("books.txt");
            foreach (string line in lines) 
            {
                string[] parts = line.Split(',');
                int ISBN = int.Parse(parts[0]);
                string title = parts[1];
                string author = parts[2];
                string genre = parts[3];

                Book book = new Book(ISBN, title, author, genre);
                books.Add(book);    
            }
        }

        public void LoadQueue(int bookId)
        {
            
            if (!File.Exists("queue_" + bookId + ".txt"))
            {

                return;
            }
            queues.Clear();
            string[] lines = File.ReadAllLines("queue_" + bookId + ".txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                int ISBN = int.Parse(parts[0]);
                int ssn = int.Parse(parts[1]);

                Queue queue = new Queue(ISBN, ssn);
                queues.Add(queue);
            }
        }

        public void LoadCopies()
        {
            if (!File.Exists("copy.txt"))
            {
                File.Create("copy.txt");
            }

            string[] lines = File.ReadAllLines("copy.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                int id = int.Parse(parts[0]);
                int ISBN = int.Parse(parts[1]);
                int ssn = int.Parse(parts[2]);

                Copy copy = new Copy(id, ISBN, ssn);
                copies.Add(copy);
            }

        }

        public void SaveCopies()
        {
            List<string> lines = new List<string>();
            foreach (Copy copy in copies)
            {
                lines.Add(copy.id + "," + copy.ISBN + "," + copy.ssn);
            }
            File.WriteAllLines("copy.txt", lines);
        }

        public void SaveBooks()
        {
            List<string> lines = new List<string>();
            foreach (Book book in books) 
            { 
                lines.Add(book.id + "," + book.title + "," + book.author + "," + book.genre);
            }
            File.WriteAllLines("books.txt", lines);
        }

        public void SaveQueue(int ISBN)
        {

            List<string> lines = new List<string>();
            foreach (Queue queue in queues)
            {
                lines.Add(queue.ISBN + "," + queue.ssn);
            }
            File.WriteAllLines("queue_" + ISBN + ".txt", lines);
            if (queues.Count <= 0) 
            {
                File.Delete("queue_" + ISBN + ".txt");
            }
        }

       
    }
}


