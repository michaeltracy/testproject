using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using Dexter.Model;
using Dexter.Properties;
using System.Data;

namespace Dexter.Data
{
    public class ContactRepo : IDisposable
    {
        private string connectionString;
        private IDbConnection _conn;
        private IDbCommand _cmd;

        public ContactRepo() : 
            this(new NpgsqlConnection(Settings.Default.ConnectionString)) { }
        public ContactRepo(IDbConnection conn)
        {
            _conn = conn;
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Close();
                _conn.Open();
            }
            _cmd = _conn.CreateCommand();
        }

        public long Count
        {
            get
            {
                _cmd.CommandText = "SELECT COUNT(id) FROM contact;";
                return (long)(_cmd.ExecuteScalar());
            }
        }

        public IEnumerable<Contact> All()
        {
            _cmd.CommandText = "SELECT * FROM contact;";
            using (var reader = _cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new Contact
                    {
                        Name = (string)(reader["name"]),
                        Email = (string)(reader["email"]),
                        Id = (int)(reader["id"])
                    };
                }
            }
        }

        public Contact Destory(Contact deleteMe)
        {
            _cmd.CommandText = string.Format("DELETE FROM contact WHERE id = {0}", deleteMe.Id);
            _cmd.ExecuteNonQuery();
            deleteMe.Id = 0;
            return deleteMe;
        }

        public Contact Create(Contact newEntry)
        {
            _cmd.CommandText =
                string.Format("INSERT INTO contact (name, email) VALUES ('{0}', '{1}');",
                    newEntry.Name, newEntry.Email);
            _cmd.ExecuteNonQuery();
            _cmd.CommandText = string.Format("SELECT id FROM contact WHERE email = '{0}';", newEntry.Email);
            newEntry.Id = (int)(_cmd.ExecuteScalar());
            return newEntry;
        }

        public Contact Find(int id) { return FindByField("id", id); }
        public Contact FindByEmail(string email) { return FindByField("email", string.Format("'{0}'", email)); }
        public Contact FindByField(string field, object value)
        {
            _cmd.CommandText =
                string.Format("SELECT * FROM contact WHERE {0} = {1}", field, value);
            using (var reader = _cmd.ExecuteReader())
            {
                if (!reader.Read())
                    throw new Exception(string.Format("No record exists in contact for {0} = {1}", field, value));
                return new Contact
                {
                    Name = (string)(reader["name"]),
                    Email = (string)(reader["email"]),
                    Id = (int)(reader["id"])
                };
            }
        }

        public void Update(Contact existing)
        {
            _cmd.CommandText =
                string.Format(@"
UPDATE contact
SET name = '{0}', email = '{1}'
where id = {2}", 
               existing.Name, existing.Email, existing.Id);
            switch (_cmd.ExecuteNonQuery())
            {
                case 0:
                    throw new Exception("No record exsists for " + existing.Email);
                case 1:
                    return;
                default:
                    throw new Exception("Multiple users exists with id " + existing.Id);
            }
            
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
