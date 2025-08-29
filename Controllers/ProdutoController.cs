using CadastroClient_ASP.Net_SqlServer.Models;
using CadastroClient_ASP.Net_SqlServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroClient_ASP.Net_SqlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoServices _produtoServices;

        public ProdutoController(ProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> GetUser()
        {
            try
            {
                var produtos = await _produtoServices.GetUserAsync();

                if (produtos == null)
                {
                    return NotFound("Sem usuários cadastrados.");
                }
                
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
