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

                if(categoria != null)
                {
                    categoria.Id = Convert.ToInt32(cont);
                    categorias.Add(categoria);
                }

                if (cont == Convert.ToInt32(counter))
                {
                    break;
                }
                cont = cont + 1;
            }

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

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            FirebaseResponse reponse = firebase.Get("Categoria/" + id);
            Categoria categoria = reponse.ResultAs<Categoria>();

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Categoria c)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Categoria/"+c.Id, c);
            Categoria result = reponseFirebase.ResultAs<Categoria>();

            return RedirectToAction("Index");
        }

        //public IActionResult Delete(int id)
        //{
        //    _categoriaDAO.Remover(id);
        //    return RedirectToAction("Index");
        //}

        // GET: Categoria/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FirebaseResponse reponse = await firebase.DeleteAsync("Categoria/" + id);
            Categoria categoria = reponse.ResultAs<Categoria>();

            return RedirectToAction("Index");
        }

        /*// POST: Categoria/Delete/5
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
