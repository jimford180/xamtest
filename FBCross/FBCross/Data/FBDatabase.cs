using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Data
{
    public class FBDatabase
    {
        SQLiteAsyncConnection database;
        public FBDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SessionMerchant>().Wait();
        }

        private Repository<SessionMerchant> _sessions;
        public Repository<SessionMerchant> Sessions {
            get
            {
                if (_sessions == null)
                    _sessions = new Repository<SessionMerchant>(database);
                return _sessions;
            }
        }


    }
}
