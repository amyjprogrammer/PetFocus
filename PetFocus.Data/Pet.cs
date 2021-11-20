using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Data
{
    public enum Species
    {
        Cat, Dog
    }
    public enum Sex
    {
        Male, Female
    }

    public class Pet
    {
        [Key]
        public int PetId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least two characters.")]
        [MaxLength(100)]
        [Display(Name ="Pet Name")]
        public string PetName { get; set; }

        [Required]
        public Species Species { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public Sex PetSex { get; set; }

        [Display(Name ="Spayed or neutered?")]
        public bool IsSpayedNeutered { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Display(Name ="MicroChip Number")]
        public string MicrochipNum { get; set; }

        [Display(Name ="Vet's Name")]
        public string VetName { get; set; }

        [Display(Name = "Diabetes")]
        public bool HasDiabetes { get; set; }

        public virtual Reminder Reminder { get; set; }

        public virtual ICollection<Weight> Weights { get; set; } = new List<Weight>();
        public virtual ICollection<Diabetes> Diabetic { get; set; } = new List<Diabetes>();
        public virtual ICollection<HomemadeFood> HomemadeFoods { get; set; } = new List<HomemadeFood>();

        //to hold my enums 
        public struct ConvertEnum
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
    }
}
