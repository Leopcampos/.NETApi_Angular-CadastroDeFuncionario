using ProjetoAPI01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAPI01.Repository.Contracts
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