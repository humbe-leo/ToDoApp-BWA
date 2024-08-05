namespace ToDoApp.Client.Models
{
    public class FilterCriteria
    {
        public string TitleFilter { get; set; }
        public DateTime? DateFilter { get; set; }
        public string StatusFilter { get; set; }
    }
}
