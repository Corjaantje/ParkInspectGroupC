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
                var command = new SQLiteCommand(sql, _connection);
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
            var datatable = new DataTable();
            try
            {
                _connection.Open();
                var command = new SQLiteCommand(sql, _connection);
                var reader = command.ExecuteReader();
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