using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessApp.ViewModels
{
    public class TrainingTypeIndexViewModel
    {
        public int Id { get; set; }     
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }      
        public Trainer Trainer { get; set; }
    }
}