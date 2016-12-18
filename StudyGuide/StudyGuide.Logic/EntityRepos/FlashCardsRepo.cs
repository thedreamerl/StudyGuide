using StudyGuide.DataBase;
using StudyGuide.DataBase.DBO;
using StudyGuide.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.EntityRepos
{
    public class FlashCardsRepo
    {
        public event Action<string> ShowMessgae;
        public async Task AddFlashCard(FlashCardViewModel card, ScheduleViewModel schedule)
        {
           using (var c = new Context())
            {
                if (await c.FlashCards.FirstOrDefaultAsync(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType) != null)
                    throw new ArgumentException("Similar card has been already added");
                c.FlashCards.Add(new FlashCards
                {
                    Term = card.Term,
                    Definition = card.Definition,
                    Level = 0,
                    ScheduleID = await c.Schedule.FirstAsync(x => x.SubjectID.Name == schedule.Subject && x.WorkTypeID.Name == schedule.WorkType)
                });
               await c.SaveChangesAsync();
            }
        }
        public Task<IEnumerable<FlashCardViewModel>> AllFlashCards(ScheduleViewModel s)
        {
            return Task.Run(() =>
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
                    return (IEnumerable<FlashCardViewModel>)result;

                }
            });

        }
        public async Task LevelUp(FlashCardViewModel card,ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                c.FlashCards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType).Level += 1;
                await c.SaveChangesAsync();
                 if (c.FlashCards.FirstOrDefault(x => x.Term == card.Term && x.Definition == card.Definition && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType).Level == 4)
                {
                    ShowMessgae?.Invoke("Congrats! You've learned this card!");
                    await DeleteCard();
                }
                    
            }
            
        }
        public async Task DeleteCard()
        {
            using (var c = new Context())
            {
                var result = (from s in c.FlashCards
                              where s.Level == 4
                              select s).ToList();
                c.FlashCards.RemoveRange(result);
               await c.SaveChangesAsync();
            }
        }
    }
}