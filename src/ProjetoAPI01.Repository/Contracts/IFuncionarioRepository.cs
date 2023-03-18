using ProjetoAPI01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoAPI01.Repository.Contracts
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

        #endregion
    }
}