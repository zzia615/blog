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
        }

        public DbSet<User> User { get; set; }
    }
}