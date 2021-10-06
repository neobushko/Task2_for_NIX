using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Interfaces
{
    interface DoWithFile<T>
    {
        public List<T> GetAll();
        public void Add(T elem);
        public T Seek(int id);
        public bool Delete(int id);
    }
}
