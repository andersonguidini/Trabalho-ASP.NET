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

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public ReceitaController(ReceitaDAO receitaDAO)
        {
            _receitaDAO = receitaDAO;
            firebase = new FireSharp.FirebaseClient(config);
        }

        // GET: Receita
        public IActionResult Index()
        {
            List<Receita> receitas = new List<Receita>();

            FirebaseResponse reponse = firebase.Get("Receita/Counter");
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

                reponse = firebase.Get("Receita/" + cont);
                Receita receita = reponse.ResultAs<Receita>();

                if (receita != null)
                {
                    receita.Id = Convert.ToInt32(cont);
                    receitas.Add(receita);
                }
            }

            return View(receitas);
        }

        // GET: Receita/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Receita receita, string drpTipo)
        {
            FirebaseResponse reponse = firebase.Get("Receita/Counter");
            String counter = reponse.ResultAs<String>();

            SetResponse reponseFirebase;
            Receita result;

            if (counter == null)
            {
                reponseFirebase = await firebase.SetAsync("Receita/Counter", "1");
                result = reponse.ResultAs<Receita>();

                counter = "0";
            }

            Int32 intCounter = Convert.ToInt32(counter);
            intCounter = intCounter + 1;

            var data = receita;

            reponseFirebase = await firebase.SetAsync("Receita/" + intCounter, data);
            result = reponseFirebase.ResultAs<Receita>();

            reponseFirebase = await firebase.SetAsync("Receita/Counter", Convert.ToString(intCounter));

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            FirebaseResponse reponse = firebase.Get("Receita/" + id);
            Receita receita = reponse.ResultAs<Receita>();

            return View(receita);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Receita r)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Receita/" + r.Id, r);
            Receita result = reponseFirebase.ResultAs<Receita>();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FirebaseResponse reponse = await firebase.DeleteAsync("Receita/" + id);
            Receita receita = reponse.ResultAs<Receita>();

            return RedirectToAction("Index");
        }

    }
}
