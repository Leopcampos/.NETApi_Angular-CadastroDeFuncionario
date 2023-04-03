using Dapper;
using ProjetoAPI01.Repository.Contracts;
using ProjetoAPI01.Repository.Entities;
using System.Data.SqlClient;

namespace ProjetoAPI01.Repository.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        //atributo
        private readonly string connectionstring;

        /// <summary>
        /// Método construtor para inicialização do atributo (injeção de dependência)
        /// </summary>
        public FuncionarioRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public void Inserir(Funcionario funcionario)
        {
            var query = @"INSERT INTO FUNCIONARIO
                        (ID, NOME, CPF, 
                        MATRICULA, DATAADMISSAO, SALARIO)
                        VALUES (@ID, @NOME, @CPF, 
                        @MATRICULA, @DATAADMISSAO, @SALARIO)";

            using var connection = new SqlConnection(connectionstring);
            connection.Execute(query, funcionario);
        }

        public void Alterar(Funcionario funcionario)
        {
            var query = @"UPDATE FUNCIONARIO SET
                        NOME = @NOME, MATRICULA = @MATRICULA,
                        DATAADMISSAO = @DATAADMISSAO, SALARIO = @SALARIO
                        WHERE ID = @ID";

            using var connection = new SqlConnection(connectionstring);
            connection.Execute(query, funcionario);
        }

        public void Excluir(Funcionario funcionario)
        {
            var query = @"DELETE FROM FUNCIONARIO WHERE ID = @ID";

            using var connection = new SqlConnection(connectionstring);
            connection.Execute(query, funcionario);
        }

        public List<Funcionario> ObterTodos()
        {
            var query = "SELECT * FROM FUNCIONARIO ORDER BY NOME";

            using var connection = new SqlConnection(connectionstring);
            return connection.Query<Funcionario>(query).ToList();
        }

        public Funcionario ObterPorCpf(string cpf)
        {
            var query = "SELECT * FROM FUNCIONARIO WHERE CPF = @CPF";

            using var connection = new SqlConnection(connectionstring);
            return connection.Query<Funcionario>(query, new { cpf }).FirstOrDefault();
        }

        public Funcionario ObterPorId(Guid id)
        {
            var query = "SELECT * FROM FUNCIONARIO WHERE ID = @ID";

            using var connection = new SqlConnection(connectionstring);
            return connection.Query<Funcionario>(query, new { id }).FirstOrDefault();
        }
    }
}
