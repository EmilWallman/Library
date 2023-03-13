using System;

namespace library
{
    public class Book
    {
        public int id;
        public string title;
        public string author;
        public string genre;
        public bool status;

        public Book(int id, string title, string author, string genre, bool status)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.status = status;
        }
    }
}
