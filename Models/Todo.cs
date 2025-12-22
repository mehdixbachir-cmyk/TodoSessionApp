namespace TodoSessionApp.Models
{
    public class Todo
    {
        public string Libelle { get; set; }
        public string Description { get; set; }
        public string State { get; set; } // ToDo, Doing, Done
        public DateTime Date { get; set; }
    }
}
