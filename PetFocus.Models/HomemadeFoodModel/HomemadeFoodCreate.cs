using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.HomemadeFoodModel
{
    public class HomemadeFoodCreate
    {
        public int HomemadeListId { get; set; }

        [Required]
        public string Ingredient { get; set; }

        [Required]
        public string Quantity { get; set; }

        public int HomemadeFoodId { get; set; }
        public string Notes { get; set; }

        [Required]
        public bool IsStillUsed { get; set; }

        public List<Pet> Pets { get; set; } = new List<Pet>();

    }
}
