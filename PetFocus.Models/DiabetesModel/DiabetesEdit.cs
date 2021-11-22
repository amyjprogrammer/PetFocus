using PetFocus.Data;
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
        public Pet Pet { get; set; }
        public int DiabetesId { get; set; }

        public double Glucose { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DiabetesDate { get; set; }
    }
}
