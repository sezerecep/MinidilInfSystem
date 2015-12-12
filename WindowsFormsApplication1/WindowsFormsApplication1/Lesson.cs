using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinidilInformationSystem
{
    class Lesson
    {
        private DateTime Created_At;
        private DateTime Updated_At;
        private DateTime Deleted_At;
        private int id;
        private string Name;
        private string book;

        public DateTime _Created_At
        {
            get
            {
                return Created_At;
            }
            set
            {
                Created_At = value;
            }
        }
        public DateTime _Deleted_At
        {
            get
            {
                return Deleted_At;
            }
            set
            {
                Deleted_At = value;
            }
        }
        public DateTime _Updated_At
        {
            get
            {
                return Updated_At;
            }
            set
            {
                Updated_At = value;
            }
        }
        public int _id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string _Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public string _book
        {
            get
            {
                return book;
            }
            set
            {
                book = value;
            }
        }

    }
}
