using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAPI01.Repository.Entities
{
    public class Dependente
    {
        #region Propriedades (Campos)

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid FuncionarioId { get; set; }

        #endregion

        #region Relacionamentos (Associações)

        public Funcionario Funcionario { get; set; }

        #endregion
    }
}