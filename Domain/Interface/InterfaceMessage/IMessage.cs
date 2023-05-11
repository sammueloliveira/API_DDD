using Domain.Entities;
using Domain.Interface.Generic;
using System.Linq.Expressions;

namespace Domain.Interface.InterfaceMessage
{
    public interface IMessage : IGeneric<Message>

         {
            Task<List<Message>> ListarMessage(Expression<Func<Message, bool>> exMessage);
         }
   
}
