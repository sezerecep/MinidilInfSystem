using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MinidilInformationSystem
{
    class DatabaseConnection
    {
        MySqlConnection connection;

        public bool is_Connected()
        {
            try
            {
                connection = new MySqlConnection("Server=db4free.net;Database=minidil;Uid=minidil;Pwd='15241524'");
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void closeConnection()
        {
            connection.Close();
        }
        public bool NonReturnQuery(string command)
        {
            if (this.is_Connected())
            {
                MySqlCommand comm = new MySqlCommand(command, this.connection);
                if (comm.ExecuteNonQuery() <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        public DataTable ReturningQuery(string command)
        {
            if (this.is_Connected())
            {
                DataTable myTable = new DataTable();
                MySqlCommand myComm = new MySqlCommand(command, this.connection);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(myComm);
                myAdapter.Fill(myTable);
                if (myTable.Rows.Count>0&& (myTable!= null))
                    return myTable;
                else
                    return new DataTable("Connected but Empty");
            }
            else
            {
                return new DataTable("I am Empty");
            }

        }
    }
}
