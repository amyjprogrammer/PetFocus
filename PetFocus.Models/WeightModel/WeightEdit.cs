using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.WeightModel
{
    public class WeightEdit
    {
        public Pet Pet { get; set; }
        public int WeightId { get; set; }

        [Display(Name = "Pet Weight")]
        public double PetWeight { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime WeightDate { get; set; }
    }
}
