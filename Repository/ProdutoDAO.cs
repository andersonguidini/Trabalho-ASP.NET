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
    public class ProdutoDAO : IRepository<Produto>
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public ProdutoDAO(Context context)
        {
            firebase = new FireSharp.FirebaseClient(config);
        }

        public Produto BuscarPorId(int id)
        {
            FirebaseResponse reponse = firebase.Get("Produto/" + id);
            Produto produto = reponse.ResultAs<Produto>();

            return produto;
        }

        public Produto BuscarPorNome(Produto objeto)
        {
            List<Produto> produtos = ListarTodos();

            foreach (Produto produto in produtos)
            {
                if (produto.Nome.Equals(objeto.Nome))
                {
                    return produto;
                }
            }
            return null;
        }

        public List<Produto> ListarTodos()
        {
            List<Produto> produtos = new List<Produto>();

            FirebaseResponse reponse = firebase.Get("Produto/Counter");
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

                reponse = firebase.Get("Produto/" + cont);
                Produto produto = reponse.ResultAs<Produto>();

                if (produto != null)
                {
                    produto.Id = Convert.ToInt32(cont);
                    produtos.Add(produto);
                }
            }

            return produtos;
        }

        public async Task<bool> Create(Produto produto)
        {
            if (BuscarPorNome(produto) == null)
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
                return true;
            }
            return false;
        }

        public async void Remover(int? id)
        {
            FirebaseResponse reponse = await firebase.DeleteAsync("Produto/" + id);
            Produto produto = reponse.ResultAs<Produto>();
        }

        public async void Edit(Produto p)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Produto/" + p.Id, p);
            Produto result = reponseFirebase.ResultAs<Produto>();
        }

    }
}