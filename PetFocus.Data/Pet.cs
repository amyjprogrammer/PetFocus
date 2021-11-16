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
        public string PetName { get; set; }

        [Required]
        public Species Species { get; set; }

        [Required]
        public Sex PetSex { get; set; }

        public bool IsSpayedNeutered { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public DateTimeOffset Birthdate { get; set; }

        public string MicrochipNum { get; set; }

        public string VetName { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

        public virtual ICollection<Weight> Weights { get; set; } = new List<Weight>();
    }
}
