using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.DataBase.DBO;
using StudyGuide.DataBase;

namespace StudyGuide.Logic.EntityRepos
{
    public class SubjectRepo
    {
        public void AddNew(string el)
        {
            using (var c = new Context())
            {
                if (c.Subjects.FirstOrDefault(x => x.Name == el) != null)
                    throw new ArgumentException("This subject does already exist");
                c.Subjects.Add(new Subject { Name = el });
                c.SaveChanges();
            }
        }

        public IEnumerable<string> ShowAll()
        {
            using (var c = new Context())
            {
                var result = (from f in c.Subjects
                             select f.Name).ToList();
                return result;
            }
        }
    }
}
