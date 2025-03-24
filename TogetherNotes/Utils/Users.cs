using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TogetherNotes.Utils
{
    public class Users
    {
        public Users(int Id, string Fullname, string Mail, string Password, string Role)
        {
            this.Id = Id;
            this.Fullname = Fullname;
            this.Mail = Mail;
            this.Password = Password;
            this.Role = Role;
        }

        public Users(int Id, string Fullname, string Mail, string Password, string Role, int Rating, int Capacity)
        {
            this.Id = Id;
            this.Fullname = Fullname;
            this.Mail = Mail;
            this.Password = Password;
            this.Role = Role;
            this.Rating = Rating;
            this.Capacity = Capacity;
        }

        public Users(int Id, string Fullname, string Mail, string Password, string Role, int Rating, string Genre)
        {
            this.Id = Id;
            this.Fullname = Fullname;
            this.Mail = Mail;
            this.Password = Password;
            this.Role = Role;
            this.Rating = Rating;
            this.Genre = Genre;
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Rating { get; set; }
        public int Capacity { get; set; }
        public string Genre { get; set; }


    }
}
