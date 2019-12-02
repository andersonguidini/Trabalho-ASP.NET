using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace BakeryManagement.Controllers
{
    public class HomeController : Controller
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public HomeController(ProdutoDAO produtoDAO)
        {
            firebase = new FireSharp.FirebaseClient(config);
        }

        public IActionResult Index()
        {
            List<Produto> produtos = new List<Produto>();
            List<ProdutoFinal> produtosFinais = new List<ProdutoFinal>();

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
    }
}