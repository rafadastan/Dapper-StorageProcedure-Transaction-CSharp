using Projeto05.Entities;
using Projeto05.Repository;
using System;
using System.Collections.Generic;

namespace Projeto05
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BD_Projeto05;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            try
            {
                //criando produtos..
                var produto1 = new Produto { IdProduto = Guid.NewGuid(), Nome = "Mouse", Preco = 40 };
                var produto2 = new Produto { IdProduto = Guid.NewGuid(), Nome = "Notebook", Preco = 3000 };

                //criando um cliente..
                var cliente = new Cliente { IdCliente = Guid.NewGuid(), Nome = "Ana Paula", Cpf = "12345678912" };

                //gravando os produtos..
                var produtoRepository = new ProdutoRepository(connectionstring);
                produtoRepository.Inserir(produto1);
                produtoRepository.Inserir(produto2);

                //gravando o cliente..
                var clienteRepository = new ClienteRepository(connectionstring);
                clienteRepository.Inserir(cliente);

                //gravando um pedido..
                var pedido = new Pedido { IdPedido = Guid.NewGuid(), IdCliente = cliente.IdCliente, DataPedido = DateTime.Now };

                List<ItemPedido> itens = new List<ItemPedido>();
                itens.Add(new ItemPedido { IdItemPedido = Guid.NewGuid(), IdPedido = pedido.IdPedido, IdProduto = produto1.IdProduto, Quantidade = 1 });
                itens.Add(new ItemPedido { IdItemPedido = Guid.NewGuid(), IdPedido = pedido.IdPedido, IdProduto = produto2.IdProduto, Quantidade = 1 });

                var pedidoRepository = new PedidoRepository(connectionstring);
                pedidoRepository.Inserir(pedido, itens);

                Console.WriteLine("\nDados gravados com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nErro: " + e.Message);
            }

            Console.ReadKey();
        }
    }
}
