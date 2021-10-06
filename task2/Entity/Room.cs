using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Entity
{
    class Room
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int RoomNumber { get; set; }

        public Room(int id, int roomNumber, string category, decimal price)
        {
            this.Id = id;
            this.RoomNumber = roomNumber;
            this.Category = category;
            this.Price = price;
        }
    }
}
