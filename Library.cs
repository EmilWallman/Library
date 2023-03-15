
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using library;

/*

*/
    
namespace library
{

    public class Library
    {
        //Create a link between this and the interface
        


        public List<Book> books;
        public List<Que> queues;

        public Library()
        {
            queues = new List<Que>();
            books = new List<Book>();
            LoadBooks();
        }

        public void AdminLibrary()
        {
            Console.WriteLine("1. Add Books");
            Console.WriteLine("2. Delete Books");
            Console.WriteLine("3. List all books");
            Console.WriteLine("4. Search for Books");
            Console.WriteLine("5. Back");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddBooks();
                    break;

                case 2:
                    Console.Write("Enter the title of the book that you want to delete: ");
                    string title = Console.ReadLine();

                    DeleteBooks(title);
                    break;
                case 3:
                    //List of all books

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
                        foreach (Book book in matchedBooks)
                        {
                            Console.WriteLine("Title: {0}\nAuthor: {1}\nGenre: {2}\nStatus: {3}\n", book.title, book.author, book.genre, book.status);
                        }
                    }



                    break;

                case 5:
                    Interface loginSite = new Interface();
                    loginSite.AdminMenu();

                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            AdminLibrary();
        }

        //denna e TOM ***** 
        public void UserLibrary()
        {

        }

        public void DeleteBooks(string title)
        {
            Book bookToDelete = books.Find(b => b.title == title);

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                SaveBooks();

                Console.WriteLine("Book with title '{0}' deleted successfully.", title);
            }

            else
            {
                Console.WriteLine("Book with title '{0}' not found.", title);
            }
        }

        public void AddBooks()
        {

            //Makes it so that the next book gest a higher ID
            int id = books.Count;
            

            Console.Write("Enter the Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter the Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter the Genre: ");
            string genre = Console.ReadLine();
            int ssnUser = 0;


            Console.WriteLine();

            Book book = new Book(id, title, author, genre, true, ssnUser);
            books.Add(book);
            SaveBooks();

            Console.WriteLine("Book Successfully added.");

            Console.Clear();
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
                int id = int.Parse(parts[0]);
                string title = parts[1];
                string author = parts[2];
                string genre = parts[3];
                bool status = bool.Parse(parts[4]);
                int ssnUser = int.Parse(parts[5]);

                Book book = new Book(id, title, author, genre, status, ssnUser);
                books.Add(book);
                
            }
        }   

        public void SaveBooks()
        {
            List<string> lines = new List<string>();
            foreach (Book book in books) 
            { 
                lines.Add(book.id + "," + book.title + "," + book.author + "," + book.genre + "," + book.status + "," + book.ssnUser);
            }
            File.WriteAllLines("books.txt", lines);
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            const int MAX_DISTANCE = 2; // Maximum Levenshtein distance for a match

            // Search for books that match the search term in the title, author, or genre
            List<Book> matchingBooks = books.Where(b =>
                b.title.ToLower().Contains(searchTerm.ToLower()) ||
                b.author.ToLower().Contains(searchTerm.ToLower()) ||
                b.genre.ToLower().Contains(searchTerm.ToLower())
            ).ToList();

            // Search for close matches using Levenshtein distance
            foreach (Book book in books)
            {
                int distance = ComputeLevenshteinDistance(book.title.ToLower(), searchTerm.ToLower());
                if (distance <= MAX_DISTANCE && !matchingBooks.Contains(book))
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
        
        public void LoadQue()
        {
            if (!File.Exists("que.txt"))
            {
                return;
            }

            string[] lines = File.ReadAllLines("que.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                int id = int.Parse(parts[0]);
                int ssn = int.Parse(parts[1]);
                int placeInLine = int.Parse(parts[2]);

                Que que = new Que (id, ssn, placeInLine);
                queues.Add(que);

            }
        }

        public void SaveBookUser()
        {
            List<string> lines = new List<string>();
            foreach (BookUser bookUser in booksUser)
            {
                lines.Add()
            }
        }
        

    }
}


