using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessApp.ViewModels
{
    public class TrainingTypeTrainerViewModel
    {
        public IEnumerable<Trainer> Trainers { get; set; }
        public TrainingType TrainingType { get; set; }
    }
}