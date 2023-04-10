using Microsoft.AspNetCore.Mvc;
using ProjetoAPI01.Presentation.Models.Dependente;
using ProjetoAPI01.Presentation.Models.Funcionario;
using ProjetoAPI01.Repository.Contracts;
using ProjetoAPI01.Repository.Entities;

namespace ProjetoAPI01.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependentesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(DependenteCadastroModel model, [FromServices] IDependenteRepository dependenteRepository)
        {
            try
            {
                var dependente = new Dependente();

                dependente.Id = Guid.NewGuid();
                dependente.Nome = model.Nome;
                dependente.DataNascimento = DateTime.Parse(model.DataNascimento);
                dependente.FuncionarioId = Guid.Parse(model.FuncionarioId);
                dependenteRepository.Inserir(dependente);
                return Ok("Dependente cadastrado com sucesso.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(DependenteEdicaoModel model, [FromServices] IDependenteRepository dependenteRepository)
        {
            try
            {
                //verificar se o dependente informado existe no banco de dados..
                var dependente = dependenteRepository.ObterPorId(Guid.Parse(model.Id));

                if (dependente != null)
                {
                    dependente.Nome = model.Nome;
                    dependente.DataNascimento = DateTime.Parse(model.DataNascimento);
                    dependente.FuncionarioId = Guid.Parse(model.FuncionarioId);
                    dependenteRepository.Alterar(dependente);
                    return Ok("Dependente atualizado com sucesso.");
                }
                else
                {
                    return BadRequest("Dependente não encontrado. Verifique o id enviado.");
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id, [FromServices] IDependenteRepository dependenteRepository)
        {
            try
            {
                //verificando se o dependente existe no banco de dados
                var dependente = dependenteRepository.ObterPorId(id);
                if (dependente != null)
                {
                    dependenteRepository.Excluir(dependente);
                    return Ok("Dependente excluído com sucesso.");
                }
                else
                {
                    return BadRequest("Dependente não encontrado. Verifique o id enviado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromServices] IDependenteRepository dependenteRepository)
        {
            try
            {
                var result = new List<DependenteConsultaModel>();
                foreach (var item in dependenteRepository.ObterTodos())
                {
                    result.Add(new DependenteConsultaModel
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        DataNascimento = item.DataNascimento,
                        Funcionario = new FuncionarioConsultaModel
                        {
                            Id = item.Funcionario.Id,
                            Nome = item.Funcionario.Nome,
                            Cpf = item.Funcionario.Cpf,
                            DataAdmissao = item.Funcionario.DataAdmissao,
                            Matricula = item.Funcionario.Matricula,
                            Salario = item.Funcionario.Salario
                        }
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(Guid id, [FromServices] IDependenteRepository dependenteRepository)
        {
            try
            {
                //verificando se o dependente informado
                //existe no banco de dados..
                var dependente = dependenteRepository.ObterPorId(id);
                if (dependente != null)
                {
                    return Ok(new DependenteConsultaModel
                    {
                        Id = dependente.Id,
                        Nome = dependente.Nome,
                        DataNascimento = dependente.DataNascimento,
                        Funcionario = new FuncionarioConsultaModel
                        {
                            Id = dependente.Funcionario.Id,
                            Nome = dependente.Funcionario.Nome,
                            Cpf = dependente.Funcionario.Cpf,
                            DataAdmissao = dependente.Funcionario.DataAdmissao,
                            Matricula = dependente.Funcionario.Matricula,
                            Salario = dependente.Funcionario.Salario
                        }
                    });
                }
                else
                {
                    return BadRequest("Dependente não encontrado. Verifique o id enviado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}