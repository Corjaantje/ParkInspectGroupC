using System;
using System.Data;
using System.Data.SQLite;

namespace LocalDatabase
{
    public class DatabaseActions
    {
        public bool CUD(SQLiteConnection _connection, string sql)
        {
            try
            {
                _connection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, _connection);
                command.ExecuteNonQuery();
                _connection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable Get(SQLiteConnection _connection, string sql)
        {
            DataTable datatable = new DataTable();
            try
            {
                _connection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, _connection);
                SQLiteDataReader reader = command.ExecuteReader();
                datatable.Load(reader);
                _connection.Close();

                return datatable;
            }
            catch (Exception)
            {
                return datatable;
            }
        }
    }
}
