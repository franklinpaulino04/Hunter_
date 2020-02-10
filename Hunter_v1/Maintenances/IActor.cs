using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenances
{
    public interface IActor<T>
    {
        int insert(T obj);
        int update(T obj);
        List<T> Datatable();
        T Find(int Id);
        int delete(int Id);
    }
}
