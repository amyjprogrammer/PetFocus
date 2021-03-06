using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Models.HomemadeFoodModel
{
    public class HomemadeFoodListItem
    {
        public int HomemadeFoodId { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Still in Use?")]
        public bool IsStillUsed { get; set; }

    }
}
