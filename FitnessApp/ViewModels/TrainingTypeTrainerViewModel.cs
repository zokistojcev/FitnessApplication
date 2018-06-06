using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessApp.ViewModels
{
    public class TrainingTypeTrainerViewModel
    {
        public List<Trainer> Trainers { get; set; }
        public TrainingTypeViewModel TrainingType { get; set; }
     
    }
}