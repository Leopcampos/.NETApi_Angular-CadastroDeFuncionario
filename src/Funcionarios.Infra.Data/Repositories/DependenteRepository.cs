using Dapper;
using Funcionarios.Infra.Data.Contracts;
using Funcionarios.Infra.Data.Entities;
using System.Data.SqlClient;

namespace Funcionarios.Infra.Data.Repositories
{
    public class DependenteRepository : IDependenteRepository
    {
        //atributo
        private readonly string connectionstring;

        /// <summary>
        /// Método construtor para inicialização do atributo (injeção de dependência)
        /// </summary>
        public DependenteRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public void Inserir(Dependente dependente)
        {
            var query = @"INSERT INTO DEPENDENTE (ID, NOME, DATANASCIMENTO, FUNCIONARIOID)
                        VALUES (@ID, @NOME, @DATANASCIMENTO, @FUNCIONARIOID)";

            using var connection = new SqlConnection(connectionstring);
            connection.Execute(query, dependente);
        }

        public void Alterar(Dependente dependente)
        {
            var query = @"UPDATE DEPENDENTE SET NOME = @NOME,
                        DATANASCIMENTO = @DATANASCIMENTO, FUNCIONARIOID = @FUNCIONARIOID 
                            WHERE ID = @ID";

            using var connection = new SqlConnection(connectionstring);
            connection.Execute(query, dependente);
        }

        public void Excluir(Dependente dependente)
        {
            var query = @"DELETE FROM DEPENDENTE 
                            WHERE ID = @ID";

            using var connection = new SqlConnection(connectionstring);
            connection.Execute(query, dependente);
        }

        public List<Dependente> ObterTodos()
        {
            var query = @"SELECT * FROM DEPENDENTE D 
                            INNER JOIN FUNCIONARIO F
                            ON F.ID = D.FUNCIONARIOID
                            ORDER BY D.NOME";

            using var connection = new SqlConnection(connectionstring);

            return connection.Query(query,
                (Dependente d, Funcionario f) =>
                {
                    d.Funcionario = f; //associando o dependente ao funcionário
                    return d; //retornando o dependente
                },
                splitOn: "FuncionarioId" //atributo chave estrangeira
                ).ToList();
        }

        public Dependente ObterPorId(Guid id)
        {
            var query = @"SELECT * FROM DEPENDENTE D
                            INNER JOIN FUNCIONARIO F
                            ON F.ID = D.FUNCIONARIOID
                            WHERE D.ID = @Id";

            using var connection = new SqlConnection(connectionstring);

            return connection.Query(query,
               (Dependente d, Funcionario f) =>
               {
                   d.Funcionario = f; //associando o dependente ao funcionário
                   return d; //retornando o dependente
               },
               new { id }, //parametro where da consulta
               splitOn: "FuncionarioId" //atributo chave estrangeira
               ).FirstOrDefault();
        }
    }
}