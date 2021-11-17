using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.WeightModel
{
    public class WeightListItem
    {
        public int WeightId { get; set; }

        public Pet Pet { get; set; }

        [Display(Name ="Pet Weight")]
        public double PetWeight { get; set; }

        [Display(Name = "Date")]
        public DateTime WeightDate { get; set; }
    }
}
