using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnessApp.Models
{
    public class TrainingType
    {
       
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Training")]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Photo { get; set; }

        //[Display(Name = "Photo")]
        //public HttpPostedFileBase PhotoUpload { get; set; }

        [Required]
        public Trainer Trainer { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
    }
}