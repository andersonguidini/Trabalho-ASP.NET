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

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public ProdutoController(ProdutoDAO produto)
        {
            _produtoDAO = produto;
            firebase = new FireSharp.FirebaseClient(config);
        }

        // GET: Produto
        public IActionResult Index()
        {
            List<Produto> produtos = new List<Produto>();

            FirebaseResponse reponse = firebase.Get("Produto/Counter");
            String counter = reponse.ResultAs<String>();

            //-------------------------------------------------------------------------------------------------//

            if (counter == null)
            {
                counter = "0";
            }

            int cont = 0;

            while (true)
            {
                if (cont == Convert.ToInt32(counter))
                {
                    break;
                }
                cont = cont + 1;

                reponse = firebase.Get("Produto/" + cont);
                Produto produto = reponse.ResultAs<Produto>();

                if (produto != null)
                {
                    produto.Id = Convert.ToInt32(cont);
                    produtos.Add(produto);
                }
            }

            return View(produtos);
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produto produto, string drpTipo)
        {
            FirebaseResponse reponse = firebase.Get("Produto/Counter");
            String counter = reponse.ResultAs<String>();

            SetResponse reponseFirebase;
            Produto result;

            if (counter == null)
            {
                reponseFirebase = await firebase.SetAsync("Produto/Counter", "1");
                result = reponse.ResultAs<Produto>();

                counter = "0";
            }

            Int32 intCounter = Convert.ToInt32(counter);
            intCounter = intCounter + 1;

            var data = produto;

            reponseFirebase = await firebase.SetAsync("Produto/" + intCounter, data);
            result = reponseFirebase.ResultAs<Produto>();

            reponseFirebase = await firebase.SetAsync("Produto/Counter", Convert.ToString(intCounter));

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            FirebaseResponse reponse = firebase.Get("Produto/" + id);
            Produto produto = reponse.ResultAs<Produto>();

            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Produto p)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Produto/" + p.Id, p);
            Produto result = reponseFirebase.ResultAs<Produto>();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FirebaseResponse reponse = await firebase.DeleteAsync("Produto/" + id);
            Produto produto = reponse.ResultAs<Produto>();

            return RedirectToAction("Index");
        }
    }
}
