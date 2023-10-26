using System.ComponentModel.DataAnnotations;

namespace Funcionarios.Models.Dependente
{
    public class DependenteCadastroModel
    {
        [Required(ErrorMessage = "Por favor, informe o nome do dependente.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de nascimento.")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor, informe o id do funcionário.")]
        public string FuncionarioId { get; set; }
    }
}