using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface IRepository<T>
    {
        Task<bool> Create(T objeto);

        T BuscarPorId(int id);

        T BuscarPorNome(T objeto);

        void Remover(int? id);

        void Edit(T objeto);

        List<T> ListarTodos();

    }
}
