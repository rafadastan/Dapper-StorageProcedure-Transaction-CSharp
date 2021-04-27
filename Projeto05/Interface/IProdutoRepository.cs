using Projeto05.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto05.Interface
{
    public interface IProdutoRepository : IBaseRepository<Produto, Guid>
    {
    }
}
