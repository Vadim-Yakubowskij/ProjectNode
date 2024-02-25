using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{
    public class Tasklist
    {
        private string name;
        private int id;
        public List<Task> list;

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }

        public Tasklist(string Name, int Id)
        {
            name = Name;
            id = Id;
            List<Task> List = new List<Task>();
            list = List;
        }
        public Tasklist()
        {
            name = "";
            id = 0;
            List<Task> List = new List<Task>();
            list = List;
        }
        public void Add(Task element)
        {
            list.Add(element);
        }
        public class CardListData : UniqueData
        {
            public bool IsEssential;
            public List<CardData> List;
        }

    }
}
