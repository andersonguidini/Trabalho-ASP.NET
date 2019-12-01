using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;

namespace BakeryManagement.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly FornecedorDAO _fornecedorDAO;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public FornecedorController(FornecedorDAO fornecedorDAO)
        {
            _fornecedorDAO = fornecedorDAO;
            firebase = new FireSharp.FirebaseClient(config);
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
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {
            var data = fornecedor;

            SetResponse reponse = await firebase.SetAsync("Fornecedor/" + data.Nome, data);
            Fornecedor result = reponse.ResultAs<Fornecedor>();

            return RedirectToAction("Index");
        }





        // GET: Fornecedor/Edit/5
        public IActionResult Edit(int id)
        {
            return View
                (_fornecedorDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Edit(Fornecedor f)
        {
            _fornecedorDAO.Editar(f);
            return RedirectToAction("Index");
        }

        //// POST: Fornecedor/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Fornecedor fornecedor)
        //{
        //    if (id != fornecedor.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(fornecedor);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FornecedorExists(fornecedor.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(fornecedor);
        //}

        //// GET: Fornecedor/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var fornecedor = await _context.Fornecedores
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (fornecedor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(fornecedor);
        //}

        //// POST: Fornecedor/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var fornecedor = await _context.Fornecedores.FindAsync(id);
        //    _context.Fornecedores.Remove(fornecedor);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool FornecedorExists(int id)
        //{
        //    return _context.Fornecedores.Any(e => e.Id == id);
        //}
    }
}
