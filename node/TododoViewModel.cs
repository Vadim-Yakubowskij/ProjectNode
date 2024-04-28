using node.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using node1;

using DataBase.Repository;
using Task = DataBase.Repository.Task;

namespace node
{
    class TododoViewModel : ObservableObject
    {
        private ObservableCollection<Task> _taskListSunday;
        public ObservableCollection<Task> TaskListSunday { get => _taskListSunday; set { _taskListSunday = value; OnPropertyChanged("TaskListSunday"); } }

        private ObservableCollection<Task> _taskListMonday;
        public ObservableCollection<Task> TaskListMonday { get => _taskListMonday; set { _taskListMonday = value; OnPropertyChanged("TaskListMonday"); } }

        private ObservableCollection<Task> _taskListTuesday;
        public ObservableCollection<Task> TaskListTuesday { get => _taskListTuesday; set { _taskListTuesday = value; OnPropertyChanged("TaskListTuesday"); } }

        private ObservableCollection<Task> _taskListWensday;
        public ObservableCollection<Task> TaskListWensday { get => _taskListWensday; set { _taskListWensday = value; OnPropertyChanged("TaskListWensday"); } }

        private ObservableCollection<Task> _taskListThursday;
        public ObservableCollection<Task> TaskListThursday { get => _taskListThursday; set { _taskListThursday = value; OnPropertyChanged("TaskListThursday"); } }

        private ObservableCollection<Task> _taskListFriday;
        public ObservableCollection<Task> TaskListFriday { get => _taskListFriday; set { _taskListFriday = value; OnPropertyChanged("TaskListFriday"); } }

        private ObservableCollection<Task> _taskListSaturday;
        public ObservableCollection<Task> TaskListSaturday { get => _taskListSaturday; set { _taskListSaturday = value; OnPropertyChanged("TaskListSaturday"); } }
        public Task SelectedTask { get => _selectedTask; set { _selectedTask = value; OnPropertyChanged("SelectedTask"); } }

        public string DataTimee { get => _dataTimee; set { _dataTimee = value; OnPropertyChanged("DataTimee"); } }

        private Task _selectedTask;

        private string _dataTimee;

        private DateTime _datePointer;
        private TaskRepository _tasklistRepository;
        private TaskRepository TasklistRepository { get => _tasklistRepository; set => _tasklistRepository = value; }

        public TododoViewModel()
        {

            TasklistRepository = new TaskRepository();
            TaskListMonday =  new ObservableCollection<Task>(TasklistRepository.read());
            TaskListThursday = new ObservableCollection<Task>(TasklistRepository.read());
            TaskListWensday = new ObservableCollection<Task>(TasklistRepository.read());
            TaskListFriday = new ObservableCollection<Task>(TasklistRepository.read());
            TaskListSaturday = new ObservableCollection<Task>(TasklistRepository.read());
            TaskListSunday = new ObservableCollection<Task>(TasklistRepository.read());
            TaskListTuesday =  new ObservableCollection<Task>(TasklistRepository.read());
            DateTime today = DateTime.Today;
            _datePointer = DateTime.Today;
            int day = today.Day;
            int month = today.Month;
            int year = today.Year;
            DataTimee = GetCurrentWeekDay($"{day:00}.{month:00}.{year:00}");
        }
        public string GetCurrentWeekDay(string inputDate)
         {
            int delta = 0;
            if (DateTime.TryParseExact(inputDate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {

                DayOfWeek today = date.DayOfWeek;
                if (today == DayOfWeek.Sunday)
                {
                    delta = -6;
                }
                else
                {
                    delta = DayOfWeek.Monday - today;
                }
                DateTime monday = date.AddDays(delta);
                DateTime sunday = monday.AddDays(6);
                return $"{monday:dd.MM} - {sunday:dd.MM}";
            }
            else
            {
                return "";
            }

        }

        private RelayCommand nextWeekcommand; 
        public RelayCommand NextWeekCommand
        {
            get
            {
                return nextWeekcommand ??
                    (nextWeekcommand = new RelayCommand(obj =>
                    {
                        _datePointer = _datePointer.AddDays(7);

                        int day = _datePointer.Day;
                        int month = _datePointer.Month;
                        int year = _datePointer.Year;
                        DataTimee = GetCurrentWeekDay($"{day:00}.{month:00}.{year:00}");
                    }));
            }
        }
        private RelayCommand prevWeekcommand;
        public RelayCommand PrevWeekCommand
        {
            get
            {
                return prevWeekcommand ??
                    (prevWeekcommand = new RelayCommand(obj =>
                    {
                        _datePointer = _datePointer.AddDays(-7);

                        int day = _datePointer.Day;
                        int month = _datePointer.Month;
                        int year = _datePointer.Year;
                        DataTimee = GetCurrentWeekDay($"{day:00}.{month:00}.{year:00}");
                    }));
            }
        }

    }


}
