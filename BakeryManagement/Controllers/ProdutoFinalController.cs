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

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public ProdutoFinalController(ProdutoFinalDAO produtoFinalDAO)
        {
            _produtoFinalDAO = produtoFinalDAO;
            firebase = new FireSharp.FirebaseClient(config);
        }

        // GET: ProdutoFinal
        public IActionResult Index()
        {
            List<ProdutoFinal> produtosFinais = new List<ProdutoFinal>();

            FirebaseResponse reponse = firebase.Get("ProdutoFinal/Counter");
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

                reponse = firebase.Get("ProdutoFinal/" + cont);
                ProdutoFinal produtoFinal = reponse.ResultAs<ProdutoFinal>();

                if (produtoFinal != null)
                {
                    produtoFinal.Id = Convert.ToInt32(cont);
                    produtosFinais.Add(produtoFinal);
                }
            }

            return View(produtosFinais);
        }

        // GET: ProdutoFinal/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProdutoFinal produtoFinal, string drpTipo)
        {
            FirebaseResponse reponse = firebase.Get("ProdutoFinal/Counter");
            String counter = reponse.ResultAs<String>();

            SetResponse reponseFirebase;
            ProdutoFinal result;

            if (counter == null)
            {
                reponseFirebase = await firebase.SetAsync("ProdutoFinal/Counter", "1");
                result = reponse.ResultAs<ProdutoFinal>();

                counter = "0";
            }

            Int32 intCounter = Convert.ToInt32(counter);
            intCounter = intCounter + 1;

            var data = produtoFinal;

            reponseFirebase = await firebase.SetAsync("ProdutoFinal/" + intCounter, data);
            result = reponseFirebase.ResultAs<ProdutoFinal>();

            reponseFirebase = await firebase.SetAsync("ProdutoFinal/Counter", Convert.ToString(intCounter));

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            FirebaseResponse reponse = firebase.Get("ProdutoFinal/" + id);
            ProdutoFinal produtoFinal = reponse.ResultAs<ProdutoFinal>();

            return View(produtoFinal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoFinal pf)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("ProdutoFinal/" + pf.Id, pf);
            ProdutoFinal result = reponseFirebase.ResultAs<ProdutoFinal>();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FirebaseResponse reponse = await firebase.DeleteAsync("ProdutoFinal/" + id);
            ProdutoFinal produtoFinal = reponse.ResultAs<ProdutoFinal>();

            return RedirectToAction("Index");
        }
    }
}
