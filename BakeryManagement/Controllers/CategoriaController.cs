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

        public CategoriaController(CategoriaDAO categoriaDAO)
        {
            _categoriaDAO = categoriaDAO;
        }

        // GET: Categoria
        public IActionResult Index()
        {
            return View(_categoriaDAO.ListarTodos().ToList());
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria, string drpTipo)
        {
            if(await _categoriaDAO.Create(categoria))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "A categoria já existe");
            return View();
        }

        public IActionResult Edit(int id)
        {
            Categoria categoria = _categoriaDAO.BuscarPorId(id);

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Categoria c)
        {
            _categoriaDAO.Edit(c);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _categoriaDAO.Remover(id);

            return RedirectToAction("Index");
        }
    }
}
