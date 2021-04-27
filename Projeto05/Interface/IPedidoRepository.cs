using Projeto05.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto05.Interface
{
    public interface IPedidoRepository : IBaseRepository<Pedido, Guid>
    {
        void Inserir(Pedido pedido, List<ItemPedido> itens);
    }
}
