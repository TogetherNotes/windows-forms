using System.Collections.Generic;

namespace TogetherNotes.Utils
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? Rating { get; set; }
        public List<string> Genre { get; set; }
        public int? Capacity { get; set; }

    }
}
