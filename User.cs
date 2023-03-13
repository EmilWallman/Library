using System;
namespace library
{
    public class User
    {
        
        public string username;
        public string password;
        public string ssn;
        public bool isAdmin;

        public User(string username, string password, string ssn, bool isAdmin)
        {
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
            this.ssn = ssn;
        }
    }
}

