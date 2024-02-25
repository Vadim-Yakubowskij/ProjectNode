using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task a = new Task("First",10,"dldl");
            Task b = new Task("two", 11 , "deff");
            Task c = new Task();
            Tasklist spisok = new Tasklist();
            spisok.Add(a);
            spisok.Add(b);
            spisok.Add(c);

            for (int i = 0; i < spisok.list.Count; i++)
            {
                Console.WriteLine(spisok.list[i].Name);
                Console.WriteLine(spisok.list[i].Id);
                Console.WriteLine(spisok.list[i].Description);
            }
            Console.ReadKey();

        }
    }
}
