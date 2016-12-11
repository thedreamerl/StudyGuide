using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.DataBase.DBO;
using StudyGuide.DataBase;

namespace StudyGuide.Logic.EntityRepos
{
    public class WorkTypeRepo
    {
        public void AddNew(string el)
        {
            using (var c = new Context())
            {
                if (c.WorkType.FirstOrDefault(x => x.Name == el) != null)
                    throw new ArgumentException("This work type does already exist");
                c.WorkType.Add(new WorkType { Name = el });
                c.SaveChanges();
            }
        }

        public IQueryable<string> ShowAll()
        {
            using (var c = new Context())
            {
                var result = from w in c.WorkType
                             select w.Name;
                return result;
            }
        }
    }
}