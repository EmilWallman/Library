
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using library;
using System.Runtime.InteropServices;
using System.IO.Pipes;
using System.Collections.Immutable;

/*

*/

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
            //LoadQue();
        }

        public void AdminLibrary(string ssn)
        {
            string currentSsn = ssn;
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
                    //LendBook(currentSsn);

                    //ListAllBooks();

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
                            Console.WriteLine("Title: {0}\nAuthor: {1}\nGenre: {2}\nStatus: {3}\n", book.title, book.author, book.genre);
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
            AdminLibrary(ssn);
        }

        //denna e TOM ***** 
        public void UserLibrary()
        {

        }

        //Gör så att när man söker så tar den och lsitar dom böckerna som kommer upp fint
        
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
                    
                    Console.WriteLine("Title: {0}\nAuthor: {1}\nGenre: {2}\nStatus: {3}\n", book.title, book.author, book.genre);
                }
            }

            Console.Write("What book do you want?: ");
            string lendbook = Console.ReadLine();

            //Check if book is avalible

            int bookId = 0;
            foreach(Book book in books)
            {
                if (book.title == lendbook)
                {
                    bookId = book.id;
                }
            }

            bool avalible = false;
            foreach(Copy copy in copies)
            {
                int i = 0;
                i++;
                if (copy.id == bookId)
                {
                    
                    if (copy.ssn == -1)
                    {
                        avalible = true;
                        Console.WriteLine("The book " + lendbook + " is avalible.");
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

                    if (i == copies.Count - 1)
                    {
                        Console.WriteLine("The book " + lendbook + " is not avalible right now.");
                        Console.WriteLine("Would you like to reserve it?");
                    }
                    
                }
            }

            //this is where the add to queue is
            foreach(Copy copy in copies)
            {

            }

            for (int i = 0; i < matchedBooks.Count; i++)
            {
                if (matchedBooks[i].id == lendbook)
                {
                    
                    Book bookToModify = books.Find(b => b.id == matchedBooks[i].id);
                    //Only if its avalible:
                    if (matchedBooks[i].status == true)
                    {
                        Console.WriteLine("You have chosen the book " + matchedBooks[i].title + " is that correct? y/n");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() == "y")
                        {
                            

                            //Uppdating the book with user and status
                            bookToModify.ssnUser = ssnUser;
                            bookToModify.status = false;

                            SaveBooks();

                            Console.WriteLine("The book " + matchedBooks[i].title + " is now lended");

                        }
                    }
                    else
                    {
                        Console.WriteLine(matchedBooks[i].title + " is not avalible at the moment, do you want to reserve it? y/n");
                        string answer = Console.ReadLine();

                        if(answer.ToLower() == "y")
                        {

                            foreach(Queue que in queues)
                            {
                                if(que.id == matchedBooks[i].id)
                                {
                                    que.placeInLine += 1;
                                    AddToQue(matchedBooks[i].id, ssnUser, que.placeInLine);
                                    SaveBookUser();
                                }
                                else
                                {
                                    AddToQue(matchedBooks[i].id, ssnUser, 1);
                                }
                            }
                            
                        }

                    }
                }
            }
        }
        
        
        public void AddToQue(int id, int ssn, int placeInLine)
        {
            Queue que = new Queue (id, ssn, placeInLine);
            queues.Add(que);
            SaveBookUser();
        }
        


        //Funkar typ behöver fixas till lite
        public void ListAllBooks()
        {
            Console.WriteLine("All the books: ");

            Console.WriteLine("______________________________________________________");
            Console.WriteLine("|   Title   |   Author   |   Genre   |   Available   |");
            Console.WriteLine("------------------------------------------------------");


            foreach (Book book in books)
            {
                
                Console.WriteLine("|    " + book.title + "   |   " + book.author + "   |   " + book.genre + "   |       |");
                Console.WriteLine("--------------------------------------------------------------------------------");
            }
        }

        //Not Working
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

                    //individuellt
                    check = 1;
                    id = book.id;
                    if (check == 1)
                    {
                        Copy copy = new Copy(ISBN, id, ssn);
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


                Copy copy = new Copy(ISBN, id, ssn);

                Book book = new Book(id, title, author, genre);
                books.Add(book);
                SaveCopies();
                SaveBooks();

                Console.WriteLine("Book Successfully added.");

                Console.Clear();
                
            }

            check = 0;


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

                Book book = new Book(id, title, author, genre);
                books.Add(book);    
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
                int ISBN = int.Parse(parts[0]);
                int id = int.Parse(parts[1]);
                int ssn = int.Parse(parts[2]);

                Copy copy = new Copy(ISBN, id, ssn);
                copies.Add(copy);
            }

        }

        public void SaveCopies()
        {
            List<string> lines = new List<string>();
            foreach (Copy copy in copies)
            {
                lines.Add(copy.ISBN + "," + copy.id + "," + copy.ssn);
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

        //Funkar nästan behöver bara fixas till lite
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

        //skapa en ny textfil för varje kopia
        public void LoadQueue(int bookId)
        {
            if (!File.Exists("queue_"+bookId))
            {
                // finns ingen kö
            }

        }



        /*
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

                Queue que = new Queue (id, ssn);
                queues.Add(que);

            }
        }

        public void SaveBookUser()
        {
            List<string> lines = new List<string>();
            foreach (Queue que in queues)
            {
                lines.Add(que.id + "," + que.ssn);
            }
            File.WriteAllLines("que.txt", lines);
        }
        */
    }
}


