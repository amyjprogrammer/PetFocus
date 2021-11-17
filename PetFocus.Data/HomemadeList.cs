using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Data
{
    public class HomemadeList
    {
        [Key]
        public int HomemadeListId { get; set; }

        [Required]
        public string Ingredient { get; set; }

        [Required]
        public string Quantity { get; set; }
    }
}
