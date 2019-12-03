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
    [Route("api/ProdutoFinal")]
    [ApiController]
    public class ProdutoFinalAPIController : ControllerBase
    {
        private readonly ProdutoFinalDAO _produtoFinalDAO;
        public ProdutoFinalAPIController(ProdutoFinalDAO produtoFinalDAO)
        {
            _produtoFinalDAO = produtoFinalDAO;
        }

        //Get: /api/ProdutoFinal/BuscarPorId/{id}
        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public IActionResult BuscarPorId([FromRoute]int id)
        {
            ProdutoFinal pf = _produtoFinalDAO.BuscarPorId(id);
            if (pf != null)
            {
                return Ok(pf);
            }
            return NotFound(new { msg = "Produto Final não encontrado!" });
        }

    }
}