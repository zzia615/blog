namespace jyfangyy.Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Damage",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 100),
                        msg = c.String(nullable: false, maxLength: 2000),
                        imageUrl = c.String(nullable: false, maxLength: 2000),
                        code = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 100),
                        status = c.Int(nullable: false),
                        pub_user_code = c.String(maxLength: 50),
                        pub_user_name = c.String(maxLength: 20),
                        pub_user_type = c.String(maxLength: 10),
                        publishDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        code = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 100),
                        status = c.Int(nullable: false),
                        laboratory_code = c.String(nullable: false, maxLength: 50),
                        laboratory_name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.code);
            
            CreateTable(
                "dbo.LabApply",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 60),
                        mode = c.String(nullable: false, maxLength: 20),
                        code = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 100),
                        apply_date = c.DateTime(nullable: false),
                        user_code = c.String(nullable: false, maxLength: 50),
                        user_name = c.String(nullable: false, maxLength: 20),
                        user_type = c.String(nullable: false, maxLength: 10),
                        plan_date = c.DateTime(nullable: false),
                        plan_sjd = c.String(nullable: false, maxLength: 20),
                        remark = c.String(nullable: false, maxLength: 200),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Laboratory",
                c => new
                    {
                        code = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 100),
                        max_num = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        occupy_date = c.DateTime(),
                        storey_code = c.String(nullable: false, maxLength: 50),
                        storey_name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.code);
            
            CreateTable(
                "dbo.Loss",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 100),
                        msg = c.String(nullable: false, maxLength: 2000),
                        place = c.String(nullable: false, maxLength: 100),
                        imageUrl = c.String(nullable: false, maxLength: 2000),
                        publishDate = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                        gotDate = c.DateTime(),
                        pub_user_code = c.String(maxLength: 50),
                        pub_user_name = c.String(maxLength: 20),
                        pub_user_type = c.String(maxLength: 10),
                        user_code = c.String(maxLength: 50),
                        user_name = c.String(maxLength: 20),
                        user_type = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Storey",
                c => new
                    {
                        code = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.code);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        code = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 20),
                        pwd = c.String(nullable: false, maxLength: 100),
                        sex = c.String(maxLength: 10),
                        grade = c.String(maxLength: 50),
                        major = c.String(maxLength: 100),
                        type = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Storey");
            DropTable("dbo.Loss");
            DropTable("dbo.Laboratory");
            DropTable("dbo.LabApply");
            DropTable("dbo.Device");
            DropTable("dbo.Damage");
        }
    }
}
