using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{
    public class Task
    {
        private string name;
        private int id;
        private string description;

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public Task()
        {
            Name = "";
            Id = 0;
            Description = "";
        }
        public Task( string _Name, int _Id,string _Description)
        {
            Name = _Name;
            Id = _Id;
            Description = _Description;
        }
    }
}
