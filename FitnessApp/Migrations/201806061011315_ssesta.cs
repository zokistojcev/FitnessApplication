namespace FitnessApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssesta : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TrainingTypes", name: "Trainer_Id", newName: "TrainerId");
            RenameIndex(table: "dbo.TrainingTypes", name: "IX_Trainer_Id", newName: "IX_TrainerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TrainingTypes", name: "IX_TrainerId", newName: "IX_Trainer_Id");
            RenameColumn(table: "dbo.TrainingTypes", name: "TrainerId", newName: "Trainer_Id");
        }
    }
}
