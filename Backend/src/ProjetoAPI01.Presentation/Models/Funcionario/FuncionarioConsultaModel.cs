using ProjetoAPI01.Presentation.Models.Dependente;

namespace ProjetoAPI01.Presentation.Models.Funcionario
{
    public class FuncionarioConsultaModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }

        public List<DependenteItemConsultaModel> Dependentes { get; set; }
    }
}