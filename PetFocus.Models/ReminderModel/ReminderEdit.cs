﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.ReminderModel
{
    public class ReminderEdit
    {
        public int PetId { get; set; }

        [Display(Name = "Last Date of Heartworm medication")]
        public DateTime HeartwormMed { get; set; }

        [Display(Name = "Last Date of Rabies Vaccination")]
        public DateTime RabiesVac { get; set; }

        [Display(Name = "Was it a three year rabies vaccination?")]
        public bool IsThreeYearRabiesVac { get; set; }


        [Display(Name = "Last Date of Flea treatment")]
        public DateTime FleaTreatment { get; set; }


        [Display(Name = "Last Date of Nail Trim")]
        public DateTime NailTrim { get; set; }

        [Display(Name = "How often do you trim the nails in days?")]
        public int TrimSchedule { get; set; }
    }
}