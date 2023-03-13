using library;
using System;
namespace library
{
    public class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            LoginSite loginSite = new LoginSite();
            loginSite.Start();

        }
    }
}

