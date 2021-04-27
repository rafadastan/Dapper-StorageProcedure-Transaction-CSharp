using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto05.Entities
{
    public class Pedido
    {
        public Guid IdPedido { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime DataPedido { get; set; }

        public Cliente Cliente { get; set; }
    }
}
