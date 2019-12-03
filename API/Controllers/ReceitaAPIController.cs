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
    [Route("api/Receita")]
    [ApiController]
    public class ReceitaAPIController : ControllerBase
    {
        private readonly ReceitaDAO _receitaDAO;

        public ReceitaAPIController(ReceitaDAO receitaDAO)
        {
            _receitaDAO = receitaDAO;

        }

        //Get: /api/Receita/BuscarPorNome/{Nome}
        [HttpGet]
        [Route("BuscarPorNome/{nome}")]
        public IActionResult BuscarPorNome([FromRoute]Receita receita)
        {
            Receita r = _receitaDAO.BuscarPorNome(receita);

            if (r != null)
            {
                return Ok(r);
            }
            return NotFound(new { msg = "Receita não encontrada!" });
        }

    }
}