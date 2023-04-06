using ProjetoAPI01.Presentation.Models.Funcionario;

namespace ProjetoAPI01.Presentation.Models.Dependente
{
    public class DependenteConsultaModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }


        //Relacionamento
        public FuncionarioConsultaModel Funcionario { get; set; }
    }
}