using PetFocus.Data;
using PetFocus.Models.DiabetesModel;
using PetFocus.Models.HomemadeFoodModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Services
{
    public class DiabetesService
    {
        public bool CreateDiabetes(DiabetesCreate model)
        {
            var entity =
                new Diabetes()
                {
                    PetId = model.PetId,
                    Glucose = model.Glucose,
                    DiabetesDate = model.DiabetesDate
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Diabetic.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<DiabetesListItem> GetDiabetes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Diabetic
                    .Select(
                        e =>
                        new DiabetesListItem
                        {
                            DiabetesId = e.DiabetesId,
                            Pet = e.Pet,
                            Glucose = e.Glucose,
                            DiabetesDate = e.DiabetesDate
                        }
                        );
                return query.ToArray();
            }
        }
    }
}
