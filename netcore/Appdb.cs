using System;
using MySql.Data.MySqlClient;

namespace Api_slot_machine
{
    public class Appdb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public Appdb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}