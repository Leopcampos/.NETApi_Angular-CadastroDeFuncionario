using Funcionarios.Infra.Data.Entities;

namespace Funcionarios.Infra.Data.Contracts
{
    public interface IDependenteRepository
    {
        #region Métodos abstratos

        void Inserir(Dependente dependente);
        void Alterar(Dependente dependente);
        void Excluir(Dependente dependente);

        List<Dependente> ObterTodos();
        Dependente ObterPorId(Guid id);

        #endregion
    }
}