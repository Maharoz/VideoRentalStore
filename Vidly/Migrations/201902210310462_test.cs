namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsSubscribeToNewsLetter", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MembershipTypes", "Name", c => c.String());
            DropColumn("dbo.Customers", "IsSubscribedToNewsletter");
        }
        
        public override void Down()
        {


            AddColumn("dbo.Customers", "IsSubscribedToNewsletter", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "IsSubscribeToNewsLetter");
        }
    }
}
