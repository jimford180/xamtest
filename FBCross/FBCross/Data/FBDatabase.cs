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
            database.CreateTableAsync<Employee>().Wait();
            database.CreateTableAsync<Service>().Wait();
            database.CreateTableAsync<MasterClass>().Wait();
            database.CreateTableAsync<MasterSchedule>().Wait();
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

        private Repository<Employee> _employees;
        public Repository<Employee> Employees
        {
            get
            {
                if (_employees == null)
                    _employees = new Repository<Employee>(database);
                return _employees;
            }
        }

        private Repository<Service> _services;
        public Repository<Service> Services
        {
            get
            {
                if (_services == null)
                    _services = new Repository<Service>(database);
                return _services;
            }
        }
        private Repository<MasterClass> _masterClasses;
        public Repository<MasterClass> MasterClasses
        {
            get
            {
                if (_masterClasses == null)
                    _masterClasses = new Repository<MasterClass>(database);
                return _masterClasses;
            }
        }
        private Repository<MasterSchedule> _masterSchedules;
        public Repository<MasterSchedule> MasterSchedules
        {
            get
            {
                if (_masterSchedules == null)
                    _masterSchedules = new Repository<MasterSchedule>(database);
                return _masterSchedules;
            }
        }

    }
}
