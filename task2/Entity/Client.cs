using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Entity
{
    class Client
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Passport { get; set; }

        public Client(int id, string name, DateTime birthday, string passport)
        {
            this.Id = id;
            this.Name = name;
            this.Birthday = birthday;
            this.Passport = passport;
        }
    }
}
