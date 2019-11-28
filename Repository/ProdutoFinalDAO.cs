using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProdutoFinalDAO : IRepository<ProdutoFinal>
    {
        private readonly Context _context;

        public ProdutoFinalDAO(Context context)
        {
            _context = context;
        }

        public ProdutoFinal BuscarPorId(int id)
        {
            return _context.ProdutosFinais.Find(id);
        }

        public ProdutoFinal BuscarPorNome(ProdutoFinal objeto)
        {
            return _context.ProdutosFinais.FirstOrDefault(x => x.Receita.Equals(objeto.Receita));
        }

        public List<ProdutoFinal> ListarTodos()
        {
            return _context.ProdutosFinais.ToList();
        }

        public bool Create(ProdutoFinal objeto)
        {
            if (BuscarPorNome(objeto) == null)
            {
                _context.ProdutosFinais.Add(objeto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Remover(int id)
        {
            _context.ProdutosFinais.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public void Editar(ProdutoFinal objeto)
        {
            _context.ProdutosFinais.Update(objeto);
            _context.SaveChanges();
        }

    }
}