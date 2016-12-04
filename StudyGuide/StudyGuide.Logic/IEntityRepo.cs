using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic
{
    interface IEntityRepo<T>
    {
        List<T> ShowAll();
        void AddNew(T el);          
    }
}
