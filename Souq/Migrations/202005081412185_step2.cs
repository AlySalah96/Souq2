namespace Souq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order_Product", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Order_Product", "ProductId");
            AddForeignKey("dbo.Order_Product", "ProductId", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order_Product", "ProductId", "dbo.Products");
            DropIndex("dbo.Order_Product", new[] { "ProductId" });
            DropColumn("dbo.Order_Product", "ProductId");
        }
    }
}
