namespace DataBase.Repository
{
    public class Task
    {
        public Task(int id, string date_time, string name, string more_details)
        {
            Id = id;
            Date_time = date_time;
            Name = name;
            More_details = more_details;
        }

        public int Id { get; }
        public string Date_time { get; }
        public string Name { get; }
        public string More_details { get; }
    }
}