using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.ReminderModel
{
    public class ReminderCreate
    {
        public int PetId { get; set; }


        [Required]
        [Display(Name = "Last Date of Heartworm medication")]
        [DataType(DataType.Date)]
        public DateTime HeartwormMed { get; set; }


        [Display(Name = "Last Date of Rabies Vaccination")]
        [DataType(DataType.Date)]
        public DateTime RabiesVac { get; set; }

        [Display(Name = "Three year rabies vaccination?")]
        public bool IsThreeYearRabiesVac { get; set; }


        [Display(Name = "Last Date of Flea treatment")]
        [DataType(DataType.Date)]
        public DateTime FleaTreatment { get; set; }


        [Display(Name = "Last Date of Nail Trim")]
        [DataType(DataType.Date)]
        public DateTime NailTrim { get; set; }

        [Display(Name = "How often do you trim the nails in days?")]
        public int TrimSchedule { get; set; }
    }
}
