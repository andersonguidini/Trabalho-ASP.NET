using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CategoriaDAO : IRepository<Categoria>
    {
        private readonly Context _context;

        public CategoriaDAO(Context context)
        {
            _context = context;
        }

        public Categoria BuscarPorId(int id)
        {
            return _context.Categorias.Find(id);
        }

        public Categoria BuscarPorNome(Categoria c)
        {
            return _context.Categorias.FirstOrDefault(x => x.Nome.Equals(c.Nome));
        }

        public List<Categoria> ListarTodos()
        {
            return _context.Categorias.ToList();
        }

        public bool Create(Categoria objeto)
        {
            if (BuscarPorNome(objeto) == null)
            {
                _context.Categorias.Add(objeto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Remover(int id)
        {
            _context.Categorias.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public void Editar(Categoria objeto)
        {
            _context.Categorias.Update(objeto);
            _context.SaveChanges();
        }
    }
}
