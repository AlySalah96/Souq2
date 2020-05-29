namespace Souq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColDesInCat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Description");
        }
    }
}
