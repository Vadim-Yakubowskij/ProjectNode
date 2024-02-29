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
            Manager manager = Manager.Instance;
            Tasklist tmp = manager.All[0];

            for (int i = 0; i < tmp.list.Count; i++)
            {
                Console.WriteLine(tmp.list[i].Name);
                Console.WriteLine(tmp.list[i].Id);
                Console.WriteLine(tmp.list[i].Description);
            }

            Console.ReadKey();

            Task a = new Task("First","dldl");
            Task b = new Task("two", "deff");
            Tasklist spisok = new Tasklist("qwerty");
            spisok.Add(a);
            spisok.Add(b);

            for (int i = 0; i < spisok.list.Count; i++)
            {
                Console.WriteLine(spisok.list[i].Name);
                Console.WriteLine(spisok.list[i].Id);
                Console.WriteLine(spisok.list[i].Description);
            }
            manager.SetList(spisok);
            manager.SaveData();


            Console.ReadKey();

           
        }
    }
}
