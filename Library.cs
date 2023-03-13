
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
        LoginSite loginSite = new LoginSite();


        public List<Book> books;
        

        public Library()
        {
            books = new List<Book>();
            LoadBooks();
        }

        public void AdminLibrary()
        {
            AddBooks();
        }

        public void UserLibrary()
        {

        }


        private void AddBooks()
        {

            //Makes it so that the next book gest a higher ID
            int id = books.Count;
            

            Console.Write("Enter the Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter the Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter the Genre: ");
            string genre = Console.ReadLine();



            Console.WriteLine();

            Book book = new Book(id, title, author, genre, true);
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

                Book book = new Book(id, title, author, genre, status);
                books.Add(book);
                
            }
        }   

        public void SaveBooks()
        {
            List<string> lines = new List<string>();
            foreach (Book book in books) 
            { 
                lines.Add(book.id + "," + book.title + "," + book.author + "," + book.genre + "," + book.status);
            }
            File.WriteAllLines("books.txt", lines);
        }
    }
}


