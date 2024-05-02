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
    }
}