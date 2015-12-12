using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinidilInformationSystem
{
    class Teacher
    {
        private int refTC;
        private string skills;
        private int numberOfLessons;

        public int _refTC
        {
            get
            {
                return refTC;
            }
            set
            {
                refTC = value;
            }
        }
        public int _numberOfLessons
        {
            get
            {
                return numberOfLessons;
            }
            set
            {
                numberOfLessons = value;
            }
        }
        public string _skills
        {
            get
            {
                return skills;
            }
            set
            {
                skills = value;
            }
        }
    }
}
