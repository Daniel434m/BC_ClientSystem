namespace BC_ClientSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClientCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        Surname = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.ClientContacts",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientId, t.ContactId })
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Contact", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientContacts", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.ClientContacts", "ClientId", "dbo.Client");
            DropIndex("dbo.ClientContacts", new[] { "ContactId" });
            DropIndex("dbo.ClientContacts", new[] { "ClientId" });
            DropTable("dbo.ClientContacts");
            DropTable("dbo.Contact");
            DropTable("dbo.Client");
        }
    }
}
