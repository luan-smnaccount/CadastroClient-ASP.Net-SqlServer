using CadastroClient_ASP.Net_SqlServer.Models;
using CadastroClient_ASP.Net_SqlServer.Services;
using DTO;
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
        public async Task<ActionResult<List<ProdutoModel>>> GetUser() // Adicionar uma mensg de retorno caso não existam usuários cadastrados
        {
            try
            {
                var produtos = await _produtoServices.GetUserAsync();

                if (produtos.Count < 1)
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

        [HttpGet("{idUser}")]
        public async Task<ActionResult<ProdutoModel>> GetUserById([FromRoute] int idUser) // Adicionar uma mensagem de retorno caso não exista usuário com o ID
        {
            try
            {
                var user = await _produtoServices.GetByIdAsync(idUser);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> PostUser([FromBody] ProdutoModel infoUser) // Criar middleware para validar as entradas e mudar para uma mensagem de retorno
        {
            var usuario = new ProdutoModel
            {
                Nome = infoUser.Nome,
                Email = infoUser.Email,
                Senha = infoUser.Senha,
                DataNascimento = infoUser.DataNascimento
            };

            try
            {
                var user = await _produtoServices.CreateUserAsync(usuario);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idUser}")]
        public async Task<ActionResult<string>> DeleteUser([FromRoute] int idUser) // Mudar para que o ID seja resetado ao remover um usuário
        {
            try
            {
                var mensgRetorno = await _produtoServices.DeleteUserAsync(idUser);
                return Ok(mensgRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idUser}")]
        public async Task<ActionResult<string>> PutUser([FromRoute] int idUser, [FromBody] UserUpdateDTO userUpdate) // Garantir que caso o valor informado seja nulo, o sistema mantenha cadastrado o mesmo
        {
            try
            {
                var mensgRetorno = await _produtoServices.UpdateUserAsync(idUser, userUpdate);
                return Ok(mensgRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
