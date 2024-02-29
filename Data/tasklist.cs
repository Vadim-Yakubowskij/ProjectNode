using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{
    public class Tasklist : Unique<Tasklist>, IUniqueIdentifiable
    {
        public List<Task> list;

        public Tasklist(string Name) : base(Name)
        {
            List<Task> List = new List<Task>();
            list = List;
        }
        public void Add(Task element)
        {
            list.Add(element);
        }

        public Tasklist()
        {

        }
    }
}
