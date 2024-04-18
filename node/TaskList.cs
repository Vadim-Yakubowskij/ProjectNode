using node1;
using System.Collections.ObjectModel;

namespace node
{
    internal class TaskList
    {
        public TaskList(ObservableCollection<Task> lists)
        {
            Lists = lists;
        }

        public string Name { get;set;}
        public ObservableCollection<Task> Lists { get; set; }
    }
}