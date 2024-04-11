using node.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using node1;

namespace node
{
    class TododoViewModel : ObservableObject
    {
        private ObservableCollection<Task> _lists = new ObservableCollection<Task>(new List<Task> {Randomizer.RandomTask(), Randomizer.RandomTask(), Randomizer.RandomTask(), Randomizer.RandomTask(), Randomizer.RandomTask() });

        public ObservableCollection<Task> Lists { get => _lists; set { _lists = value; OnPropertyChanged("Lists"); } }

        public Task SelectedTask { get => _selectedTask; set { _selectedTask = value; OnPropertyChanged("SelectedTask"); } }

        private Task _selectedTask;

    }
}
