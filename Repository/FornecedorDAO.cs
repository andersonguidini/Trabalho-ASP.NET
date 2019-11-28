using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class FornecedorDAO : IRepository<Fornecedor>
    {
        private readonly Context _context;

        public FornecedorDAO(Context context)
        {
            _context = context;
        }

        public Fornecedor BuscarPorId(int id)
        {
            return _context.Fornecedores.Find(id);
        }

        public Fornecedor BuscarPorNome(Fornecedor objeto)
        {
            return _context.Fornecedores.FirstOrDefault(x => x.Nome.Equals(objeto.Nome));
        }

        public List<Fornecedor> ListarTodos()
        {
            return _context.Fornecedores.ToList();
        }

        public bool Create(Fornecedor objeto)
        {
            if (BuscarPorNome(objeto) == null)
            {
                _context.Fornecedores.Add(objeto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Remover(int id)
        {
            _context.Fornecedores.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public void Editar(Fornecedor objeto)
        {
            _context.Fornecedores.Update(objeto);
            _context.SaveChanges();
        }
    }
}