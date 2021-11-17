﻿using PetFocus.Data;
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
                    PetId = model.PetId,
                    HeartwormMed = model.HeartwormMed,
                    RabiesVac = model.RabiesVac,
                    IsThreeYearRabiesVac = model.IsThreeYearRabiesVac,
                    FleaTreatment = model.FleaTreatment,
                    NailTrim = model.NailTrim,
                    TrimSchedule = model.TrimSchedule
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Reminders.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReminderListItem> GetReminders()
        {
            using( var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Reminders
                    .Select(
                        e =>
                        new ReminderListItem
                        {
                            PetId = e.PetId,
                            HeartwormMed = e.HeartwormMed,
                            RabiesVac = e.RabiesVac,
                            IsThreeYearRabiesVac = e.IsThreeYearRabiesVac,
                            FleaTreatment = e.FleaTreatment,
                            NailTrim = e.NailTrim,
                            TrimSchedule = e.TrimSchedule
                        }
                        );
                return query.ToArray();
            }
        }
    }
}
