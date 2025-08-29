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
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idUser}")]
        public async Task<ActionResult<ProdutoModel>> GetUserById([FromRoute] int idUser)
        {
            try
            {
                var user = await _produtoServices.GetByIdAsync(idUser);

                if (user == null)
                {
                    return NotFound($"Usuário com ID {idUser} não cadastrado.");
                }
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
