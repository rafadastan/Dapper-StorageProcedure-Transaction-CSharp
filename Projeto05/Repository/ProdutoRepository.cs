using Dapper;
using Projeto05.Entities;
using Projeto05.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto05.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private string connectionstring;

        public ProdutoRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public void Inserir(Produto obj)
        {
            var query = @"
                            INSERT INTO PRODUTO(IDPRODUTO, NOME, PRECO)
                            VALUES(@IdProduto, @Nome, @Preco)
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public void Alterar(Produto obj)
        {
            var query = @"
                            UPDATE PRODUTO
                            SET
                                NOME = @Nome,
                                PRECO = @Preco
                            WHERE
                                IDPRODUTO = @IdProduto
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public void Excluir(Produto obj)
        {
            var query = @"
                            DELETE FROM PRODUTO
                            WHERE IDPRODUTO = @IdProduto
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public List<Produto> ObterTodos()
        {
            var query = @"
                            SELECT * FROM PRODUTO
                            ORDER BY NOME
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                return connection
                        .Query<Produto>(query)
                        .ToList();
            }
        }

        public Produto ObterPorId(Guid id)
        {
            var query = @"
                            SELECT * FROM PRODUTO
                            WHERE IDPRODUTO = @id
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                return connection
                        .Query<Produto>(query, new { id })
                        .FirstOrDefault();
            }
        }
    }
}
