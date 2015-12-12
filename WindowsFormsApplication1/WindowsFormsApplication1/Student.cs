using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinidilInformationSystem
{
    class Student
    {
        private string parentName;
        private string parentSurname;
        private string parentEmail;
        private int parentPhone;
        private string allergies;
        private string bloodType;
        
        public string _parentName
        {
            get
            {
                return parentName;
            }
            set
            {
                parentName = value;
            }
        }
        public string _parentSurname
        {
            get
            {
                return parentSurname;
            }
            set
            {
                parentSurname = value;
            }
        }
        public string _parentEmail
        {
            get
            {
                return parentEmail;
            }
            set
            {
                parentEmail = value;
            }
        }
        public string _allergies
        {
            get
            {
                return allergies;
            }
            set
            {
                parentName = value;
            }
        }
        public string _bloodType
        {
            get
            {
                return bloodType;
            }
            set
            {
                bloodType = value;
            }
        }
        public int _parentPhone
        {
            get
            {
                return parentPhone;
            }
            set
            {
                parentPhone = value;
            }
        }

    }
}
