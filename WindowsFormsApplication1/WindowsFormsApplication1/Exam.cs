using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinidilInformationSystem
{
    class Exam
    {
        private DateTime Created_At;
        private DateTime Updated_At;
        private DateTime Date_Time;
        private int id;
        private string Name;

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
        public DateTime _Date_Time
        {
            get
            {
                return Date_Time;
            }
            set
            {
                Date_Time = value;
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
    }
}
