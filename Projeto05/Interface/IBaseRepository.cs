using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto05.Interface
{
    public interface IBaseRepository<T, TKey> 
        where T : class
        where TKey : struct
    {
        void Inserir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);
        List<T> ObterTodos();
        T ObterPorId(Guid id);
    }
}
