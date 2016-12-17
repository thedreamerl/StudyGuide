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
        public event Action<string> ShowMessgae;
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
        public void LevelUp(FlashCardViewModel card,ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                c.Flashards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType).Level += 1;
                c.SaveChanges();
                 if (c.Flashards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType).Level == 4)
                {
                    ShowMessgae?.Invoke("Congrats! You've learned this card!");
                    DeleteCard();
                }
                    
            }
            
        }
        public void DeleteCard()
        {
            using (var c = new Context())
            {
                var result = (from s in c.Flashards
                              where s.Level == 4
                              select s).ToList();
                c.Flashards.RemoveRange(result);
            }
        }
    }
}