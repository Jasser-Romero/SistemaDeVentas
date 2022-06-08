using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.AppCore.Interfaces
{
    public interface IService<T>
    {
        int Create(T t);
        int Update(T t);
        bool Delete(T t);
        List<T> GetAll();
    }
}
