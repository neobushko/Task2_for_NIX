using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Entity
{
    
    class Booking
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking(int id, int clientId, int roomId, DateTime checkIn, DateTime checkOut, DateTime bookinDate)
        {
            this.Id = id;
            this.ClientId = clientId;
            this.RoomId = roomId;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
            this.BookingDate = bookinDate;
        }

        public Booking(int id, int clientId, int roomId, DateTime checkIn, DateTime checkOut)
        {
            this.Id = id;
            this.ClientId = clientId;
            this.RoomId = roomId;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
            this.BookingDate = this.CheckIn;
        }


    }
}
