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
using System.Collections;

namespace BakeryManagement.Controllers
{
    public class ReceitaController : Controller
    {
        private readonly ReceitaDAO _receitaDAO;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public ReceitaController(ReceitaDAO receitaDAO)
        {
            _receitaDAO = receitaDAO;
            firebase = new FireSharp.FirebaseClient(config);
        }

        // GET: Receita
        public IActionResult Index()
        {
            return View (_receitaDAO.ListarTodos());
        }

        // GET: Receita/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Receita receita)
        {
            var data = receita;
           
            SetResponse reponse = await firebase.SetAsync("Receita/" + data.Nome, data);
            
            Receita result = reponse.ResultAs<Receita>();

            return RedirectToAction("Index");
        }

        // GET: Receita/Edit/5
        public IActionResult Edit(int id)
        {
            return View
                (_receitaDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Edit(Receita r)
        {
            _receitaDAO.Editar(r);
            return RedirectToAction("Index");
        }

        //    // POST: Receita/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Rendimento,TempoDePreparo")] Receita receita)
        //    {
        //        if (id != receita.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(receita);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ReceitaExists(receita.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(receita);
        //    }

        //    // GET: Receita/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var receita = await _context.Receitas
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (receita == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(receita);
        //    }

        //    // POST: Receita/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var receita = await _context.Receitas.FindAsync(id);
        //        _context.Receitas.Remove(receita);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ReceitaExists(int id)
        //    {
        //        return _context.Receitas.Any(e => e.Id == id);
        //    }
    }
}
