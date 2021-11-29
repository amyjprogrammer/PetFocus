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
        private readonly Guid _userId;

        public WeightService(Guid userId) => _userId = userId;
            
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
                    .Where(w => w.Pet.OwnerId == _userId)
                    .Select(
                        e =>
                        new WeightListItem
                        {
                            WeightId = e.WeightId,
                            Pet = e.Pet,
                            PetWeight = e.PetWeight,
                            WeightDate = e.WeightDate,
                            PetId = e.PetId
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
                    .Single(e => e.WeightId == id && e.Pet.OwnerId == _userId);
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

        public IEnumerable<WeightListItem> GetWeightByPetId(int petId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Weights
                    .Where(e => e.PetId == petId && e.Pet.OwnerId == _userId)
                    .Select(
                        e =>
                        new WeightListItem
                        {
                            Pet = e.Pet,
                            PetWeight = e.PetWeight,
                            WeightId = e.WeightId,
                            WeightDate = e.WeightDate,
                            PetId = e.PetId
                        }
                        );
                return query.ToArray();
            }
        }

        public bool UpdateWeight(WeightEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Weights
                    .Single(e => e.WeightId == model.WeightId && e.Pet.OwnerId ==_userId);

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
                    .Single(e => e.WeightId == weightId && e.Pet.OwnerId == _userId);

                ctx.Weights.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
