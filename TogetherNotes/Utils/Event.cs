﻿namespace TogetherNotes.Utils
{
    public class Event
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Title { get; set; }

        public Event(string timestamp, string title)
        {
            this.Timestamp = DateTimeOffset.Parse(timestamp);
            this.Title = title;
        }
    }

}
