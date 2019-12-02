using Domain;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProdutoFinalDAO : IRepository<ProdutoFinal>
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public ProdutoFinalDAO(Context context)
        {
            firebase = new FireSharp.FirebaseClient(config);
        }

        public ProdutoFinal BuscarPorId(int id)
        {
            FirebaseResponse reponse = firebase.Get("ProdutoFinal/" + id);
            ProdutoFinal produtoFinal = reponse.ResultAs<ProdutoFinal>();

            return produtoFinal;
        }

        public ProdutoFinal BuscarPorNome(ProdutoFinal objeto)
        {
            List<ProdutoFinal> produtosFinais = ListarTodos();

            foreach (ProdutoFinal produtoFinal in produtosFinais)
            {
                if (produtoFinal.Receita.Equals(objeto.Receita))
                {
                    return produtoFinal;
                }
            }
            return null;
        }

        public List<ProdutoFinal> ListarTodos()
        {
            List<ProdutoFinal> produtosFinais = new List<ProdutoFinal>();

            FirebaseResponse reponse = firebase.Get("ProdutoFinal/Counter");
            String counter = reponse.ResultAs<String>();

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

            return produtosFinais;
        }

        public async Task<bool> Create(ProdutoFinal produtoFinal)
        {
            if (BuscarPorNome(produtoFinal) == null)
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
                return true;
            }
            return false;
        }

        public async void Remover(int? id)
        {
            FirebaseResponse reponse = await firebase.DeleteAsync("ProdutoFinal/" + id);
            ProdutoFinal produtoFinal = reponse.ResultAs<ProdutoFinal>();
        }

        public async void Edit(ProdutoFinal pf)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("ProdutoFinal/" + pf.Id, pf);
            ProdutoFinal result = reponseFirebase.ResultAs<ProdutoFinal>();
        }

    }
}