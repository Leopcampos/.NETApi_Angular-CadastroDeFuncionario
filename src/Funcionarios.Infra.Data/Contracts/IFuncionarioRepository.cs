using Funcionarios.Infra.Data.Entities;

namespace Funcionarios.Infra.Data.Contracts
{
    public interface IFuncionarioRepository
    {
        #region Métodos abstratos

        void Inserir(Funcionario funcionario);
        void Alterar(Funcionario funcionario);
        void Excluir(Funcionario funcionario);

        List<Funcionario> ObterTodos();

        Funcionario ObterPorCpf(string cpf);
        Funcionario ObterPorId(Guid id);

        List<Dependente> ObterDependentes(Guid id);

        #endregion
    }
}