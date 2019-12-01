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
    public class CategoriaController : Controller
    {
        private readonly CategoriaDAO _categoriaDAO;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;
            

        public CategoriaController(CategoriaDAO categoriaDAO)
        {
            _categoriaDAO = categoriaDAO;
            firebase = new FireSharp.FirebaseClient(config);
        }

        // GET: Categoria
        public IActionResult Index()
        {
            List<Categoria> categorias = new List<Categoria>();

            FirebaseResponse reponse = firebase.Get("Categoria/Counter");
            String counter = reponse.ResultAs<String>();

            int cont = 1;

            while (true)
            {
                reponse = firebase.Get("Categoria/"+cont);
                Categoria categoria = reponse.ResultAs<Categoria>();

                categorias.Add(categoria);

                if (cont == Convert.ToInt32(counter))
                {
                    break;
                }
                cont = cont + 1;
            }
            //FirebaseResponse reponse = firebase.Get("Categoria/");
            //List<Categoria> categorias = reponse.ResultAs<List<Categoria>>();

            return View(categorias);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria, string drpTipo)
        {
            FirebaseResponse reponse = firebase.Get("Categoria/Counter");
            String counter = reponse.ResultAs<String>();

            SetResponse reponseFirebase;
            Categoria result;

            if (counter == null)
            {
                reponseFirebase = await firebase.SetAsync("Categoria/Counter", "1");
                result = reponse.ResultAs<Categoria>();

                counter = "0";
            }

            Int32 intCounter = Convert.ToInt32(counter);
            intCounter = intCounter + 1;

            var data = categoria;

            reponseFirebase = await firebase.SetAsync("Categoria/"+intCounter, data);
            result = reponseFirebase.ResultAs<Categoria>();

            reponseFirebase = await firebase.SetAsync("Categoria/Counter", Convert.ToString(intCounter));
            //result = reponse.ResultAs<String>();

            return RedirectToAction("Index");
        }





        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Categoria categoria, string drpTipo)
        //{

        //    categoria.Tipo = drpTipo;
        //    if (_categoriaDAO.Create(categoria))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Essa categoria já existe!");
        //    return View(categoria);
        //}

        public IActionResult Edit(int id)
        {
            return View
                (_categoriaDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Edit(Categoria c)
        {
            _categoriaDAO.Editar(c);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _categoriaDAO.Remover(id);
            return RedirectToAction("Index");
        }

        /*
        

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Tipo")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
         */
    }
}
