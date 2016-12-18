using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.DataBase.DBO;
using StudyGuide.DataBase;
using System.Data.Entity;

namespace StudyGuide.Logic.EntityRepos
{
    public class WorkTypeRepo
    {
        public async Task AddNew(string el)
        {
            using (var c = new Context())
            {
                if (await c.WorkType.FirstOrDefaultAsync(x => x.Name == el) != null)
                    throw new ArgumentException("This work type does already exist");
                c.WorkType.Add(new WorkType { Name = el });
                await c.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<string>> ShowAll()
        {
            return Task.Run(() =>
            {
                using (var c = new Context())
                {
                    var result = (from w in c.WorkType
                                  select w.Name).ToList();
                    return (IEnumerable<string>)result;
                }
            });
        }
    }
}