using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogetherNotes.Utils
{
    class Event
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
