using Dapper;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Produto produto)
        {
            var query = @"INSERT INTO PRODUTO(ID, NOME, PRECO, QUANTIDADE) 
                          VALUES(@ID, @NOME, @PRECO, @QUANTIDADE)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, produto);
            }
        }

        public void Update(Produto produto)
        {
            var query = @"UPDATE PRODUTO SET NOME = @NOME, PRECO = @PRECO, QUANTIDADE = @QUANTIDADE 
                          WHERE ID = @ID";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, produto);
            }
        }

        public void Delete(Produto produto)
        {
            var query = @"DELETE FROM PRODUTO WHERE ID = @ID";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, produto);
            }
        }

        public List<Produto> GetAll()
        {
            var query = "SELECT * FROM PRODUTO ORDER BY NOME";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto>(query).ToList();
            }
        }

        public Produto GetById(Guid id)
        {
            var query = "SELECT * FROM PRODUTO WHERE ID = @ID";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Produto>(query, new { id });
            }
        }
    }
}
