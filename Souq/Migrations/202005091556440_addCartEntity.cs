namespace Souq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCartEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        Product_ID = c.Int(nullable: false),
                        Cart_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ID, t.Cart_Id })
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_Id, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Cart_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCarts", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.ProductCarts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Carts", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.ProductCarts", new[] { "Cart_Id" });
            DropIndex("dbo.ProductCarts", new[] { "Product_ID" });
            DropIndex("dbo.Carts", new[] { "CustomerId" });
            DropTable("dbo.ProductCarts");
            DropTable("dbo.Carts");
        }
    }
}
