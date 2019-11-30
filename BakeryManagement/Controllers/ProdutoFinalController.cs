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
    public class ProdutoFinalController : Controller
    {
        private readonly ProdutoFinalDAO _produtoFinalDAO;

        public ProdutoFinalController(ProdutoFinalDAO produtoFinalDAO)
        {
            _produtoFinalDAO = produtoFinalDAO;
        }

        // GET: ProdutoFinal
        public IActionResult Index()
        {
            return View(_produtoFinalDAO.ListarTodos());
        }

        // GET: ProdutoFinal/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProdutoFinal produtoFinal)
        {

            if (_produtoFinalDAO.Create(produtoFinal))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Esse produto já existe!");
            return View(produtoFinal);
        }

        public IActionResult Edit(int id)
        {
            return View
                (_produtoFinalDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Edit(ProdutoFinal pf)
        {
            _produtoFinalDAO.Editar(pf);
            return RedirectToAction("Index");
        }


        /*
        // POST: ProdutoFinal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Preco,Custo,PrazoValidade")] ProdutoFinal produtoFinal)
        {
            if (id != produtoFinal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoFinalExists(produtoFinal.Id))
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
            return View(produtoFinal);
        }

        // GET: ProdutoFinal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoFinal = await _context.ProdutosFinais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoFinal == null)
            {
                return NotFound();
            }

            return View(produtoFinal);
        }

        // POST: ProdutoFinal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoFinal = await _context.ProdutosFinais.FindAsync(id);
            _context.ProdutosFinais.Remove(produtoFinal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoFinalExists(int id)
        {
            return _context.ProdutosFinais.Any(e => e.Id == id);
        }

    */
    }
}
