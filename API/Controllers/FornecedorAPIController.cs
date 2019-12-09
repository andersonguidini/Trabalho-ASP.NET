using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Fornecedor")]
    [ApiController]
    public class FornecedorAPIController : ControllerBase
    {
        private readonly FornecedorDAO _fornecedorDAO;

        public FornecedorAPIController(FornecedorDAO fornecedorDAO)
        {
            _fornecedorDAO = fornecedorDAO;

        }


        //Get: /api/Fornecedor/BuscarPorNome/{Nome}
        [HttpGet]
        [Route("BuscarPorNome/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            Fornecedor f = _fornecedorDAO.BuscarPorNomeAPI(nome);

            if(f != null)
            {
                return Ok(f);
            }
            return NotFound(new { msg = "Fornecedor não encontrado!" });
        }

    }
}