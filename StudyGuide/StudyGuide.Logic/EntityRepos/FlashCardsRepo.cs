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
                if (c.FlashCards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType) != null)
                    throw new ArgumentException("Similar card has been already added");
                c.FlashCards.Add(new FlashCards
                {
                    Term = card.Term,
                    Definition = card.Definition,
                    Level = 0,
                    ScheduleID = c.Schedule.First(x => x.SubjectID.Name == schedule.Subject && x.WorkTypeID.Name == schedule.WorkType)
                });
                c.SaveChanges();
            }
        }
        public IEnumerable<FlashCardViewModel> AllFlashCards(ScheduleViewModel s)
        {
            using (var c = new Context())
            {
                var result = (from fc in c.FlashCards
                              where fc.ScheduleID.SubjectID.Name == s.Subject && fc.ScheduleID.WorkTypeID.Name == s.WorkType
                              select new FlashCardViewModel
                              {
                                  Term = fc.Term,
                                  Definition = fc.Definition
                              }).ToList();
                return result;

            }
        }
        public void LevelUp(FlashCardViewModel card,ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                c.FlashCards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType).Level += 1;
                c.SaveChanges();
                 if (c.FlashCards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType).Level == 4)
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
                var result = (from s in c.FlashCards
                              where s.Level == 4
                              select s).ToList();
                c.FlashCards.RemoveRange(result);
                c.SaveChanges();
            }
        }
    }
}