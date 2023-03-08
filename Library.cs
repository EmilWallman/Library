/*
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace library
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, bool isAvailable)
        {
            Title = title;
            Author = author;
            IsAvailable = isAvailable;
        }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"{Title} by {Author} ({(IsAvailable ? "available" : "not available")})";
        }
    }

    class Library
    {
        private List<Book> books;

        public Library()
        {
            books = new List<Book>();

            // Read books from file
            string[] lines = File.ReadAllLines("books.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string title = parts[0].Trim();
                string author = parts[1].Trim();
                bool isAvailable = bool.Parse(parts[2].Trim());
                books.Add(new Book(title, author, isAvailable));
            }
        }

        public void DisplayBooks()
        {
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }
        }

        public void LendBook(string title)
        {
            Book book = FindBookByTitle(title);
            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false;
                Console.WriteLine($"Successfully lent {book.Title}!");
            }
            else if (book != null && !book.IsAvailable)
            {
                Console.WriteLine($"{book.Title} is not available for lending.");
            }
            else
            {
                Console.WriteLine($"Could not find book with title {title}.");
            }
        }

        public void ReturnBook(string title)
        {
            Book book = FindBookByTitle(title);
            if (book != null && !book.IsAvailable)
            {
                book.IsAvailable = true;
                Console.WriteLine($"Successfully returned {book.Title}!");
            }
            else if (book != null && book.IsAvailable)
            {
                Console.WriteLine($"{book.Title} has already been returned.");
            }
            else
            {
                Console.WriteLine($"Could not find book with title {title}.");
            }
        }

        public void ReserveBook(string title)
        {
            Book book = FindBookByTitle(title);
            if (book != null && book.IsAvailable)
            {
                Console.WriteLine($"{book.Title} is already available for lending.");
            }
            else if (book != null && !book.IsAvailable)
            {
                Console.WriteLine($"Successfully reserved {book.Title}!");
            }
            else
            {
                Console.WriteLine($"Could not find book with title {title}.");
            }
        }

        private Book FindBookByTitle(string title)
        {
            foreach (Book book in books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }
            return null;
        }

        public void AddBook(string title, string author)
        {
            Book book = new Book(title, author);
            books.Add(book);
            Console.WriteLine($"Added {book} to library.");
        }

        public void DeleteBook(string title)
        {
            Book book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine($"Removed {book} from library.");
            }
            else
            {
                Console.WriteLine($"Could not find book with title {title}.");
            }
        }

    }
    //Admin
    class Admin
    {
        public static void AddBook(Library library)
        {
            Console.WriteLine("Enter title of book to add:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter author of book to add:");
            string author = Console.ReadLine();
            library.AddBook(title, author);
        }

        public static void DeleteBook(Library library)
        {
            Console.WriteLine("Enter title of book to delete:");
            string title = Console.ReadLine();
            library.DeleteBook(title);
        }
    }


    class LibraryProgram
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            while (true)
            {
                Console.WriteLine("Enter command (display/lend/return/reserve/quit):");
                string command = Console.ReadLine();

                if (command.Equals("display", StringComparison.OrdinalIgnoreCase))
                {
                    library.DisplayBooks();
                }
                else if (command.Equals("lend", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter title of book to lend:");
                    string title = Console.ReadLine();
                    library.LendBook(title);
                }
                else if (command.Equals("return", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter title of book to return:");
                    string title = Console.ReadLine();
                    library.ReturnBook(title);
                }
                else if (command.Equals("reserve", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter title of book to reserve:");
                    string title = Console.ReadLine();
                    library.ReserveBook(title);
                }
                else if (command.Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }

}
*/

