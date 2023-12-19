namespace Web_VANDAOPC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequired : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CatId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CatId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Brand = c.String(),
                        Details = c.String(),
                        Img = c.String(),
                        Status = c.String(),
                        Description = c.String(),
                        CatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CatId, cascadeDelete: true)
                .Index(t => t.CatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CatId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CatId" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
