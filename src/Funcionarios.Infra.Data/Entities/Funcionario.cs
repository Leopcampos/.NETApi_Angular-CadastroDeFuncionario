namespace Funcionarios.Infra.Data.Entities
{
    public class Funcionario
    {
        #region Propriedades (Campos / Atributos)

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public DateTime DataAdmissao { get; set; }
        public decimal Salario { get; set; }

        #endregion

        #region Relacionamentos (Associações)

        public List<Dependente> Dependentes { get; set; }

        #endregion
    }
}