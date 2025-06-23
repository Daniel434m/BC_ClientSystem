namespace BC_ClientSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "ClientCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "ClientCode", c => c.String(nullable: false));
        }
    }
}
