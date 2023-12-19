namespace Web_VANDAOPC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Products", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "Id", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "Id" });
            DropTable("dbo.Carts");
        }
    }
}
