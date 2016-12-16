using StudyGuide.DataBase;
using StudyGuide.DataBase.DBO;
using StudyGuide.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.EntityRepos
{
    public class FlashCardsRepo
    {
        public void AddFlashCard(FlashCardViewModel card, ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                if (c.Flashards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType) != null)
                    throw new ArgumentException("Similar card has been already added");
                c.Flashards.Add(new FlashCards
                {
                    Term = card.Term,
                    Definition = card.Definition,
                    Level = 0,
                    ScheduleID = c.Schedule.First(x => x.SubjectID.Name == schedule.Subject && x.WorkTypeID.Name == schedule.WorkType)
                });
                c.SaveChanges();
            }
        }
    }
}