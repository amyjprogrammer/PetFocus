﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.HomemadeFoodModel
{
    public class DiabetesCreate
    {
        public int PetId { get; set; }
        
        [Required]
        [Range(0, 1000)]//good is between 80-120
        public double Glucose { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime DiabetesDate { get; set; }
    }
}
