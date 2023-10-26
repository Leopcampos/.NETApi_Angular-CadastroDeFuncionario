using System.ComponentModel.DataAnnotations;

namespace Funcionarios.Models.Funcionario
{
    public class FuncionarioEdicaoModel
    {
        [Required(ErrorMessage = "Por favor, informe o id do funcionário.")]
        public string Id { get; set; }

        [StringLength(150, MinimumLength = 6, ErrorMessage = "Por favor, informe no mínimo {2} e no máximo {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe o nome do funcionário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a matrícula do funcionário.")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de admissão do funcionário.")]
        public string DataAdmissao { get; set; }

        [Required(ErrorMessage = "Por favor, informe o salário do funcionário.")]
        public string Salario { get; set; }
    }
}