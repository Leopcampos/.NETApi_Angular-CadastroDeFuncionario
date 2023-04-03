using Dapper;
using ProjetoAPI01.Repository.Contracts;
using ProjetoAPI01.Repository.Entities;
using System.Data.SqlClient;

namespace ProjetoAPI01.Repository.Repositories
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
            var query = @"INSERT INTO DEPENDETE (ID, NOME, DATANASCIMENTO, FUNCIONARIOID)
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
            var query = "SELECT * FROM DEPENDENTE ORDER BY NOME";

            using var connection = new SqlConnection(connectionstring);
            return connection.Query<Dependente>(query).ToList();
        }

        public Dependente ObterPorId(Guid id)
        {
            var query = "SELECT * FROM DEPENDENTE WHERE ID = @ID";

            using var connection = new SqlConnection(connectionstring);
            return connection.Query<Dependente>(query, new { id }).FirstOrDefault();
        }
    }
}