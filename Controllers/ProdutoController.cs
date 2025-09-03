using CadastroClient_ASP.Net_SqlServer.Models;
using CadastroClient_ASP.Net_SqlServer.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;

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

                if (produtos.Count < 1)
                {
                    return StatusCode(404, new
                    {
                        Sucesso = false,
                        Mensagem = "Banco de dados sem usuários cadastrados"
                    });
                }

                return StatusCode(200, new
                {
                    Sucesso = true,
                    Usuarios = produtos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Sucesso = false,
                    Menssagem = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostUser([FromBody] ProdutoModel infoUser)
        {
            var camposVazios = new List<string>();

            if (string.IsNullOrWhiteSpace(infoUser.Nome)) camposVazios.Add("Nome");
            if (string.IsNullOrWhiteSpace(infoUser.Email)) camposVazios.Add("Email");
            if (string.IsNullOrWhiteSpace(infoUser.Senha)) camposVazios.Add("Senha");
            if (string.IsNullOrWhiteSpace(infoUser.DataNascimento)) camposVazios.Add("DataNascimento");

            if (camposVazios.Any())
            {
                return StatusCode(400, new
                {
                    Sucesso = false,
                    Mensagem = "Os seguintes campos estão em branco:",
                    Campos = camposVazios
                });
            }

            try
            {
                await _produtoServices.CreateUserAsync(infoUser);
                return StatusCode(200, new
                {
                    Sucesso = true,
                    Messagem = "Usuário cadastrado com sucesso."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Sucesso = false,
                    Mensagem = "Erro interno ao cadastrar usuário.",
                    Detalhes = ex.Message
                });
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
                    return StatusCode(404, new
                    {
                        Sucesso = false,
                        Menssagem = $"Usuário com id {idUser} não encontrado"
                    });
                }

                return StatusCode(200, new
                {
                    Sucesso = true,
                    Usuario = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Sucesso = false,
                    Menssagem =  ex.Message
                });
            }
        }

        [HttpDelete("{idUser}")]
        public async Task<ActionResult<string>> DeleteUser([FromRoute] int idUser)
        {
            try
            {
                var mensgRetorno = await _produtoServices.DeleteUserAsync(idUser);
                if (!mensgRetorno)
                {
                    return StatusCode(404, new
                    {
                        Sucesso = false,
                        Mensagem = $"Usuário com id {idUser} não encontrado."
                    });
                }

                return StatusCode(200, new
                {
                    Sucesso = true,
                    Mensagem = "Usuário removido com sucesso."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Sucesso = false,
                    Menssagem = ex.Message
                });
            }
        }

        [HttpPut("{idUser}")]
        public async Task<ActionResult<string>> PutUser([FromRoute] int idUser, [FromBody] UserUpdateDTO userUpdate)
        {
            try
            {
                var mensgRetorno = await _produtoServices.UpdateUserAsync(idUser, userUpdate);
                if (!mensgRetorno)
                {
                    return StatusCode(404, new
                    {
                        Sucesso = false,
                        Mensagem = $"Usuário com id {idUser} não encontrado."
                    });
                }

                return StatusCode(200, new
                {
                    Sucesso = true,
                    Mensagem = "Usuário atualizado com sucesso."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Sucesso = false,
                    Menssagem = ex.Message
                });
            }
        }
    }
}
