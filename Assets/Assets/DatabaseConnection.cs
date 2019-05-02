﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCreport
{
    class DatabaseConnection
    {
        private static DatabaseConnection instance;
        public String ConnectionString;
        private SqlConnection connection;


        public static DatabaseConnection getInstance()
        {
            if (instance == null)
                instance = new DatabaseConnection();
            return instance;
        }

        private DatabaseConnection()
        {

        }

        public SqlConnection getConnection()
        {
            connection = new SqlConnection(ConnectionString);
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public SqlDataReader getData(String commnadText)
        {
            connection = getConnection();
            SqlCommand cmd = new SqlCommand(commnadText,connection);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public int exectuteQuery(String commnadText)
        {
            connection = getConnection();
            SqlCommand cmd = new SqlCommand(commnadText,connection);
            int rows = cmd.ExecuteNonQuery();
            return rows;
        }
        /*
        public int getcount(String commnadText)
        {
            int rows = 0;
            connection = getConnection();
            SqlCommand cmd = new SqlCommand(commnadText, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                closeConnection();
                connection = getConnection();
                SqlCommand cm = new SqlCommand(commnadText, connection);
                rows = (int)cm.ExecuteScalar();
            }
            return rows;
        }
        */
        public void closeConnection()
        {
            if (connection != null)
                connection.Close();
        }

    }
}
