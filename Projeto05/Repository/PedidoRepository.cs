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
    public class PedidoRepository : IPedidoRepository
    {
        private string connectionstring;

        public PedidoRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public void Inserir(Pedido obj)
        {
            var query = @"
                        INSERT INTO PEDIDO(IDPEDIDO, IDCLIENTE, DATAPEDIDO)
                        VALUES(@IdPedido, @IdCliente, @DataPedido)
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public void Inserir(Pedido pedido, List<ItemPedido> itens)
        {
            var queryPedido = @"
                            INSERT INTO PEDIDO(IDPEDIDO, IDCLIENTE, DATAPEDIDO)
                            VALUES(@IdPedido, @IdCliente, @DataPedido)
                    ";

            var queryItemPedido = @"
                            INSERT INTO ITEMPEDIDO(IDITEMPEDIDO, IDPEDIDO, IDPRODUTO, QUANTIDADE)
                            VALUES(@IdItemPedido, @IdPedido, @IdProduto, @Quantidade)
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(queryPedido, pedido, transaction);

                        foreach (var item in itens)
                        {
                            connection.Execute(queryItemPedido, item, transaction);
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        public void Alterar(Pedido obj)
        {
            var query = @"
                        UPDATE PEDIDO
                        SET
                            IDCLIENTE = @IdCliente,
                            DATAPEDIDO = @DataPedido
                        WHERE
                            IDPEDIDO = @IdPedido
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public void Excluir(Pedido obj)
        {
            var query = @"
                        DELETE FROM PEDIDO
                        WHERE IDPEDIDO = @IdPedido
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public List<Pedido> ObterTodos()
        {
            var query = @"
                        SELECT * FROM PEDIDO
                        ORDER BY DATAPEDIDO
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                return connection
                        .Query<Pedido>(query)
                        .ToList();
            }
        }

        public Pedido ObterPorId(Guid id)
        {
            var query = @"
                        SELECT * FROM PEDIDO
                        WHERE IDPEDIDO = @id
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                return connection
                        .Query<Pedido>(query, new { id })
                        .FirstOrDefault();
            }
        }
    }
}
