using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace MinidilInformationSystem
{
    class DatabaseConnection
    {
        MySqlConnection connection;

        public bool is_Connected()
        {
            try
            {
                connection = new MySqlConnection("Server=127.0.0.1;Database=minidil;Uid=root;Pwd='didemhatun1103'");
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
                int effrow;
                MySqlCommand comm = new MySqlCommand(command, this.connection);
                try
                {
                     effrow = comm.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    effrow = 0;
                }
                if (effrow <= 0)
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
