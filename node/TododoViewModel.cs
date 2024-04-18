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
        private ObservableCollection<TaskList> _listsList = new ObservableCollection<TaskList>(new List<TaskList> {});

        public ObservableCollection<TaskList> ListsList { get => _listsList; set { _listsList = value; OnPropertyChanged("ListsList"); } }

        private ObservableCollection<Task> _lists = new ObservableCollection<Task>(new List<Task> {Randomizer.RandomTask(), Randomizer.RandomTask(), Randomizer.RandomTask(), Randomizer.RandomTask(), Randomizer.RandomTask() });

        public ObservableCollection<Task> Lists { get => _lists; set { _lists = value; OnPropertyChanged("Lists"); } }

        public Task SelectedTask { get => _selectedTask; set { _selectedTask = value; OnPropertyChanged("SelectedTask"); } }

        public string DataTimee { get => _dataTimee; set { _dataTimee = value; OnPropertyChanged("DataTimee"); } }

        private Task _selectedTask;

        private string _dataTimee;

        private DateTime _datePointer;

        public TododoViewModel()
        {
            ListsList = new ObservableCollection<TaskList>()
            {
                new TaskList(
                    Lists
                    ),
                 new TaskList(
                Lists
                    ),
                  new TaskList(
                Lists
                    ),
                   new TaskList(
                Lists
                    ),
                    new TaskList(
                Lists
                    ),
                     new TaskList(
                Lists
                    ),
            };
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
