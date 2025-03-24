using System;

namespace TogetherNotes.Utils
{
    public class Event
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Title { get; set; }

        public Event(DateTimeOffset timestamp, string title)
        {
            this.Timestamp = timestamp;
            this.Title = title;
        }
    }
}