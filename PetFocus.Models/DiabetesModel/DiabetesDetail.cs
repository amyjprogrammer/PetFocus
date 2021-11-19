using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.DiabetesModel
{
    public class DiabetesDetail
    {
        public int DiabetesId { get; set; }

        public virtual Pet Pet { get; set; }

        public double Glucose { get; set; }

        [Display(Name = "Date")]
        public DateTime DiabetesDate { get; set; }
    }
}
