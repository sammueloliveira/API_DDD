using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.InterfaceService
{
    public interface IServiceMessage
    {
        Task Adicionar(Message Objeto);

        Task Atualizar(Message Objeto);

        Task<List<Message>> ListarMessageAtivas();

    }
}
