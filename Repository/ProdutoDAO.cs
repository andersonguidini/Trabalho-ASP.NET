using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProdutoDAO : IRepository<Produto>
    {
        private readonly Context _context;

        public ProdutoDAO(Context context)
        {
            _context = context;
        }

        public Produto BuscarPorId(int id)
        {
            return _context.Produtos.Find(id);
        }

        public Produto BuscarPorNome(Produto objeto)
        {
            return _context.Produtos.FirstOrDefault(x => x.Nome.Equals(objeto.Nome));
        }

        public List<Produto> ListarTodos()
        {
            return _context.Produtos.ToList();
        }

        public bool Create(Produto objeto)
        {
            if (BuscarPorNome(objeto) == null)
            {
                _context.Produtos.Add(objeto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Remover(int id)
        {
            _context.Produtos.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public void Editar(Produto objeto)
        {
            _context.Produtos.Update(objeto);
            _context.SaveChanges();
        }

    }
}