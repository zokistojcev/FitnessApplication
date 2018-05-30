using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessApp.Models
{
    public class Trainer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]  
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Biography { get; set; }

        [StringLength(200)]
        public string Photo { get; set; }

        [Display(Name = "Photo")]
        public HttpPostedFileBase PhotoUpload { get; set; }
    }
}