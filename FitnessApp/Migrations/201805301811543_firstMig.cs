namespace FitnessApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false),
                        Email = c.String(maxLength: 30),
                        Biography = c.String(maxLength: 200),
                        Photo = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Photo = c.String(maxLength: 200),
                        Trainer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainers", t => t.Trainer_Id, cascadeDelete: true)
                .Index(t => t.Trainer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingTypes", "Trainer_Id", "dbo.Trainers");
            DropIndex("dbo.TrainingTypes", new[] { "Trainer_Id" });
            DropTable("dbo.TrainingTypes");
            DropTable("dbo.Trainers");
        }
    }
}
