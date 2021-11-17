using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Data
{
    public class HomemadeFood
    {
        [Key]
        public int HomemadeFoodId { get; set; }

        public string Notes { get; set; }

        [Required]
        [Display(Name ="Still in Use?")]
        public bool IsStillUsed { get; set; }

        public virtual ICollection<HomemadeList> HomemadeLists { get; set; } = new List<HomemadeList>();

        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
