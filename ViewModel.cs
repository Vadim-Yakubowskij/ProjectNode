using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{
    class ViewModel
    {
        public void print(Tasklist element)
        {
            for (int i = 0; i < element.All.Count; i++)
            {
                Console.WriteLine(element.All[i]);
                Console.WriteLine(element.All[i].Id);
                Console.WriteLine(element.All[i].Description);

            }
        }
    }
}
