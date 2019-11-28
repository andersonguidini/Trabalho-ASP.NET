using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    interface IRepository<T>
    {
        bool Create(T objeto);

        T BuscarPorId(int id);

        T BuscarPorNome(T objeto);

        void Remover(int id);

        void Editar(T objeto);

        List<T> ListarTodos();


    }
}
