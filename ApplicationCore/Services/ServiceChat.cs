using ApplicationCore.Interfaces;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceChat : IServiceChat
    {
        public IEnumerable<Chat> GetChatsByVendedorProducto(int id, int idProducto)
        {
            IRepositoryChat repositoryChat = new RepositoryChat();

            return repositoryChat.GetChatsByVendedorProducto(id, idProducto);
        }
    }
}
