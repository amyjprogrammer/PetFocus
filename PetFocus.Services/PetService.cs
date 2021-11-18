using PetFocus.Data;
using PetFocus.Models.PetModel;
using PetFocus.Models.WeightModel;
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

        public PetDetail GetPetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Pets
                    .Single(e => e.PetId == id && e.OwnerId == _userId);
                var pet = new PetDetail
                    {
                        PetId = entity.PetId,
                        PetName = entity.PetName,
                        Species = entity.Species,
                        PetSex = entity.PetSex,
                        IsSpayedNeutered = entity.IsSpayedNeutered,
                        Breed = entity.Breed,
                        Birthdate = entity.Birthdate,
                        MicrochipNum = entity.MicrochipNum,
                        VetName = entity.VetName,
                        Reminder = entity.Reminder
                    };
                foreach(var weight in entity.Weights)
                {
                    pet.Weights.Add(new WeightListItem { WeightId = weight.WeightId, Pet = weight.Pet, PetWeight = weight.PetWeight, WeightDate = weight.WeightDate });
                }

                return pet;
            }
        }

        public bool UpdatePet(PetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Pets
                    .Single(e => e.PetId == model.PetId && e.OwnerId == _userId);

                entity.PetName = model.PetName;
                entity.Species = model.Species;
                entity.PetSex = model.PetSex;
                entity.IsSpayedNeutered = model.IsSpayedNeutered;
                entity.Breed = model.Breed;
                entity.Birthdate = model.Birthdate;
                entity.MicrochipNum = model.MicrochipNum;
                entity.VetName = model.VetName;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
