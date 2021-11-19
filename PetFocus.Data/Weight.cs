using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Data
{
    public class Weight
    {
        [Key]
        public int WeightId { get; set; }

        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }

        [Required]
        [Range(0.1, 350.0)]//world's heaviest dog was 345
        public double PetWeight { get; set; }

        [Required]
        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        public DateTime WeightDate { get; set; }
    }
}
