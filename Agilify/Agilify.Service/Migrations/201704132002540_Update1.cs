namespace AgilifyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Member_Id", "dbo.Members");
            DropIndex("dbo.Members", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "Member_Id" });
            CreateTable(
                "dbo.TeamMember",
                c => new
                    {
                        TeamId = c.String(nullable: false, maxLength: 128),
                        MemberId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TeamId, t.MemberId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.MemberId);
            
            DropColumn("dbo.Members", "Team_Id");
            DropColumn("dbo.Teams", "Member_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Member_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Members", "Team_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.TeamMember", "MemberId", "dbo.Members");
            DropForeignKey("dbo.TeamMember", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamMember", new[] { "MemberId" });
            DropIndex("dbo.TeamMember", new[] { "TeamId" });
            DropTable("dbo.TeamMember");
            CreateIndex("dbo.Teams", "Member_Id");
            CreateIndex("dbo.Members", "Team_Id");
            AddForeignKey("dbo.Teams", "Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.Members", "Team_Id", "dbo.Teams", "Id");
        }
    }
}
