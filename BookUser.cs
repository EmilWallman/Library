using System;

namespace library
{
    public class BookUser
    {

        public int id;
        public bool inLine;
        public int ssn;
        public int placeInLine;

        public BookUser(int id, bool inLine, int ssn, int placeInLine)
        {
            this.id = id;
            this.inLine = inLine;
            this.ssn = ssn;
            this.placeInLine = placeInLine;
        }

    }

}