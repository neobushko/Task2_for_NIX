using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task2.Interfaces;
using task2.Entity;
using System.IO;

namespace task2.Repository
{
    class ClientRepository : IDoWithFile<Client>
    {
        private char sep = '|';
        private string Address;
        public ClientRepository(string Address)
        {
            this.Address = Address;
        }
        public void Add(Client elem)
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
                    sw.WriteLine($"{elem.Id}{sep}{elem.Name}{sep}{elem.Birthday}{sep}{elem.Passport}");
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
                                    sw.WriteLine($"{buff[0]}{sep}{buff[1]}{sep}{buff[2]}{sep}{buff[3]}");
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

        public List<Client> GetAll()
        {
            List<Client> clients = new List<Client>();
            string[] buff;
            using (var f = File.Open(Address, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(f))
                {
                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        clients.Add(new Client(Convert.ToInt32(buff[0]), buff[1], Convert.ToDateTime(buff[2]), buff[3]));
                    }
                }
            }
            return clients;
        }

        public Client Seek(int id)
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
                            return new Client(Convert.ToInt32(buff[0]), buff[1], Convert.ToDateTime(buff[2]), buff[3]);
                    }
                }
            }
            return new Client(-1, "", DateTime.Parse("1/1/0001"), "");
        }
    }
}
