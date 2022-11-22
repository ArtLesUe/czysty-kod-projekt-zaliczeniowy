﻿namespace CkpTodoApp.DatabaseControllers
{
    public interface IDatabaseManagerInterface
    {
        void InitDatabase();

        void SeedDatabase();

        void ExecuteSQL(string sql);

        string ExecuteSQLQuery(string sql);

        string DatabasePath();
    }
}
