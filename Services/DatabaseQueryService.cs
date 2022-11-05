using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ModelX.Data;
using System.Data.Common;
namespace ModelX.Services
{
    public class DatabaseQueryService
    {
        private readonly AppDbContext AppDbContext;
        public DatabaseQueryService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;

        }

        //public IQueryable<DatabaseTable> ShowTables()
        //{
        //    return AppDbContext.DatabaseTables.FromSqlRaw("select  name from sqlite_master where type='table' order by name");
        //}

        //public IQueryable<DatabaseTableColumn> ShowTableDetail(string tableName)
        //{
        //    return AppDbContext.DatabaseTableColumns.FromSqlRaw($"pragma table_info ('{tableName}')");
        //}

        public IQueryable<DatabaseTable> ShowTables()
        {
            return AppDbContext.DatabaseTables.FromSqlRaw(" select table_name as Name,TABLE_COMMENT  from information_schema.tables where table_schema = 'test' and table_type = 'base table'");
        }

        public IQueryable<DatabaseTableColumn> ShowTableDetail(string tableName)
        {
            return AppDbContext.DatabaseTableColumns.FromSqlRaw($"select COLUMN_KEY,COLUMN_NAME,COLUMN_TYPE,COLUMN_COMMENT from information_schema.columns where table_schema='test' and table_name='{tableName}';\r\n\r\n");
        }
       




        public List<Apps> QueryApps()
        {
            var tables = ShowTables().ToList();
            var tableNames = tables.Where(t => t.Name.Contains("_")).Where(t => !t.Name.EndsWith("_")).Select(t => t.Name);
            var apps = tableNames.Select(n => n.Split("_").FirstOrDefault()).Distinct().Select(n => new Apps { Name = n ,Description=n=="rbac"?"角色权限":"系统"}).ToList();
            foreach (var app in apps)
            {
                var app_tables = tableNames.Where(t => t.StartsWith(app.Name + "_")).ToList();
                app_tables.ForEach(t =>
                {
                   var table= tables.FirstOrDefault(table => table.Name == t);
                    app.DatabaseTables.Add(new DatabaseTable { Columns = ShowTableDetail(t).ToList(), Name = t , TABLE_COMMENT = table.TABLE_COMMENT });
                });

            }

            return apps;
        }

    }

    public static class SqlQueryExtensions
    {
        public static IList<T> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
            {
                // share the current database transaction, if one exists
                var transaction = db.Database.CurrentTransaction;
                if (transaction != null)
                    db2.Database.UseTransaction(transaction.GetDbTransaction());
                return db2.Set<T>().FromSqlRaw(sql, parameters).ToList();
            }
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private readonly DbConnection connection;

            public ContextForQueryType(DbConnection connection)
            {
                this.connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // switch on the connection type name to enable support multiple providers
                // var name = con.GetType().Name;
                optionsBuilder.UseSqlite(connection);

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<T>().HasNoKey();
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
