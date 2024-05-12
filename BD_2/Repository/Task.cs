using System;

namespace DataBase.Repository
{
    public class Task
    {
        public Task(Task x)
        {
            Id = x.Id;
            Date_time = x.Date_time;
            Name = x.Name;
            More_details = x.More_details;
            if(x.More_details.Length <= 5)
                More_details_short = x.More_details;
            else
            {
                More_details_short = x.More_details.Substring(0, Math.Min(x.More_details.Length,5)) + "...";
            }
            if (x.Name.Length <= 5)
                Name_short = x.Name;
            else
            {
                Name_short = x.Name.Substring(0, Math.Min(x.Name.Length, 5)) + "...";
            }
        }

        public Task(int id, string date_time, string name, string more_details)
        {
            Id = id;
            Date_time = date_time;
            Name = name;
            More_details = more_details;
        }

        public int Id { get; }
        public string Date_time { get; set; }
        public string Name { get; set; }
        public string More_details { get; set; }
        public string More_details_short { get; set; }
        public string Name_short { get; set; }
    }
}