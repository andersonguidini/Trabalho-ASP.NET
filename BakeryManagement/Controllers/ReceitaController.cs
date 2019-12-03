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
        private readonly ProdutoDAO _produtoDAO;

        public ReceitaController(ReceitaDAO receitaDAO, ProdutoDAO produtoDAO)
        {
            _receitaDAO = receitaDAO;
            _produtoDAO = produtoDAO;
        }

        // GET: Receita
        public IActionResult Index()
        {
            return View(_receitaDAO.ListarTodos());
        }

        // GET: Receita/Create
        public IActionResult Create()
        {
            ViewBag.Produtos = _produtoDAO.ListarTodos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Receita receita, string drpProdutos)
        {
            

            return RedirectToAction("Index");
        }

        public IActionResult AddProduto()
        {


            return View();
        }

        public IActionResult Edit(int id)
        {
            return View(_receitaDAO.BuscarPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Receita r)
        {
            _receitaDAO.Edit(r);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _receitaDAO.Remover(id);

            return RedirectToAction("Index");
        }

    }
}
