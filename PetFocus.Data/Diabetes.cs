using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Data
{
    public class Diabetes
    {
        public int DiabetesId { get; set; }

        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }

        [Required]
        [Range(0,1000)]//good is between 80-120
        public double Glucose { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DiabetesDate { get; set; }
    }
}
