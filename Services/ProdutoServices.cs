using CadastroClient_ASP.Net_SqlServer.Data;
using CadastroClient_ASP.Net_SqlServer.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroClient_ASP.Net_SqlServer.Services
{
    public class ProdutoServices
    {
        private readonly AppDBContext _context;

        public ProdutoServices(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoModel>> GetUserAsync()
        {
            return await _context.Produto.ToListAsync();
        }

        public async Task<ProdutoModel> GetByIdAsync(int idUser)
        {
            var usuario = await _context.Produto.FindAsync(idUser);
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }

        public async Task<ProdutoModel> CreateUserAsync(ProdutoModel user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(int idUser)
        {
            var usuarioIdentificado = await _context.Produto.FindAsync(idUser);
            if (usuarioIdentificado == null)
            {
                return false;
            }

            _context.Produto.Remove(usuarioIdentificado);
            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Produto', RESEED, 0)");
            return true;
        }

        public async Task<string> UpdateUserAsync(int idUser, UserUpdateDTO userUpdate)
        {
            var usuarioIdentificado = await _context.Produto.FindAsync(idUser);
            if (usuarioIdentificado == null)
            {
                return "Usuário não cadastrado!";
            }

            usuarioIdentificado.Nome = userUpdate.NovoNome ?? usuarioIdentificado.Nome;
            usuarioIdentificado.Email = userUpdate.NovoEmail ?? usuarioIdentificado.Email;
            usuarioIdentificado.Senha = userUpdate.NovaSenha ?? usuarioIdentificado.Senha;
            usuarioIdentificado.DataNascimento = userUpdate.NovaDataNascimento ?? usuarioIdentificado.DataNascimento;

            _context.Produto.Update(usuarioIdentificado);
            await _context.SaveChangesAsync();

            return "Usuário atualizado com sucesso!";
        }
    }
}
