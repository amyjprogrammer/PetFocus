using PetFocus.Data;
using PetFocus.Models.WeightModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Services
{
    public class WeightService
    {
        public bool CreateWeight(WeightCreate model)
        {
            var entity =
                new Weight()
                {
                    PetId = model.PetId,
                    PetWeight = model.PetWeight,
                    WeightDate = model.WeightDate
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Weights.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<WeightListItem> GetWeights()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Weights
                    .Select(
                        e =>
                        new WeightListItem
                        {
                            WeightId = e.WeightId,
                            Pet = e.Pet,
                            PetWeight = e.PetWeight,
                            WeightDate = e.WeightDate
                        }
                        );
                return query.ToArray();
            }
        }
    }
}
