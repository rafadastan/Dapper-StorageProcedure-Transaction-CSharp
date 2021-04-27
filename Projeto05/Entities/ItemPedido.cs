using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto05.Entities
{
    public class ItemPedido
    {
        public Guid IdItemPedido { get; set; }
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }

        #region Associações
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
        #endregion
    }
}
