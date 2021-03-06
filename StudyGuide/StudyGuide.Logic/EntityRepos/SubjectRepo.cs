﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.DataBase.DBO;
using StudyGuide.DataBase;
using System.Data.Entity;

namespace StudyGuide.Logic.EntityRepos
{
    public class SubjectRepo
    {
        public async Task AddNew(string el)
        {
            using (var c = new Context())
            {
                if (await c.Subjects.FirstOrDefaultAsync(x => x.Name == el) != null)
                    throw new ArgumentException("This subject does already exist");
                c.Subjects.Add(new Subject { Name = el });
                await c.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<string>> ShowAll()
        {
            return Task.Run(() =>
            {
                using (var c = new Context())
                {
                    var result = (from f in c.Subjects
                                  select f.Name).ToList();
                    return (IEnumerable<string>)result;
                }
            });
        }
    }
}
