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
    public class ProdutoFinalController : Controller
    {
        private readonly ProdutoFinalDAO _produtoFinalDAO;
        private readonly ReceitaDAO _receitaDAO;

        public ProdutoFinalController(ProdutoFinalDAO produtoFinalDAO, ReceitaDAO receitaDAO)
        {
            _receitaDAO = receitaDAO;
            _produtoFinalDAO = produtoFinalDAO;
            _receitaDAO = receitaDAO;
        }

        // GET: ProdutoFinal
        public IActionResult Index()
        {

            return View(_produtoFinalDAO.ListarTodos());
        }

        // GET: ProdutoFinal/Create
        public IActionResult Create()
        {
            ViewBag.Fornecedores = new SelectList(_receitaDAO.ListarTodos(),
                "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProdutoFinal produtoFinal, int drpReceita)
        {
            produtoFinal.Receita = _receitaDAO.BuscarPorId(Convert.ToInt32(drpReceita));
            await _produtoFinalDAO.Create(produtoFinal);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_produtoFinalDAO.BuscarPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoFinal pf)
        {
            _produtoFinalDAO.Edit(pf);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _produtoFinalDAO.Remover(id);

            return RedirectToAction("Index");
        }
    }
}
