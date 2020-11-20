using jyfangyy.Main.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace jyfangyy.Main.Data
{
    public class SqlDbContext:DbContext
    {
        public SqlDbContext() :base("myLocalDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlDbContext, Migrations.Configuration>());
            //InitData();
        }
        void InitData()
        {
            if (User.Count() <= 0)
            {
                User.Add(new Models.User
                {
                    code = "admin",
                    name = "admin",
                    pwd = "123456",
                    sex = "1",
                    type = "1"
                });
                SaveChanges();
            }
        }
        public DbSet<User> User { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Laboratory> Laboratory { get; set; }
        public DbSet<Storey> Storey { get; set; }
        public DbSet<LabApply> LabApply { get; set; }
        public DbSet<Loss> Loss { get; set; }
        public DbSet<Damage> Damage { get; set; }
    }
}