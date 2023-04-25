using System;
using System.Collections.Generic;

namespace library
{
    public class Book
    {
        
        public int id;
        public string title;
        public string author;
        public string genre;


        public Book(int id, string title, string author, string genre)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.genre = genre;
        }
    }

    public class Queue
    {

        public int ISBN;
        public int ssn; //Specifikt för boken

        public Queue(int ISBN, int ssn)
        {
            this.ISBN = ISBN;
            this.ssn = ssn;
        }
    }

    public class Copy
    {
        public int id;
        public int ISBN;
        public int ssn;

        public Copy(int ISBN, int id, int ssn)
        {
            this.id = id;
            this.ISBN = ISBN;
            this.ssn = ssn;
        }
    }
}