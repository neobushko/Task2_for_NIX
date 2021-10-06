using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using task2.Entity;
using task2.Interfaces;

namespace task2.Repository
{
    class BookingRepository : DoWithFile<Booking>
    {
        private char sep = '|';
        private string Address;
        public BookingRepository(string Address)
        {
            this.Address = Address;
        }

        public List<Room> FindRoomForDate(DateTime date, RoomRepository roomRepository)
        {
            List<Room> rooms = roomRepository.GetAll();
            List<Room> freeRooms = new List<Room>();
            bool isBusy;
            string[] buff;

            foreach (var room in rooms)
            {
                using (var f = File.Open(Address, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    using (var sr = new StreamReader(f))
                    {

                        isBusy = false;
                        while (sr.Peek() > -1)
                        {

                            buff = sr.ReadLine().Split(sep);

                            if (room.Id == Convert.ToInt32(buff[2]))
                            {
                                if (DateTime.Compare(date, Convert.ToDateTime(buff[3])) < 0)
                                    continue;
                                else if (DateTime.Compare(date, Convert.ToDateTime(buff[4])) <= 0)
                                    isBusy = true;
                            }
                        }
                    }
                }
                if (!isBusy)
                    freeRooms.Add(room);
            }
            return freeRooms;
        }
        public List<Room> FindRoomForDate(DateTime checkIn, DateTime checkOut, RoomRepository roomRepository)
        {
            List<Room> rooms = roomRepository.GetAll();
            List<Room> freeRooms = new List<Room>();
            bool isBusy;
            string[] buff;

            foreach (var room in rooms)
            {
                using (var f = File.Open(Address, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    using (var sr = new StreamReader(f))
                    {
                        isBusy = false;
                        while (sr.Peek() > -1)
                        {
                            buff = sr.ReadLine().Split(sep);

                            if (room.Id == Convert.ToInt32(buff[2]))
                            {
                                if (DateTime.Compare(checkOut, Convert.ToDateTime(buff[3])) < 0)
                                    continue;
                                else if (DateTime.Compare(checkOut, Convert.ToDateTime(buff[4])) <= 0)
                                    isBusy = true;
                                else if (DateTime.Compare(checkIn, Convert.ToDateTime(buff[4])) <= 0)
                                    isBusy = true;
                                else continue;
                            }
                        }
                    }
                }
                if (!isBusy)
                    freeRooms.Add(room);
            }

            return freeRooms;
        }
        public void MakeBooking(int id, Client client, Room room, DateTime checkIn, DateTime checkOut)
        {
            DateTime now = DateTime.Now;
            bool isExisting = false;
            string[] buff;
            using (var f = File.Open(Address, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(f))
                {

                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        if (Convert.ToInt32(buff[2]) == room.Id)
                        {
                            if (DateTime.Compare(checkOut, Convert.ToDateTime(buff[3])) < 0)
                                continue;
                            else if (DateTime.Compare(checkOut, Convert.ToDateTime(buff[4])) <= 0)
                                isExisting = true;
                            else if (DateTime.Compare(checkIn, Convert.ToDateTime(buff[4])) <= 0)
                                isExisting = true;
                            else continue;
                        }
                    }
                }
            }

            if(!isExisting)
            {
                this.Add(new Booking(id, client.Id, room.Id, checkIn, checkOut, now));
            }
        }
        public void Add(Booking elem)
        {
            bool isExisting = false;
            string[] buff;
            using (var f = File.Open(Address, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(f))
                {

                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        if (Convert.ToInt32(buff[0]) == elem.Id)
                            isExisting = true;
                    }
                }
            }
            if (!isExisting)
                using (var sw = new StreamWriter(Address, true))
                {
                    sw.WriteLine($"{elem.Id}{sep}{elem.ClientId}{sep}{elem.RoomId}{sep}{elem.CheckIn}{sep}{elem.CheckOut}{sep}{elem.BookingDate}");
                }
        }
        public bool Delete(int id)
        {
            string[] buff;
            bool IsDeleted = false;
            var tempFile = Path.GetTempFileName();
            using (var f = File.Open(Address, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var f2 = File.Open(tempFile, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (var sr = new StreamReader(f))
                    {
                        using (var sw = new StreamWriter(f2))
                        {
                            while (sr.Peek() > -1)
                            {
                                buff = sr.ReadLine().Split(sep);
                                if (Convert.ToInt32(buff[0]) != id)
                                {
                                    sw.WriteLine($"{buff[0]}{sep}{buff[1]}{sep}{buff[2]}{sep}{buff[3]}{sep}{buff[4]}{sep}{buff[5]}");
                                }
                                else IsDeleted = true;
                            }
                        }
                    }
                }
            }
            File.Delete(Address);
            File.Move(tempFile, Address);
            return IsDeleted;
        }
        public List<Booking> GetAll()
        {
            List<Booking> records = new List<Booking>();
            string[] buff;
            using (var f = File.Open(Address, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(f))
                {
                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        records.Add(new Booking(Convert.ToInt32(buff[0]), Convert.ToInt32(buff[1]), Convert.ToInt32(buff[2]), 
                            Convert.ToDateTime(buff[3]), Convert.ToDateTime(buff[4]), Convert.ToDateTime(buff[5])));
                    }
                }
            }
            return records;
        }
        public Booking Seek(int id)
        {
            string[] buff;
            using (var f = File.Open(Address, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(f))
                {

                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        if (Convert.ToInt32(buff[0]) == id)
                            return new Booking(Convert.ToInt32(buff[0]), Convert.ToInt32(buff[1]), Convert.ToInt32(buff[2]), 
                                Convert.ToDateTime(buff[3]), Convert.ToDateTime(buff[4]), Convert.ToDateTime(buff[5]));
                    }
                }
            }
            return new Booking(-1, -1, -1, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }
    }
}
