using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ReceitaDAO : IRepository<Receita>
    {
        private readonly Context _context;

        public ReceitaDAO(Context context)
        {
            _context = context;
        }

        public Receita BuscarPorId(int id)
        {
            return _context.Receitas.Find(id);
        }

        public Receita BuscarPorNome(Receita objeto)
        {
            return _context.Receitas.FirstOrDefault(x => x.Nome.Equals(objeto.Nome));
        }

        public  List<Receita> ListarTodos()
        {
            return _context.Receitas.ToList();
        }

        public bool Create(Receita objeto)
        {
            if (BuscarPorNome(objeto) == null)
            {
                _context.Receitas.Add(objeto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Remover(int id)
        {
            _context.Receitas.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public void Editar(Receita objeto)
        {
            _context.Receitas.Update(objeto);
            _context.SaveChanges();
        }
    }
}