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
    public class FornecedorController : Controller
    {
        private readonly FornecedorDAO _fornecedorDAO;

        public FornecedorController(FornecedorDAO fornecedorDAO)
        {
            _fornecedorDAO = fornecedorDAO;
        }

        // GET: Fornecedor
        public IActionResult Index()
        {
            return View(_fornecedorDAO.ListarTodos());
        }

        // GET: Fornecedor/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                if (_fornecedorDAO.Create(fornecedor))
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Esse fornecedor já existe!");
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Edit/5
        public IActionResult Edit(int id)
        {
            return View(_fornecedorDAO.BuscarPorId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Fornecedor fornecedor)
        {
            
        }

        // GET: Fornecedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }
}
