using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace BakeryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoFinalDAO _produtoFinalDAO;
        private readonly ProdutoDAO _produtoDAO;

        public HomeController(ProdutoFinalDAO produtoFinalDAO, ProdutoDAO produtoFinal)
        {
            _produtoFinalDAO = produtoFinalDAO;
            _produtoDAO = produtoFinal;
        }

        public IActionResult Index()
        {
            ViewBag.ProdutoFinal = _produtoFinalDAO.ListarTodos();
            return View(_produtoDAO.ListarTodos());
        }
    }
}