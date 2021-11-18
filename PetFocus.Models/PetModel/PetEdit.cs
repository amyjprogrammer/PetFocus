using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.PetModel
{
    public class PetEdit
    {
        public int PetId { get; set; }

        [Display(Name = "Pet Name")]
        public string PetName { get; set; }

        public Species Species { get; set; }

        [Display(Name = "Sex")]
        public Sex PetSex { get; set; }

        [Display(Name = "Is your pet spayed or neutered?")]
        public bool IsSpayedNeutered { get; set; }

        public string Breed { get; set; }

        public DateTimeOffset Birthdate { get; set; }

        [Display(Name = "MicroChip Number")]
        public string MicrochipNum { get; set; }

        [Display(Name = "Veterinarian's Name")]
        public string VetName { get; set; }

        [Display(Name = "Has Diabetes")]
        public bool HasDiabetes { get; set; }
    }
}
