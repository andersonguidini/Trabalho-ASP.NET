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
        public async Task<IActionResult> Create(Produto produto, string drpTipo)
        {
            

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_produtoDAO.BuscarPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Produto p)
        {
            _produtoDAO.Edit(p);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _produtoDAO.Remover(id);

            return RedirectToAction("Index");
        }
    }
}
