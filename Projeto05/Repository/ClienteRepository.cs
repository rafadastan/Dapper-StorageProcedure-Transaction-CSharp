using Dapper;
using Projeto05.Entities;
using Projeto05.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Projeto05.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private string connectionstring;

        public ClienteRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public void Inserir(Cliente obj)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute("SP_INSERIRCLIENTE",
                        new
                        {
                            P_IDCLIENTE = obj.IdCliente,
                            P_NOME = obj.Nome,
                            P_CPF = obj.Cpf
                        },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public void Alterar(Cliente obj)
        {
            var query = @"
                            UPDATE CLIENTE
                            SET
                                NOME = @Nome,
                                CPF = @Cpf
                            WHERE
                                IDCLIENTE = @IdCliente
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public void Excluir(Cliente obj)
        {
            var query = @"
                            DELETE FROM CLIENTE
                            WHERE IDCLIENTE = @IdCliente
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                connection
                    .Execute(query, obj);
            }
        }

        public List<Cliente> ObterTodos()
        {
            var query = @"
                            SELECT * FROM CLIENTE
                            ORDER BY NOME
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                return connection
                    .Query<Cliente>(query)
                    .ToList();
            }
        }

        public Cliente ObterPorId(Guid id)
        {
            var query = @"
                            SELECT * FROM CLIENTE
                            WHERE IDCLIENTE = @id
                    ";

            using (var connection = new SqlConnection(connectionstring))
            {
                return connection
                    .Query<Cliente>(query, new { id })
                    .FirstOrDefault();
            }
        }
    }
}