namespace jyfangyy.Main.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<jyfangyy.Main.Data.SqlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(jyfangyy.Main.Data.SqlDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (context.User.Count() <= 0)
            {
                context.User.AddOrUpdate(p => p.code, new Models.User
                {
                    code = "admin",
                    name = "admin",
                    pwd = "123456",
                    sex = "1",
                    type = "1"
                }
                , new Models.User
                {
                    code = "t001",
                    name = "测试教师",
                    pwd = "123",
                    sex = "1",
                    type = "2"
                }, new Models.User
                {
                    code = "s001",
                    name = "测试学生",
                    pwd = "1234",
                    sex = "1",
                    type = "3",
                    grade = "大四",
                    major = "计算机"
                }
                );
            }
            if (context.Storey.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    context.Storey.AddOrUpdate(p => p.code, new Models.Storey
                    {
                        code = "000" + i.ToString(),
                        name = i.ToString() + "号楼"
                    });
                }
            }
            if (context.Laboratory.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        context.Laboratory.AddOrUpdate(p => p.code, new Models.Laboratory
                        {
                            code = i.ToString() + j.ToString("000"),
                            name = j.ToString() + "号实验室",
                            storey_code = "000" + i.ToString(),
                            storey_name = i.ToString() + "号楼",
                            max_num = 100
                        });
                    }
                }
            }
            if (context.Device.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        for (int k = 1; k <= 10; k++)
                        {
                            context.Device.AddOrUpdate(p => p.code, new Models.Device
                            {
                                code = i.ToString() + j.ToString("000") + k.ToString("000"),
                                name = k.ToString("000") + "号设备",
                                laboratory_code = i.ToString() + j.ToString("000"),
                                laboratory_name = j.ToString() + "号实验室"
                            });
                        }
                    }
                }
            }

        }
    }
}
