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
    public class FornecedorDAO : IRepository<Fornecedor>
    {
        
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l0mQk1Nwesby4YaQUeUPRm87yiOVFTE0q6RX7nW3",
            BasePath = "https://trabalho-asp.firebaseio.com/"
        };
        IFirebaseClient firebase;

        public FornecedorDAO()
        {
            firebase = new FireSharp.FirebaseClient(config);
        }

        public Fornecedor BuscarPorId(int id)
        {
            FirebaseResponse reponse = firebase.Get("Fornecedor/" + id);
            Fornecedor fornecedor = reponse.ResultAs<Fornecedor>();

            return fornecedor;
        }

        public Fornecedor BuscarPorNome(Fornecedor objeto)
        {
            List<Fornecedor> fornecedores = ListarTodos();

            foreach(Fornecedor fornecedor in fornecedores)
            {
                if (fornecedor.Nome.Equals(objeto.Nome))
                {
                    return fornecedor;
                }
            }
            return null;
        }

        public Fornecedor BuscarPorNomeAPI(string nome )
        {
            List<Fornecedor> fornecedores = ListarTodos();

            foreach (Fornecedor fornecedor in fornecedores)
            {
                if (fornecedor.Nome.Equals(nome))
                {
                    return fornecedor;
                }
            }
            return null;
        }

        public List<Fornecedor> ListarTodos()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            FirebaseResponse reponse = firebase.Get("Fornecedor/Counter");
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

                reponse = firebase.Get("Fornecedor/" + cont);
                Fornecedor fornecedor = reponse.ResultAs<Fornecedor>();

                if (fornecedor != null)
                {
                    fornecedor.Id = Convert.ToInt32(cont);
                    fornecedores.Add(fornecedor);
                }
            }

            return fornecedores;
        }

        public async Task<bool> Create(Fornecedor fornecedor)
        {
            if(BuscarPorNome(fornecedor) == null)
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
                fornecedor.Id = intCounter;
                var data = fornecedor;

                reponseFirebase = await firebase.SetAsync("Fornecedor/" + intCounter, data);
                result = reponseFirebase.ResultAs<Fornecedor>();

                reponseFirebase = await firebase.SetAsync("Fornecedor/Counter", Convert.ToString(intCounter));
                return true;
            }

            return false;
        }

        public async void Remover(int? id)
        {
            FirebaseResponse reponse = await firebase.DeleteAsync("Fornecedor/" + id);
            Fornecedor fornecedor = reponse.ResultAs<Fornecedor>();
        }

        public async void Edit(Fornecedor f)
        {
            SetResponse reponseFirebase = await firebase.SetAsync("Fornecedor/" + f.Id, f);
            Fornecedor result = reponseFirebase.ResultAs<Fornecedor>();
        }


        public void Editar(Fornecedor objeto)
        {
            throw new NotImplementedException();
        }
    }
}