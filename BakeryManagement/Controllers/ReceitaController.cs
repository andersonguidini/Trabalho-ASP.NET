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
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Receita receita, string drpProdutos)
        {
            await _receitaDAO.Create(receita);
            TempData["Receita"] = receita.Nome;

            return RedirectToAction("AddIngredientes");
        }

        public IActionResult AddIngredientes(Receita r)
        {
            if(r.Id != 0)
            {
                TempData["Receita"] = r.Nome;

                ViewBag.Produtos = _receitaDAO.BuscarIngredientes(r);
            } else
            {
                ViewBag.Produtos = new List<Produto>();
            }
            return View(_produtoDAO.ListarTodos());
        }

        public IActionResult AddIngrediente(Produto p)
        {
            string nomeReceita = TempData["Receita"].ToString();

            Produto produto = _produtoDAO.BuscarPorId(p.Id);
            Receita receita = _receitaDAO.BuscarPorNome(new Receita { Nome = nomeReceita });
            _receitaDAO.AddIngrediente(receita, produto);

            TempData["Receita"] = receita.Nome;

            return RedirectToAction(nameof(AddIngredientes));
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
