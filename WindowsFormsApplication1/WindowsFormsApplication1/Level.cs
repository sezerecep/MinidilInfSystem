using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinidilInformationSystem
{
    class Level
    {
        private DateTime Created_At;
        private DateTime Updated_At;
        private DateTime Deleted_At;
        private int id;
        private string levelName;
        private DateTime StartTime;
        private DateTime EndTime;

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
        public DateTime _StartTime
        {
            get
            {
                return StartTime;
            }
            set
            {
                StartTime = value;
            }
        }
        public DateTime _EndTime
        {
            get
            {
                return EndTime;
            }
            set
            {
                EndTime = value;
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
        public string _levelNamee
        {
            get
            {
                return levelName;
            }
            set
            {
                levelName = value;
            }
        }

    }
}
