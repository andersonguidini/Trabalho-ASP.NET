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
    public class ReceitaDAO : IRepository<Receita>
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public ReceitaDAO(Context context)
        {
            firebase = new FireSharp.FirebaseClient(config);
        }

        public Receita BuscarPorId(int id)
        {
            int idReceita = id;
            FirebaseResponse reponse = firebase.Get("Receita/" + id);
            Receita receita = reponse.ResultAs<Receita>();

            receita.Id = idReceita;

            return receita;
        }

        public Receita BuscarPorNome(Receita objeto)
        {
            List<Receita> receitas = ListarTodos();

            foreach (Receita receita in receitas)
            {
                if (receita.Nome.Equals(objeto.Nome))
                {
                    return receita;
                }
            }
            return null;
        }

        public  List<Receita> ListarTodos()
        {
            List<Receita> receitas = new List<Receita>();

            FirebaseResponse reponse = firebase.Get("Receita/Counter");
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

                reponse = firebase.Get("Receita/" + cont);
                Receita receita = reponse.ResultAs<Receita>();

                if (receita != null)
                {
                    receita.Id = Convert.ToInt32(cont);
                    receitas.Add(receita);
                }
            }

            return receitas;
        }

        public async Task<bool> Create(Receita receita)
        {
            if (BuscarPorNome(receita) == null)
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
                return true;
            }
            return false;
        }

        public async void RemoverIngrediente(Receita r, Produto p)
        {
            FirebaseResponse reponse = await firebase.DeleteAsync("Receita/" + r.Id + "/Produto/" + p.Id);
            Produto produto = reponse.ResultAs<Produto>();
        }

        public async void Remover(int? id)
        {
            FirebaseResponse reponse = await firebase.DeleteAsync("Receita/" + id);
            Receita receita = reponse.ResultAs<Receita>();
        }

        public async void Edit(Receita r)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Receita/" + r.Id, r);
            Receita result = reponseFirebase.ResultAs<Receita>();
        }

        public async void AddIngrediente(Receita r, Produto p)
        {
            FirebaseResponse reponse = firebase.Get("Receita/"+r.Id+"/Produto/Counter");
            String counter = reponse.ResultAs<String>();

            SetResponse reponseFirebase;
            Receita result;

            if (counter == null)
            {
                reponseFirebase = await firebase.SetAsync("Receita/"+r.Id+"/Produto/Counter", "1");
                result = reponse.ResultAs<Receita>();

                counter = "0";
            }

            Int32 intCounter = Convert.ToInt32(counter);
            intCounter = intCounter + 1;

            var data = p;

            reponseFirebase = await firebase.SetAsync("Receita/" + r.Id + "/Produto/" + intCounter, data);
            result = reponseFirebase.ResultAs<Receita>();
            reponseFirebase = await firebase.SetAsync("Receita/" + r.Id + "/Produto/Counter", intCounter);

        }

        public List<Produto> BuscarIngredientes(Receita r)
        {
            List<Produto> produtos = new List<Produto>();

            FirebaseResponse reponse = firebase.Get("Receita/"+r.Id+"/Produto/Counter");
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

                reponse = firebase.Get("Receita/"+r.Id+"/Produto/" + cont);
                Produto produto = reponse.ResultAs<Produto>();

                if (produto != null)
                {
                    produto.Id = Convert.ToInt32(cont);
                    produtos.Add(produto);
                }
            }

            return produtos;
        }
    }
}