using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FitnessApp.Models
{
    public class FitnessDb: DbContext
    {
        public FitnessDb() : base("name=FitnessDbConnectionString")
        {
            
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
    }
}