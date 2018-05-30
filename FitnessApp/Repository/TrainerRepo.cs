using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessApp.Repository
{
    public static class TrainerRepo
    {
        public static List<Trainer> Trainers = new List<Trainer>
        {
            new Trainer { Id = 1, FullName = "Sasho Stojchev", Phone = "078144545", Email = "pss@hotmail.com", Biography = "first biography", Photo = "~/Images/Trainers/1.jpg" },
            new Trainer { Id = 2, FullName = "Neno Jelovac", Phone = "078276785", Email = "vss@hotmail.com", Biography = "second biography", Photo = "~/Images/Trainers/2.jpg" },
            new Trainer { Id = 3, FullName = "Zoran Stojchev", Phone = "078382565", Email = "tss@hotmail.com", Biography = "third biography", Photo = "~/Images/Trainers/3.jpg" },
        };

        public static List<TrainingType> TrainingTypes = new List<TrainingType>
        {
            new TrainingType { Id = 1, Name = "Aerobik", Description = "first description", Photo = "~/Images/TrainingTypes/1.jpg", Trainer = Trainers.Single(x => x.Id == 1) },
            new TrainingType { Id = 2, Name = "Fitness", Description = "second description", Photo = "~/Images/TrainingTypes/2.jpg", Trainer = Trainers.Single(x => x.Id == 2) },
            new TrainingType { Id = 3, Name = "Body building", Description = "third description", Photo = "~/Images/TrainingTypes/3.jpg", Trainer = Trainers.Single(x => x.Id == 3) },
        };
    }    
};
