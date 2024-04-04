using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_2.Repository
{
    class Class1
    {
        static void Main(string[] args)
        {
            ClassInterface repository = new ClassInterface();
            repository.read();
            repository.Update(25,"отдохнуть");
            repository.read();
        }
    }
}
