﻿using System;
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
        [MinLength(2, ErrorMessage = "Please enter at least two characters.")]
        [MaxLength(100)]
        [Display(Name ="Pet Name")]
        public string PetName { get; set; }

        [Required]
        public Species Species { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public Sex PetSex { get; set; }

        [Display(Name ="Is your pet spayed or neutered?")]
        public bool IsSpayedNeutered { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public DateTimeOffset Birthdate { get; set; }

        [Display(Name ="MicroChip Number")]
        public string MicrochipNum { get; set; }

        [Display(Name ="Veterinarian's Name")]
        public string VetName { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

        public virtual ICollection<Weight> Weights { get; set; } = new List<Weight>();
    }
}
