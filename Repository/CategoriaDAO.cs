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
    public class CategoriaDAO : IRepository<Categoria>
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public CategoriaDAO(Context context)
        {
            firebase = new FireSharp.FirebaseClient(config);
        }

        public Categoria BuscarPorId(int id)
        {
            FirebaseResponse reponse = firebase.Get("Categoria/" + id);
            Categoria categoria = reponse.ResultAs<Categoria>();

            return categoria;
        }

        public Categoria BuscarPorNome(Categoria objeto)
        {
            List<Categoria> categorias = ListarTodos();

            foreach (Categoria categoria in categorias)
            {
                if (categoria.Nome.Equals(objeto.Nome))
                {
                    return categoria;
                }
            }
            return null;
        }

        public List<Categoria> ListarTodos()
        {
            List<Categoria> categorias = new List<Categoria>();

            FirebaseResponse reponse = firebase.Get("Categoria/Counter");
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

                reponse = firebase.Get("Categoria/" + cont);
                Categoria categoria = reponse.ResultAs<Categoria>();

                if (categoria != null)
                {
                    categoria.Id = Convert.ToInt32(cont);
                    categorias.Add(categoria);
                }
            }

            return categorias;
        }

        public async Task<bool> Create(Categoria categoria)
        {
            if (BuscarPorNome(categoria) == null)
            {
                FirebaseResponse reponse = firebase.Get("Categoria/Counter");
                String counter = reponse.ResultAs<String>();

                SetResponse reponseFirebase;
                Categoria result;

                if (counter == null)
                {
                    reponseFirebase = await firebase.SetAsync("Categoria/Counter", "1");
                    result = reponse.ResultAs<Categoria>();

                    counter = "0";
                }

                Int32 intCounter = Convert.ToInt32(counter);
                intCounter = intCounter + 1;

                var data = categoria;

                reponseFirebase = await firebase.SetAsync("Categoria/" + intCounter, data);
                result = reponseFirebase.ResultAs<Categoria>();

                reponseFirebase = await firebase.SetAsync("Categoria/Counter", Convert.ToString(intCounter));
                return true;
            }

            return false;
        }

        public async void Remover(int? id)
        {
            FirebaseResponse reponse = await firebase.DeleteAsync("Categoria/" + id);
            Categoria categoria = reponse.ResultAs<Categoria>();
        }

        public async void Edit(Categoria c)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Categoria/" + c.Id, c);
            Categoria result = reponseFirebase.ResultAs<Categoria>();
        }
    }
}
