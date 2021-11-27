using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.ReminderModel
{
    public class ReminderListItem
    {
        public Pet Pet { get; set; }
        public int ReminderId { get; set; }
        public int PetId { get; set; }

        [Display(Name = "Heartworm medication")]
        public DateTime HeartwormMed { get; set; }

        [Display(Name = "Rabies Vaccination")]
        public DateTime RabiesVac { get; set; }

        [Display(Name = "Three Year Rabies")]
        public bool IsThreeYearRabiesVac { get; set; }

        [Display(Name = "Flea treatment")]
        public DateTime FleaTreatment { get; set; }

        [Display(Name = "Nail Trim")]
        public DateTime NailTrim { get; set; }

        [Display(Name = "Trim Schedule (days)")]
        public int TrimSchedule { get; set; }

        public bool IsTimeForRabies { get; set; }

    }
}
