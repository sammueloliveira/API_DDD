using Domain.Entities;
using Domain.Interface.InterfaceMessage;
using Infra.Data;
using Infra.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGeneric<Message>, IMessage
    {

        private readonly DbContextOptions<Contexto> _OptionsBuilder;

        public RepositoryMessage()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }

        public async Task<List<Message>> ListarMessage(Expression<Func<Message, bool>> exMessage)
        {
            using (var banco = new Contexto(_OptionsBuilder))
            {
                return await banco.Message.Where(exMessage).AsNoTracking().ToListAsync();
            }
        }
    }
}
    
