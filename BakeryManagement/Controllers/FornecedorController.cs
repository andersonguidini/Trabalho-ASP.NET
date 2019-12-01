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

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public FornecedorController(FornecedorDAO fornecedorDAO)
        {
            _fornecedorDAO = fornecedorDAO;
            firebase = new FireSharp.FirebaseClient(config);
        }

        // GET: Fornecedor
        public IActionResult Index()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            FirebaseResponse reponse = firebase.Get("Fornecedor/Counter");
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

                reponse = firebase.Get("Fornecedor/" + cont);
                Fornecedor fornecedor = reponse.ResultAs<Fornecedor>();

                if (fornecedor != null)
                {
                    fornecedor.Id = Convert.ToInt32(cont);
                    fornecedores.Add(fornecedor);
                }
            }

            return View(fornecedores);
        }

        // GET: Fornecedor/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Fornecedor fornecedor, string drpTipo)
        {
            FirebaseResponse reponse = firebase.Get("Fornecedor/Counter");
            String counter = reponse.ResultAs<String>();

            SetResponse reponseFirebase;
            Fornecedor result;

            if (counter == null)
            {
                reponseFirebase = await firebase.SetAsync("Fornecedor/Counter", "1");
                result = reponse.ResultAs<Fornecedor>();

                counter = "0";
            }

            Int32 intCounter = Convert.ToInt32(counter);
            intCounter = intCounter + 1;

            var data = fornecedor;

            reponseFirebase = await firebase.SetAsync("Fornecedor/" + intCounter, data);
            result = reponseFirebase.ResultAs<Fornecedor>();

            reponseFirebase = await firebase.SetAsync("Fornecedor/Counter", Convert.ToString(intCounter));

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            FirebaseResponse reponse = firebase.Get("Fornecedor/" + id);
            Fornecedor fornecedor = reponse.ResultAs<Fornecedor>();

            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Fornecedor f)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Fornecedor/" + f.Id, f);
            Fornecedor result = reponseFirebase.ResultAs<Fornecedor>();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FirebaseResponse reponse = await firebase.DeleteAsync("Fornecedor/" + id);
            Fornecedor fornecedor = reponse.ResultAs<Fornecedor>();

            return RedirectToAction("Index");
        }
    }
}
