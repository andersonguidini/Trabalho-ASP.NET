using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository;

namespace BakeryManagement.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoDAO _produtoDAO;

        public ProdutoController(ProdutoDAO produto)
        {
            _produtoDAO = produto;
        }

        // GET: Produto
        public IActionResult Index()
        {
            return View(_produtoDAO.ListarTodos());
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produto produto)
        {
            
            if (_produtoDAO.Create(produto))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Esse produto já existe!");
            return View(produto);
        }

        public IActionResult Edit(int id)
        {
            return View
                (_produtoDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Edit(Produto p)
        {
            _produtoDAO.Editar(p);
            return RedirectToAction("Index");
        }

        /*
        // POST: Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Quantidade,Unidade,PrazoValidade")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
        */
    }
}
