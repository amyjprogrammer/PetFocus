using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.DiabetesModel
{
    public class DiabetesEdit
    {
        public int DiabetesId { get; set; }

        public double Glucose { get; set; }

        [Display(Name = "Date")]
        public DateTime DiabetesDate { get; set; }
    }
}
