using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task2.Entity;
using task2.Interfaces;

namespace task2.Repository
{
    class RoomRepository : IDoWithFile<Room>
    {
        private char sep = '|';
        private string _Address;
        public string Adress
        {
            get => _Address;
        }
        public RoomRepository(string Address)
        {
            this._Address = Address;
        }

        public char Separator { get => sep; set => sep = value; }

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();
            string[] buff;
            using (var f = File.Open(_Address, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(f))
                {
                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        rooms.Add(new Room(Convert.ToInt32(buff[0]), Convert.ToInt32(buff[1]), buff[2], Convert.ToDecimal(buff[3])));
                    }
                }
            }

            return rooms;
        }
        public void Add(Room elem)
        {
            bool isExisting = false;
            string[] buff;
            using (var f = File.Open(_Address, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
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
                using (var sw = new StreamWriter(_Address, true))
                {
                    sw.WriteLine($"{elem.Id}{sep}{elem.RoomNumber}{sep}{elem.Category}{sep}{elem.Price}");
                }

        }
        // If there could be several rooms with the same number, you need to create a List of rooms in which the rooms will be entered in the cycle, and the return should be taken out for the cycle and left alone.
        public Room Seek(int id)
        {
            string[] buff;
            using (var f = File.Open(_Address, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(f))
                {

                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        if (Convert.ToInt32(buff[0]) == id)
                            return new Room(Convert.ToInt32(buff[0]), Convert.ToInt32(buff[1]), buff[2], Convert.ToDecimal(buff[3]));
                    }
                }
            }
            return new Room(-1, -1, "null", -1);
        }
        public bool Delete(int id)
        {
            string[] buff;
            bool IsDeleted = false;
            var tempFile = Path.GetTempFileName();
            using (var f = File.Open(_Address, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
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
                                    sw.WriteLine($"{buff[0]}{sep}{buff[1]}{sep}{buff[2]}{sep}{buff[3]}");
                                }
                                else IsDeleted = true;
                            }
                        }
                    }
                }
            }
            File.Delete(_Address);
            File.Move(tempFile, _Address);
            return IsDeleted;


        }
    }

}
