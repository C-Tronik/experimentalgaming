using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;




namespace Api_slot_machine.Models
{
    public class SlotQuery
    {
        public Appdb Db { get; }

        public SlotQuery(Appdb db)
        {
            Db = db;
        }

        public async Task<Slot_model_utenti> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `E_mail` FROM `Slots` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Slot_model_utenti>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `e_mail` FROM `Slots` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Slots`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Slot_model_utenti>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Slot_model_utenti>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Slot_model_utenti(Db)
                    {
                        Id = reader.GetInt32(0),
                        name = reader.GetString(1),
                        e_mail = reader.GetString(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}
