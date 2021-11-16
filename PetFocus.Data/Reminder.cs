using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Data
{
    public class Reminder
    {
        [Key]
        public int ReminderId { get; set; }

        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }

        public DateTime HeartwormMed { get; set; }

        public bool IsTimeForHeartMed
        {
            get
            {
                //heart meds need to be 30 days away
                //would like to make that every month- stretch goal
                var today = DateTime.Now;
                var next = new DateTime(today.Year, HeartwormMed.Month, HeartwormMed.Day);

                return ((next - today).Days == 30);
            }
        }

        public DateTime RabiesVac { get; set; }

        public bool IsThreeYearRabiesVac { get; set; }

        public bool IsTimeForRabies 
        { 
            get 
            {
                var today = DateTime.Now;
                var next = new DateTime(today.Year, RabiesVac.Month, RabiesVac.Day);
            } 
        }
    }
}
