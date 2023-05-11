using AutoMapper;
using Domain.Entities;
using Domain.Interface.InterfaceMessage;
using Domain.Interface.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Apresentation.Models;

namespace Web_Apresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IMessage _IMessage;
        private readonly IServiceMessage _IServiceMessage;
        public MessageController(IMapper IMapper, IMessage IMessage, IServiceMessage IServiceMessage)
        {
            _IMapper = IMapper;
            _IMessage = IMessage;
            _IServiceMessage = IServiceMessage;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await RetornarIdUsuarioLogado();
            var messageMap = _IMapper.Map<Message>(message);
           
            await _IServiceMessage.Adicionar(messageMap);
            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPut("/api/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            
            await _IServiceMessage.Atualizar(messageMap);
            return messageMap.Notificacoes;
        }
        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            await _IMessage.Delete(messageMap);
            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetEntityById")]
        public async Task<MessageViewModel> GetEntityById(Message message)
        {
            message = await _IMessage.GetEntityById(message.Id);
            var messageMap = _IMapper.Map<MessageViewModel>(message);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var mensagens = await _IMessage.List();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(mensagens);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/ListarMessageAtivas")]
        public async Task<List<MessageViewModel>> ListarMessageAtivas()
        {
            var mensagens = await _IServiceMessage.ListarMessageAtivas();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(mensagens);
            return messageMap;
        }
         private async Task<string> RetornarIdUsuarioLogado()
            {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }

            return string.Empty;

        }


    }
}
