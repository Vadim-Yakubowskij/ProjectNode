using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{

    public class Task : Unique<Task>, IUniqueIdentifiable
    {
        public DateTime Created;
        public DateTime? Updated;
        public DateTime? Planned;
        private string description;

        public string Description { get => description; set => description = value; }
        public Task(string _Name, string _Description) : base(_Name)
        {
            Description = _Description;
        }
    }

}
