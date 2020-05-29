using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Souq.Service
{
    public interface IServiceBase<T>
    {
        List<T> GetAll();
        T GetById(int id);
        int Add(T Model);
        int Update(int id, T Model);
        int Delete(int id);

    }
}