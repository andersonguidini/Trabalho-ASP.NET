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
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {
            if (await _fornecedorDAO.Create(fornecedor))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "O fornecedor já existe");
            return View();
        }

        public IActionResult Edit(int id)
        {
            Fornecedor fornecedor = _fornecedorDAO.BuscarPorId(id);

            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Fornecedor f)
        {
            _fornecedorDAO.Edit(f);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _fornecedorDAO.Remover(id);

            return RedirectToAction("Index");
        }
    }
}
