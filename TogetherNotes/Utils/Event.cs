namespace TogetherNotes.Utils
{
    public class Event
    {
        public Event(long time, string title)
        {
            this.Timestamp = time;
            this.Title = title;
        }

        public long Timestamp { get; set; }
        public string Title { get; set; }
    }
}
