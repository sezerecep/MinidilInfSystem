using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinidilInformationSystem
{
    class User
    {
        private string Name;
        private string LastName;
        private DateTime BirthDate;
        private int Phone;
        private DateTime Created_At;
        private DateTime Updated_At;
        private DateTime Deleted_At;
        private string Permissions;
        private int TC;
        private string ForgotPassQue;
        private string ForgotPassAnsw;
        private DateTime FirstLogin;
        private DateTime LastLogin;
        private enum Gender{m,f};
        private string Email;
        private string PassWord;

        public string _Email
        {
            get
            {
                return Email;
            }
            set
            {
                Email = value;
            }
        }
        public string _PassWord
        {
            get
            {
                return PassWord;
            }
            set
            {
                PassWord = value;
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
        public string _LastName
        {
            get
            {
                return LastName;
            }
            set
            {
                LastName = value;
            }
        }
        public DateTime _BirthDate
        {
            get
            {
                return BirthDate;
            }
            set
            {
                BirthDate = value;
            }
        }
        public int _Phone
        {
            get
            {
                return Phone;
            }
            set
            {
                Phone = value;
            }
        }
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
        public string _Permissions
        {
            get
            {
                return Permissions;
            }
            set
            {
                Permissions = value;
            }
        }
        public int _TC
        {
            get
            {
                return TC;
            }
            set
            {
                TC = value;
            }
        }
        public string _ForgotPassQue
        {
            get
            {
                return ForgotPassQue;
            }
            set
            {
                ForgotPassQue = value;
            }
        }
        public string _ForgotPassAnsw
        {
            get
            {
                return ForgotPassAnsw;
            }
            set
            {
                ForgotPassAnsw = value;
            }
        }
        public DateTime _FirstLogin
        {
            get
            {
                return FirstLogin;
            }
            set
            {
                FirstLogin = value;
            }
        }
        public DateTime _LastLogin
        {
            get
            {
                return LastLogin;
            }
            set
            {
                LastLogin = value;
            }
        }
        
    }
}
