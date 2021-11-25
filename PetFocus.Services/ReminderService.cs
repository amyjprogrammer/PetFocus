using PetFocus.Data;
using PetFocus.Models.ReminderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFocus.Services
{
    public class ReminderService
    {
        public bool CreateReminder(ReminderCreate model)
        {
            var entity =
                new Reminder()
                {
                    /*ReminderId = model.ReminderId,*/
                    HeartwormMed = model.HeartwormMed,
                    RabiesVac = model.RabiesVac,
                    IsThreeYearRabiesVac = model.IsThreeYearRabiesVac,
                    FleaTreatment = model.FleaTreatment,
                    NailTrim = model.NailTrim,
                    TrimSchedule = model.TrimSchedule
                };

            using (var ctx = new ApplicationDbContext())
            {
                if (ctx.Reminders.Find(model.ReminderId) != null)
                {
                    var entry =
                    ctx
                    .Reminders
                    .Single(e => e.ReminderId == model.ReminderId);

                    entry.HeartwormMed = model.HeartwormMed;
                    entry.RabiesVac = model.RabiesVac;
                    entry.IsThreeYearRabiesVac = model.IsThreeYearRabiesVac;
                    entry.FleaTreatment = model.FleaTreatment;
                    entry.NailTrim = model.NailTrim;
                    entry.TrimSchedule = model.TrimSchedule;

                    return ctx.SaveChanges() == 1;
                }
                else
                {
                    entity.ReminderId = model.PetId;
                    ctx.Reminders.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }
        }

        public IEnumerable<ReminderListItem> GetReminders()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Reminders
                    .Select(
                        e =>
                        new ReminderListItem
                        {
                            ReminderId = e.ReminderId,
                            HeartwormMed = e.HeartwormMed,
                            RabiesVac = e.RabiesVac,
                            IsThreeYearRabiesVac = e.IsThreeYearRabiesVac,
                            FleaTreatment = e.FleaTreatment,
                            NailTrim = e.NailTrim,
                            TrimSchedule = e.TrimSchedule,
                            Pet = e.Pet
                        }
                        );
                return query.ToArray();
            }
        }

        public ReminderDetail GetReminderById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Reminders
                    .Single(e => e.ReminderId == id);
                return
                    new ReminderDetail
                    {
                        ReminderId = entity.ReminderId,
                        HeartwormMed = entity.HeartwormMed,
                        RabiesVac = entity.RabiesVac,
                        IsThreeYearRabiesVac = entity.IsThreeYearRabiesVac,
                        FleaTreatment = entity.FleaTreatment,
                        NailTrim = entity.NailTrim,
                        TrimSchedule = entity.TrimSchedule,
                        Pet = entity.Pet
                    };
            }
        }

        public bool UpdateReminder(ReminderEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Reminders
                    .Single(e => e.ReminderId == model.ReminderId);

                entity.HeartwormMed = model.HeartwormMed;
                entity.RabiesVac = model.RabiesVac;
                entity.IsThreeYearRabiesVac = model.IsThreeYearRabiesVac;
                entity.FleaTreatment = model.FleaTreatment;
                entity.NailTrim = model.NailTrim;
                entity.TrimSchedule = model.TrimSchedule;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteReminder(int reminderId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Reminders
                    .Single(e => e.ReminderId == reminderId);

                ctx.Reminders.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
