using PetFocus.Data;
using PetFocus.Models.PetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Services
{
    public class PetService
    {
        private readonly Guid _userId;

        public PetService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePet(PetCreate model)
        {
            var entity =
                new Pet()
                {
                    OwnerId = _userId,
                    PetName = model.PetName,
                    Species = model.Species,
                    PetSex = model.PetSex,
                    IsSpayedNeutered = model.IsSpayedNeutered,
                    Breed = model.Breed,
                    Birthdate = model.Birthdate,
                    MicrochipNum = model.MicrochipNum,
                    VetName = model.VetName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Pets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PetListItem> GetPets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Pets
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new PetListItem
                        {
                            PetId = e.PetId,
                            PetName = e.PetName,
                            Species = e.Species,
                            PetSex = e.PetSex,
                            IsSpayedNeutered = e.IsSpayedNeutered,
                            Breed = e.Breed,
                            Birthdate = e.Birthdate,
                            MicrochipNum = e.MicrochipNum,
                            VetName = e.VetName,
                        }
                        );
                return query.ToArray();
            }
        }
    }
}
