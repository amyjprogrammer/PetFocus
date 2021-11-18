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

        public WeightDetail GetWeightById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Weights
                    .Single(e => e.WeightId == id);
                return
                    new WeightDetail
                    {
                        WeightId = entity.WeightId,
                        Pet = entity.Pet,
                        PetWeight = entity.PetWeight,
                        WeightDate = entity.WeightDate
                    };
            }
        }

        public bool UpdateWeight(WeightEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Weights
                    .Single(e => e.WeightId == model.WeightId);

                entity.PetWeight = model.PetWeight;
                entity.WeightDate = model.WeightDate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteWeight(int weightId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Weights
                    .Single(e => e.WeightId == weightId);

                ctx.Weights.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
